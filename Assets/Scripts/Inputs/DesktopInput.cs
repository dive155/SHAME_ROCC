using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : BaseInputManager {

    PlayerDesktop player;
    MoverDesktop mover;

    void Start()
    {
        player = GetComponent<PlayerDesktop>();
        mover = GetComponent<MoverDesktop>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            mover.Rotate(Input.GetAxis("Mouse X"));
        }

        if (Input.GetAxis("Mouse Y") != 0)
            mover.TiltHead(Input.GetAxis("Mouse Y"));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            mover.Strafe(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown("space"))
            mover.Jump();
    }

}
