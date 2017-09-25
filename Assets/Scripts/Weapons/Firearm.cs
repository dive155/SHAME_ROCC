using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firearm : BaseWeapon {

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem muzzleFlashParticle;
    [SerializeField] Text ammoCounter;
	public override void Start () 
    {
        base.Start();
        team = this.transform.root.GetComponentInChildren<BaseEntity>().Team;
	}
	
    protected override void OnPrimaryFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<BaseBullet>().Team = team;
        bullet.GetComponent<BaseBullet>().Holder = holder;
        bullet.GetComponent<Rigidbody>().velocity += this.GetComponent<Rigidbody>().velocity;
        //Physics.IgnoreCollision(this.GetComponentInParent<Collider>(), bullet.GetComponent<Collider>());
        foreach (Collider col in GetAllColliders())
        {
            Physics.IgnoreCollision(col, bullet.GetComponent<Collider>());
        }
        /*
        BaseUIManager holdersUI = Holder.GetComponent<BaseUIManager>();
        if (holdersUI != null)
        {
            holdersUI.SetShownAmmo(ammo);
        } */
        if (ammoCounter != null)
            ammoCounter.text = string.Format("{0}", ammo);
    }

    private Collider[] GetAllColliders()
    {
        return transform.root.GetComponentsInChildren<Collider>();
    }
}
