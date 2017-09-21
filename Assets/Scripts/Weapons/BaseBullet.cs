using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private float launchForce;

    protected TeamList team;
    public TeamList Team
    {
        get { return team;}
        set { team = value; }
    }

    protected BaseEntity holder;
    public BaseEntity Holder
    {
        get { return holder;}
        set { holder = value; }
    }

	void Start () 
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchForce);
	}
	
    void OnCollisionEnter (Collision col) 
    {
        SetOff(col);
        Destroy(this.gameObject);
    }

    protected virtual void SetOff (Collision col)
    {

    }
}
