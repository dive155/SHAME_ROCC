using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : BaseEntity
{
    [SerializeField] ParticleSystem deathParticle;

    protected override void RpcOnDeath()
    {
        Instantiate(deathParticle, transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(this.gameObject);
        base.RpcOnDeath();
    }
}