using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    // other classes can't access this PRIVATE variable
    private new Rigidbody rigidbody;
    private float nextValidFireTime;
    private AudioSource audioSource;

    // Awake is the first method that gets called on all existing game objects
    private void Awake()
    {
        // GetComponent: gets the component with the name specified (check if it's null after)
        // will return the first instance of the specified component
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (!rigidbody)
        {
            // can stylize this log with CSS!
            Debug.LogError("Missing Rigidbody");
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextValidFireTime)
        {
            nextValidFireTime = Time.time + fireRate;
            // creates GameObjects for us (while the game is running!)
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            audioSource.Play();
        }
    }

    // FixedUpdate is "fixed", runs physics at a fixed rate (normalizes fps across different devices)
    private void FixedUpdate()
    {
        // can map "Horizontal" to any keystroke you want in Unity
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // constructor -> instantiating moveVector as Vector3
        Vector3 moveVector = new Vector3(moveHorizontal, 0, moveVertical);

        rigidbody.velocity = moveVector * speed;
        
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        // note: top left of screen starts at 0,0 because graphics cards render pixels from top left to bottom right
        Vector3 clampedScreenSpacePosition
            = new Vector3(Mathf.Clamp(playerPosition.x, 0, Screen.width), Mathf.Clamp(playerPosition.y, 0, Screen.height), playerPosition.z);

        Vector3 clampedWorldSpacePosition = Camera.main.ScreenToWorldPoint(clampedScreenSpacePosition);

        rigidbody.position = new Vector3(clampedWorldSpacePosition.x, 0, clampedWorldSpacePosition.z);
        // Quaternions in Unity represent rotations: four-dimensional constructs that rotate absolutely
        rigidbody.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.x * -tilt);

    }

    // Start is the first frame in which the game object is active

}
