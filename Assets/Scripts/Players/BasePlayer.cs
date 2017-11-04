using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BasePlayer : BaseEntity {

    [SerializeField] private Slider healthBar;
    protected Vector3 startingPosition;

    public override void Start()
    {
        base.Start();
        startingPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log("changing healthbar");
        healthBar.value = currentHealth / maxHealth; 
    }

    [ClientRpc]
    protected override void RpcOnDeath()
    {
        //EHub.SignalEntityDeath(this);
        currentHealth = maxHealth;
        transform.position = startingPosition;
    }

}
