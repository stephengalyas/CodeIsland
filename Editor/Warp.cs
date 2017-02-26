using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

    /// <summary>
    /// The position that the Player will warp to.
    /// </summary>
    public Transform warpTarget;

    /// <summary>
    /// Determines what to do when a collision occurs.
    /// </summary>
    /// <param name="other">The object that collides with the object that contains this trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("An object collided."); // Print the message to the console.

        other.gameObject.transform.position = warpTarget.position; // Adjust the player's position to the new location.

        Camera.main.transform.position = warpTarget.position; // Point the camera to the warped target.
    }

}
