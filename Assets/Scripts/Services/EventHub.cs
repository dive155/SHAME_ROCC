using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EntityDeathHandler(BaseEntity killedUnit);

public class EventHub : MonoBehaviour {


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
	void Awake () {
        if (FindObjectsOfType<EventHub>().Length > 1)
            Debug.LogError("Multiple instances of EventHub (singletone) on the scene!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
