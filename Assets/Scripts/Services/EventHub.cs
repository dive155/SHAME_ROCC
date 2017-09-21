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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
