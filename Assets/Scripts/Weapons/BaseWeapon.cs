using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected int ammo = 99;
    [SerializeField] protected float coolDown = 0.5f;
    private float nextShotTime;
    public BaseEntity Holder { get; set; }
    public TeamList Team { get; set; }
    public UnityEvent OnFire;
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