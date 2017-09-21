using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Awake () 
    {
        if (FindObjectsOfType<SceneManager>().Length > 1)
            Debug.LogError("Multiple instances of SceneManager (singletone) on the scene!");

        Cursor.visible = false;
	}

}
