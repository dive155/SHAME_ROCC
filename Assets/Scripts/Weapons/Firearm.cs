using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Firearm : BaseWeapon
{
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem muzzleFlashParticle;
    [SerializeField] Text ammoCounter;

    public override void Start()
    {
        base.Start();
        Team = this.transform.root.GetComponentInChildren<BaseEntity>().Team;
        if (ammoCounter != null)
            ammoCounter.text = string.Format("{0}", ammo);
    }

    protected override void OnPrimaryFire()
    {
        CmdSpawnBullet();
        muzzleFlashParticle.Play();
        if (ammoCounter != null)
            ammoCounter.text = string.Format("{0}", ammo);
    }

    private Collider[] GetAllColliders()
    {
        return transform.root.GetComponentsInChildren<Collider>();
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<BaseBullet>().Team = Team;
        bullet.GetComponent<BaseBullet>().Holder = Holder;
        bullet.GetComponent<Rigidbody>().velocity += this.GetComponent<Rigidbody>().velocity;

        foreach (Collider col in GetAllColliders())
        {
            Physics.IgnoreCollision(col, bullet.GetComponent<Collider>());
        }
        
        NetworkServer.Spawn(bullet);
    }
}