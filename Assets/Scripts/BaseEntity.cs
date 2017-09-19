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

    public TeamList Team
    {
        get { return team;}
        set { team = value; }
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
            brain.AttackedBy(attacker);
        }
    }

    protected virtual void OnDamageTaken (float value) {}

    protected virtual void OnDeath() {}

}
