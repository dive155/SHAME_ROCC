using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : BaseWeapon {

    [SerializeField] Light lightSource;
    private bool switchedOn = true;

    protected override void OnPrimaryFire()
    {
        ammo = 99;
        switchedOn = !switchedOn;
        lightSource.enabled = switchedOn;
    }
}
