using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    new BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();

        transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        float xScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        float zScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;

        collider.size = new Vector3(2.0f * xScale, collider.size.y, 2.0f * zScale);
    }

    // Trigger colliders are for non-physical related game logic
    private void OnTriggerExit(Collider other)
    {
        // As objects leave the boundary, destroy them
        Destroy(other.gameObject);
    }
}