using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : VRInputIncapsulator {
    private VRTeleport teleport;
    [SerializeField] private PlayerDesktop player;

    void Start()
    {
        teleport = GetComponent<VRTeleport>();
        //player = GetComponentInChildren<PlayerDesktop>();
    }

    public override void OnLeftTouchPressed()
    {
        //Debug.Log("Left trigger pressed");
        teleport.AimTeleportLaser(LeftController);
    }


    public override void OnLeftTouchReleased()
    {
        teleport.AttemptTeleportation();
        //Debug.Log("Left trigger released");
    }

    public override void OnRightTriggerPressed()
    {
        player.FireGun();
    }

    public override void OnRightGripDown()
    {
        player.SwitchGun();
    }
}
