using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;

    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        // If we can find this object:
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        } else
        {
            Debug.LogError("Game Controller Object not found.");
        }
    }

    // OnTriggerEnter(other) triggers when the object collides with other's Collider component
    private void OnTriggerEnter(Collider other)
    {
        // Check if we are colliding with the boundary of the game
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player" && playerExplosion != null)
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);

        // Otherwise, destroy the other game object
        Destroy(other.gameObject);
        Destroy(gameObject); // equivalent to Destroy(this.gameObject); Unity just does it for us
    }
}
