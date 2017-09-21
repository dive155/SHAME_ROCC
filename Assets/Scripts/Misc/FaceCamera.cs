using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    [SerializeField] Transform lookAt;
	// Update is called once per frame
	void Update () {
        transform.LookAt(lookAt.position);
	}
}
