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
            Debug.LogWarningFormat("Multiple instances of {0} (singleton) on the scene (objects {1}, {2})! Exterminate!!!1", 
                                    this.GetType(), instance.gameObject.name, gameObject.name);
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
        if (!IAmUseless)
            instance = null;
    }
}