using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Firearm : BaseWeapon
{
    [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public GameObject bulletPrefab;
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
        var spawnManager = transform.root.GetComponentInChildren<SpawnManager>();
        spawnManager.CmdSpawnBullet(this.gameObject);
        muzzleFlashParticle.Play();
        if (ammoCounter != null)
            ammoCounter.text = string.Format("{0}", ammo);
    }

    public Collider[] GetAllColliders()
    {
        return transform.root.GetComponentsInChildren<Collider>();
    }


}