using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTypes
{
    Desktop, VR, XD, Fly, FiveD
}


public class ModeSelector : MonoBehaviour {

    static public PlayerTypes playerType = PlayerTypes.Desktop;

    static public void Desktop()
    {
        playerType = PlayerTypes.Desktop;
    }

    static public void Vive()
    {
        playerType = PlayerTypes.VR;
    }

    static public void XD()
    {
        playerType = PlayerTypes.XD;
    }

    static public void Fly()
    {
        playerType = PlayerTypes.Fly;
    }

    static public void FiveD()
    {
        playerType = PlayerTypes.FiveD;
    }
}
