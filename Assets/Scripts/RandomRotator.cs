using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        // angle brackets <> means that this is a template method where you can define your own parameters
        // function usually pass primitive type but with templates, you can even pass classes
        _rigidbody = GetComponent<Rigidbody>();

        // angular velocity which will allow our asteroid to rotate around another axis vs. linear velocity
        // insideUnitSphere returns a Vector3 whereas insideUnitCircle returns a Vector2
        _rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
}
