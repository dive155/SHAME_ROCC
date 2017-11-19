using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance = null;
    private bool IAmUseless = false;


    [SerializeField] 
    public PlayerTypes playerType;

    [SerializeField] 
    Transform spawnPoint;

    [SerializeField]
    private Canvas desktopUI;


    [Header("Player prefabs")]
    [SerializeField]
    GameObject desktopPrefab;

    [SerializeField] GameObject vrPrefab;
    [SerializeField] GameObject xdPrefab;
    [SerializeField] GameObject flyPrefab;
    [SerializeField] GameObject fiveDPrefab;

    private bool paused = false;
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
           

        //playerType = ModeSelector.playerType;
        /*switch (playerType)
        {
            case PlayerTypes.Desktop:
                Instantiate(desktopPrefab, spawnPoint.position, spawnPoint.rotation);
                VRSettings.enabled = false;
                break;

            case PlayerTypes.VR:
                Instantiate(vrPrefab, spawnPoint.position, spawnPoint.rotation);
                VRSettings.enabled = true;
                break;
        } */
    }

    public void SpawnPlayer()
    {
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
        Cursor.visible = false;
    }

    public void OnDestroy()
    {
        if (IAmUseless)
            instance = null;
    }

    private void Update()
    {
        if (playerType == PlayerTypes.Desktop && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Unpause()
    {
        desktopUI.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
        Cursor.visible = false;
    }

    public void Pause()
    {
        desktopUI.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        paused = true;
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}