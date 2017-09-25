using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class SceneManager : MonoBehaviour {
    enum PlayerTypes
    {
        Desktop, VR, XD, Fly, FiveD
    }

    [SerializeField] PlayerTypes playerType;
    [SerializeField] Transform spawnPoint;

    [Header("Player prefabs")]
    [SerializeField] GameObject desktopPrefab;
    [SerializeField] GameObject vrPrefab;
    [SerializeField] GameObject xdPrefab;
    [SerializeField] GameObject flyPrefab;
    [SerializeField] GameObject fiveDPrefab;

	// Use this for initialization
	void Awake () 
    {
        if (FindObjectsOfType<SceneManager>().Length > 1)
            Debug.LogError("Multiple instances of SceneManager (singletone) on the scene!");

        Cursor.visible = false;

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


}
