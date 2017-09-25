using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtferTime : MonoBehaviour
{
    [SerializeField] private float lifetime;

    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
}