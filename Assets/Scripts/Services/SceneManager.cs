using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance = null;
    private bool IAmUseless = false;


    [SerializeField] PlayerTypes playerType;
    [SerializeField] Transform spawnPoint;

    [Header("Player prefabs")]
    [SerializeField]
    GameObject desktopPrefab;

    [SerializeField] GameObject vrPrefab;
    [SerializeField] GameObject xdPrefab;
    [SerializeField] GameObject flyPrefab;
    [SerializeField] GameObject fiveDPrefab;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("Multiple instances of SceneManager (singleton) on the scene! Exterminate!!!1");
            IAmUseless = true;
            Destroy(this);
        }

        Cursor.visible = false;

        //playerType = ModeSelector.playerType;
        switch (playerType)
        {
            case PlayerTypes.Desktop:
                Instantiate(desktopPrefab, spawnPoint.position, spawnPoint.rotation);
                VRSettings.enabled = false;
                break;

            case PlayerTypes.VR:
                Instantiate(vrPrefab, spawnPoint.position, spawnPoint.rotation);
                VRSettings.enabled = true;
                break;
        }
    }

    public void OnDestroy()
    {
        if (IAmUseless)
            instance = null;
    }
}