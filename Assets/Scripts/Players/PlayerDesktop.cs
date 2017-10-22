using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

        CmdSpawnGun(currentWeaponIndex);
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
        currentWeaponIndex += 1;
        currentWeaponIndex = currentWeaponIndex % weapons.Count;

        CmdSpawnGun(currentWeaponIndex);
    }
        
    [Command]
    void CmdSpawnGun(int weaponIndex)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon.gameObject);

        currentWeaponIndex = weaponIndex;
        currentWeapon = Instantiate(weapons[currentWeaponIndex], weaponSlot.position, weaponSlot.rotation);
        currentWeapon.Team = Team;
        currentWeapon.Holder = this;
        currentWeapon.transform.parent = weaponSlot;

        NetworkServer.Spawn(currentWeapon.gameObject);
        RpcSpawnGun(currentWeapon.gameObject, currentWeaponIndex);
    }

    [ClientRpc]
    void RpcSpawnGun(GameObject weapon, int weaponIndex)
    {
        currentWeaponIndex = weaponIndex;
        currentWeapon = weapon.GetComponent<BaseWeapon>();
        currentWeapon.Team = Team;
        currentWeapon.Holder = this;
        currentWeapon.transform.parent = weaponSlot;
        //reset local transform to place spawned weapon exactly into weapon slot
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}