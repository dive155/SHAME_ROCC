﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesktop : BasePlayer {

    [SerializeField] private List<BaseWeapon> weapons;
    [SerializeField] private Transform weaponSlot;
    private DesktopUIManager uiManager;

    private BaseWeapon currentWeapon;

    public override void Start()
    {
        base.Start();
        uiManager = this.GetComponent<DesktopUIManager>();
        currentWeapon = Instantiate(weapons[0], weaponSlot.position, weaponSlot.rotation);
        currentWeapon.transform.parent = weaponSlot;
        currentWeapon.Holder = this;
        currentWeapon.Team = team;
    }

    public void FireGun()
    {
        if (currentWeapon != null)
            currentWeapon.Fire();
    }

    protected override void OnDamageTaken (float value)
    {
        uiManager.SetShownHealth(currentHealth);
    }

}
