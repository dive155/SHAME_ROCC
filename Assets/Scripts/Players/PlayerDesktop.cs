using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesktop : BasePlayer {

    [SerializeField] private List<BaseWeapon> weapons;
    [SerializeField] private Transform weaponSlot;

    private BaseWeapon currentWeapon;

    void Start()
    {
        foreach (var current in weapons)
        {
            current.Team = team;
            current.Holder = this;
        }
        currentWeapon = Instantiate(weapons[0], weaponSlot.position, weaponSlot.rotation);
        currentWeapon.transform.parent = weaponSlot;
    }

    public void FireGun()
    {
        if (currentWeapon != null)
            currentWeapon.Fire();
    }

}
