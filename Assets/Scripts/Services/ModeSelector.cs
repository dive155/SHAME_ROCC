using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTypes
{
    Desktop, VR, XD, Fly, FiveD
}


public class ModeSelector : MonoBehaviour {

    //static public PlayerTypes playerType = PlayerTypes.Desktop;

    [SerializeField]
    SceneManager sceneManager;

    [Header("After the playerType was selected:")]
    [SerializeField]
    List<GameObject> thingsToDisable;

    [SerializeField]
    List<GameObject> thingsToEnable;

    public void OnDesktop()
    {
        sceneManager.playerType = PlayerTypes.Desktop;
        OnSelected();
    }

    public void OnVive()
    {
        sceneManager.playerType = PlayerTypes.VR;
        OnSelected();
    }

    public void OnSelected()
    {
        foreach (var thing in thingsToDisable)
        {
            thing.SetActive(false);
        }
        foreach (var thing in thingsToEnable)
        {
            thing.SetActive(true);
        }
        sceneManager.SpawnPlayer();
    }

}
