using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSpawnPoint : MonoBehaviour {

    Vector3 originalPosition;
    Quaternion originalRotation;
	// Use this for initialization
    [SerializeField] Transform theParent;
    [SerializeField] Vector3 rotate;
    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

	void Start () {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = theParent.position;
        transform.rotation = theParent.rotation;
        transform.Rotate(rotate);
	}
}
