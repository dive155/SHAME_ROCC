using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EntityDeathHandler(BaseEntity killedUnit);

public class EventHub : MonoBehaviour
{
    private static EventHub instance = null;
    private bool IAmUseless = false;

    public event EntityDeathHandler EntityDeathEvent;

    public void SignalEntityDeath(BaseEntity killedEntity)
    {
        Debug.Log("EventHub: Signaling units death");
        if (EntityDeathEvent != null)
        {
            EntityDeathEvent(killedEntity);
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("Multiple instances of EventHub (singleton) on the scene! Exterminate!!!1");
            IAmUseless = true;
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDestroy()
    {
        if (IAmUseless)
            instance = null;
    }
}