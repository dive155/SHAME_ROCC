using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {

    [SerializeField] protected int ammo = 100;
    [SerializeField] protected float coolDown = 0.5f;
    private float nextShotTime;
    protected BaseEntity holder;
    public BaseEntity Holder
    {
        get { return holder;}
        set { holder = value; }
    }

    protected TeamList team;
    public TeamList Team
    {
        get { return team;}
        set { team = value; }
    }

    public virtual void Start()
    {
        nextShotTime = Time.time;
    }

    public void Fire()
    {
        if (Time.time > nextShotTime && ammo > 0)
        {
            nextShotTime = Time.time + coolDown;
            ammo--;
            OnPrimaryFire();
        }
    }

    protected virtual void OnPrimaryFire()
    {

    }
}
