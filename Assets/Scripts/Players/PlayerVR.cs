using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVR : PlayerDesktop {

    [SerializeField] Transform cameraRig;

    public override void Start()
    {
        base.Start();
        startingPosition = cameraRig.transform.position;
    }

    protected override void RpcOnDeath()
    {
        base.RpcOnDeath();
        cameraRig.transform.position = startingPosition;
    } 


}
