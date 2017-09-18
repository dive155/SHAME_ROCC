using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMover : BaseMover {

    private Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] Transform head;
    [SerializeField] private Transform currentTarget;
    float moveTime = 4;
    float moveStarted;
    float nextMove;
    float jumpCooldown = 3f;
    float nextJump;

    [SerializeField] Animator headAnimator;
    [SerializeField] Animator bodyAnimator;

    bool aggressive = true;

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
        rb = this.GetComponent<Rigidbody>();
        nextMove = Time.time;
        nextJump = Time.time;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), this.GetComponentsInChildren<Collider>()[1]);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown("n"))
        {
            Jump();
        }

        LookAtTarget(currentTarget);
        JumpTowards(currentTarget);
        if (Input.GetKey("r"))
        {
            BodyMatchHead();
        }
	}


    public void JumpTowards(Transform target)
    {
        if (Time.time > nextMove)
        {
            nextMove = Time.time + moveTime;
            moveStarted = Time.time;
        }
        if (Time.time > moveStarted + 1 && Time.time < moveStarted + 2)
        {
            BodyMatchHead();
        }
        if (Time.time > moveStarted + 2 && LooksAtTarget(currentTarget))
        {
            Jump();
        }

    }

    public void Jump()
    {   
        if (Time.time > nextJump)
        {
            nextJump = Time.time + jumpCooldown;
            Vector3 forward = head.forward;
            forward.y = 0;
            forward = forward.normalized;
            rb.AddForce((forward + Vector3.up * 0.9f).normalized * jumpForce);
            bodyAnimator.SetTrigger("Jump");
            if (aggressive)
                Bite();
        }
    }

    public bool LookAtTarget (Transform lookTarget) 
    {
        float rotSpeed = 360f; 
        Vector3 D = lookTarget.transform.position - head.transform.position;  
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), rotSpeed * Time.deltaTime);
        head.rotation = rot;

        float minRotation = -80;
        float maxRotation = 80;
        Vector3 currentRotation = head.localRotation.eulerAngles;
        if (currentRotation.y < 180)
        {
            currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        }
        else
        {
            currentRotation.y = Mathf.Clamp(currentRotation.y, 360+minRotation, 360);
        }
        head.localRotation = Quaternion.Euler (currentRotation);

        return LooksAtTarget(lookTarget);
    }

    public void BodyMatchHead()
    {
        float rotSpeed = 4f; 
        Quaternion rot = Quaternion.Slerp(transform.rotation, head.rotation, rotSpeed * Time.deltaTime);
        //rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
    }

    public bool LooksAtTarget (Transform lookTarget)
    {
        Quaternion targetRotation = Quaternion.LookRotation (lookTarget.transform.position - head.position); 
        Quaternion currentRotation = head.rotation;
        float rotatingError = Mathf.Abs (targetRotation.eulerAngles.y - currentRotation.eulerAngles.y);
        if (rotatingError < 5)
            return true;
        else
            return false;
    }

    void OnCollisionEnter ()
    {
        headAnimator.SetTrigger("Close");
    }
        
    void Bite ()
    {
        Debug.Log("Trying to bite");
        if (Physics.Raycast(head.position, head.forward, 8.0f))
        {
            Debug.Log("Got raycast");
            int c = Random.Range(0, 2);
            switch (c)
            {
                case 0:
                    headAnimator.SetTrigger("Open1");
                    break;
                case 1:
                    headAnimator.SetTrigger("Open2");
                    break;
            }
        }
    }
}
