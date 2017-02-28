/* Team Python          Last edit made by: Stephen Galyas           Created on: December 1, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This script allows the main character to be warped to a different position on the Cave level.
*/

using UnityEngine;
using System.Collections;

public class WarpCave1 : MonoBehaviour {

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
        other.gameObject.transform.position = warpTarget.position; // Adjust the player's position to the new location.
        Camera.main.transform.position = warpTarget.position; // Point the camera to the warped target.
    }

}
