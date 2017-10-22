using UnityEngine;

public class BaseMover : MonoBehaviour
{
    protected Rigidbody objectToMove;

    public virtual void Start()
    {
        objectToMove = GetComponent<Rigidbody>();
    }
}