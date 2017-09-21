using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : BaseAI {

	public enum FrogAiStates
	{
		Patroling, AttackNear, AttackFar, Seeking
	}

	FrogAiStates currentState = FrogAiStates.Patroling;
	
	[SerializeField] private Transform patrolingCenter;
	private Vector3 currentTarget;
	[SerializeField] private float patrolingRadius;
    [SerializeField] private Transform head;
	
	private BaseEntity currentTargetEntity;
	private FrogMover mover;
	
	void Start()
	{
		mover = this.GetComponent<FrogMover>();
        currentTarget = patrolingCenter.position;
	}
	

	void Update ()
	{
        //Debug.Log(currentState);
		switch (currentState)
        {
			case (FrogAiStates.Patroling):
				PatrolingState();
				break;
			case (FrogAiStates.AttackNear):
				AttackNearState();
				break;
			case (FrogAiStates.AttackFar):
				AttackFarState();
				break;
			case (FrogAiStates.Seeking):
				SeekingState();
				break;
        }
	}
	
	void PatrolingState()
	{
		Vector3 frogPosition = new Vector3 (transform.position.x, 0, transform.position.z);
		Vector3 targetPosition = new Vector3 (currentTarget.x, 0, currentTarget.z);
		float distanceToTarget = Vector3.Distance(frogPosition, targetPosition);
		
		if (distanceToTarget > 3)
		{
            mover.Aggressive = false;
            mover.LookAtTarget(currentTarget);
			mover.JumpTowards();
		}
		else
		{
			currentTarget = GetPointInCircle(patrolingCenter.position, patrolingRadius);
		}
	}
	
	void AttackNearState()
	{
		mover.Aggressive = true;
		mover.LookAtTarget();
		mover.JumpTowards();
        if (!IsInvoking())
            InvokeRepeating("CheckTargetVisible", 0.0f, 0.5f);
	}
	
	void AttackFarState()
	{
		
	}
	
	void SeekingState()
	{
		
	}
	
	void OnTriggerEnter (Collider col)
    {
		BaseEntity other = col.gameObject.GetComponent<BaseEntity>();
		if (other == null)
			return;
        if (other == this.GetComponent<BaseEntity>())
            return;
		else
		{
			if (other.Team != this.GetComponent<BaseEntity>().Team)
			{
				currentState = FrogAiStates.AttackNear;
				mover.CurrentTarget = other.transform;
				currentTargetEntity = other;
			}
			
		}
	}
	
	void OnTriggerExit (Collider col)
	{
		if (currentTargetEntity == null)
			return;
		BaseEntity other = col.gameObject.GetComponent<BaseEntity>();
		if (other == null)
			return;
        if (other != currentTargetEntity)
            return;
		currentTargetEntity = null;
		currentState = FrogAiStates.Patroling;
	}
	
    //private Vector3 vfrom, vto;

	void CheckTargetVisible()
	{
        if (currentTargetEntity == null)
        {
            CancelInvoke();
            currentState = FrogAiStates.Patroling;
            return;
        }

		RaycastHit hit;
        //if (Physics.Raycast(head.position, Quaternion.LookRotation(), out hit, 100))
        if (Physics.Linecast(head.position, currentTargetEntity.transform.position, out hit))
		{
            //vfrom = head.position;
            //vto = hit.point;
            //Debug.Log(string.Format("Raycast hit {0}", hit.collider.gameObject));
            BaseEntity other = hit.collider.gameObject.GetComponent<BaseEntity>();
            if (other != currentTargetEntity && other != this.GetComponent<BaseEntity>())
			{
				currentTargetEntity = null;
				currentState = FrogAiStates.Patroling;
                CancelInvoke();
			}
		}
	}
	
	public override void AttackedBy(BaseEntity attacker)
    {

    }
	
	public Vector3 GetPointInCircle(Vector3 center, float radius)
    {
        float x, z;
        do
        {
            x = Random.Range(center.x - radius, center.x + radius);
            z = Random.Range(center.z - radius, center.z + radius);
        }
        while ((x-center.x)*(x-center.x) + (z-center.z)*(z-center.z) >= radius*radius);
        //Debug.Log(string.Format("{0} units need a circle with radius {1}. Giving coordinates {2}.", numberOfUnits, circleRadius, new Vector3(x, center.y, z)));
        return new Vector3(x, center.y, z);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(patrolingCenter.position, patrolingRadius);
        Gizmos.DrawLine(transform.position, currentTarget);
       
    }
	
	
}
