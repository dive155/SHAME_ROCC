using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float launchForce;

    public TeamList Team { get; set; }

    public BaseEntity Holder { get; set; }

    public UnityEvent OnSetOff;

    [SerializeField] private GameObject soundHolderToSpawn;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchForce);
    }

    void OnCollisionEnter(Collision col)
    {
        SetOff(col);
        OnSetOff.Invoke();
        if (soundHolderToSpawn != null)
        {
            Instantiate(soundHolderToSpawn, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }

    protected virtual void SetOff(Collision col)
    {
    }
}