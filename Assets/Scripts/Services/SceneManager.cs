using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneManager : NetworkBehaviour
{
    private static SceneManager instance = null;
    private bool IAmUseless = false;

    [Header("Enemies")]
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> spawnPoints;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarningFormat("Multiple instances of {0} (singleton) on the scene (objects {1}, {2})! Exterminate!!!1", 
                                    this.GetType(), instance.gameObject.name, gameObject.name);
            IAmUseless = true;
            Destroy(this);
        }

        if (!FindObjectOfType<NetManager>())
            Debug.LogErrorFormat("Can't find NetManager on the scene! Prefabs will not be spawned!");

        FindObjectOfType<NetManager>().AddSpawnablePrefabs(enemyPrefabs);
    }
    
    void Start()
    {
        CmdSpawnEnemies();
    }

    [Command]
    void CmdSpawnEnemies()
    {
        foreach(var spawnPoint in spawnPoints)
            foreach(var enemyPrefab in enemyPrefabs)
            {
                var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<FrogAI>().patrolingCenter = GameObject.FindWithTag("FrogPatrolCenter").transform;
                NetworkServer.Spawn(enemy);
                //RpcSpawnEnemy(enemy);
                //enemy.GetComponentInChildren<BaseAI>().enabled = true;
                //enemy.GetComponentInChildren<BaseEntity>().enabled = true;
            }
        
    }

    [ClientRpc]
    void RpcSpawnEnemy(GameObject enemy)
    {
        if (Network.isServer)
            return;

        enemy.GetComponentInChildren<BaseAI>().enabled = false;
        enemy.GetComponentInChildren<BaseEntity>().enabled = false;
    }
    public void OnDestroy()
    {
        if (!IAmUseless)
            instance = null;
    }
}