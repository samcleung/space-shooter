using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        // Unity tells you whether this component is null automatically
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.velocity = transform.forward * speed;
    }
}






