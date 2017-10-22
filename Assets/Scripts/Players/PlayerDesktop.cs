using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesktop : BasePlayer
{
    [SerializeField] private List<BaseWeapon> weapons;
    [SerializeField] private Transform weaponSlot;
    //private DesktopUIManager uiManager;


    private BaseWeapon currentWeapon;
    private int currentWeaponIndex = 0;

    public override void Start()
    {
        base.Start();

        if (isLocalPlayer)
        {
            SetLayerRecursively(transform.Find("CameraBase/Head/HeadModel").gameObject, Layer.OwnedBody);
            SetLayerRecursively(transform.Find("Body").gameObject, Layer.OwnedBody);
            SetLayerRecursively(transform.Find("CameraBase/Canvas").gameObject, Layer.OwnedUI); 
        }

        SpawnGun();
    }

    public void FireGun()
    {
        if (currentWeapon != null)
            currentWeapon.Fire();
    }

    protected override void OnDamageTaken(float value)
    {
        //uiManager.SetShownHealth(currentHealth);
    }

    public void SwitchGun()
    {
        if (currentWeapon != null)
            Destroy(currentWeapon.gameObject);

        currentWeaponIndex += 1;
        currentWeaponIndex = currentWeaponIndex % weapons.Count;

        SpawnGun();
    }
        
    void SpawnGun()
    {
        currentWeapon = Instantiate(weapons[currentWeaponIndex], weaponSlot.position, weaponSlot.rotation);
        currentWeapon.transform.parent = weaponSlot;
        currentWeapon.Holder = this;
        currentWeapon.Team = Team;
    }
}