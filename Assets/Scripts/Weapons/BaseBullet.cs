using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float launchForce;

    public TeamList Team { get; set; }

    public BaseEntity Holder { get; set; }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchForce);
    }

    void OnCollisionEnter(Collision col)
    {
        SetOff(col);
        Destroy(this.gameObject);
    }

    protected virtual void SetOff(Collision col)
    {
    }
}