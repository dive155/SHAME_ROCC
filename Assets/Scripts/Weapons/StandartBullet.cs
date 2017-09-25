using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBullet : BaseBullet
{
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem hitParticle;

    protected override void SetOff(Collision col)
    {
        //Debug.Log(col.collider.gameObject);
        BaseEntity entityHit = col.collider.gameObject.GetComponent<BaseEntity>();
        if (entityHit != null)
        {
            if (entityHit.Team != Team)
            {
                entityHit.TakeDamage(damage);
            }
        }
        Instantiate(hitParticle, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));

        BaseEntity target = col.collider.gameObject.GetComponentInParent<BaseEntity>();
        if (target != null)
            target.TakeDamage(damage, Holder);
    }
}