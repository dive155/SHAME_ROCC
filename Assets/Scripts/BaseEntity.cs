using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamList
{
    Friendly, Hostile, Neutral
}
    
public class BaseEntity : MonoBehaviour {

    [SerializeField] protected TeamList team;
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    private bool destroyInLateUpdate;

    public TeamList Team
    {
        get { return team;}
        set { team = value; }
    }

    protected EventHub eHub;
    public EventHub EHub
    {
        get {  return eHub;}
        private set {  eHub = value; }
    }

    void Awake()
    {
        currentHealth = maxHealth;
        eHub = FindObjectOfType<EventHub>();
    }

    public virtual void Start()
    {

    }

    public void TakeDamage (float value)
    {
        currentHealth -= value;
        OnDamageTaken(value);
        if (currentHealth <= 0)
            OnDeath();
    }

    public void TakeDamage (float value, BaseEntity attacker)
    {
        TakeDamage(value);
        BaseAI brain = this.GetComponent<BaseAI>();
        if (brain != null)
        {
            //Debug.Log("got brain");
            brain.AttackedBy(attacker);
        }
    }

    protected virtual void OnDamageTaken (float value) {}

    protected virtual void OnDeath() 
    {
        eHub.SignalEntityDeath(this);
        destroyInLateUpdate = true;
    }

    void LateUpdate()
    {
        if (destroyInLateUpdate)
            Destroy(this.gameObject);
    }

}
