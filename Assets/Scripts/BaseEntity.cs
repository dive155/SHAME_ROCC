using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum TeamList
{
    Friendly, Hostile, Neutral
}

public class BaseEntity : NetworkBehaviour
{
    [SerializeField] private TeamList team;
    public TeamList Team
    { 
        get { return team; }
        set { team = value; } 
    }
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    private bool destroyInLateUpdate;

    public EventHub EHub { get; private set; }

    void Awake()
    {
        currentHealth = maxHealth;
        EHub = FindObjectOfType<EventHub>();
    }

    public virtual void Start()
    {
    }

    protected virtual void Update()
    {

    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        OnDamageTaken(value);

        Debug.Log(string.Format("{0} took {1} damage. Remaining health = {2}", gameObject, value, currentHealth));

        if (currentHealth <= 0)
            OnDeath();
    }

    public void TakeDamage(float value, BaseEntity attacker)
    {
        TakeDamage(value);
        BaseAI brain = this.GetComponent<BaseAI>();
        if (brain != null)
        {
            //Debug.Log("got brain");
            brain.AttackedBy(attacker);
        }
    }

    protected virtual void OnDamageTaken(float value) { }

    protected virtual void OnDeath()
    {
        EHub.SignalEntityDeath(this);
        destroyInLateUpdate = true;
    }

    void LateUpdate()
    {
        if (destroyInLateUpdate)
            Destroy(this.gameObject);
    }
}