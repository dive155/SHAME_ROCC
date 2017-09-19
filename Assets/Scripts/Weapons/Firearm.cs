using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : BaseWeapon {

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem muzzleFlashParticle;

	public override void Start () 
    {
        base.Start();
        team = this.GetComponentInParent<BaseEntity>().Team;
	}
	
    protected override void OnPrimaryFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<BaseBullet>().Team = team;
        bullet.GetComponent<BaseBullet>().Holder = holder;
        bullet.GetComponent<Rigidbody>().velocity += this.GetComponent<Rigidbody>().velocity;
        Physics.IgnoreCollision(this.GetComponentInParent<Collider>(), bullet.GetComponent<Collider>());
    }
}
