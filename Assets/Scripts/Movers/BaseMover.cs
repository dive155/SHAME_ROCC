using UnityEngine;
using UnityEngine.Networking;

public class BaseMover : NetworkBehaviour
{
    protected Rigidbody objectToMove;

    public virtual void Start()
    {
        objectToMove = GetComponent<Rigidbody>();
    }
}