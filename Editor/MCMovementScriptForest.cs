/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class manages the main character's movement on the map.
 * Depending on the berries that the user interacts with, the speed of the player's movement is modified.
 * Uses ForestGodScript.
*/

using UnityEngine;
using System.Collections;

public class MCMovementScriptForest : MonoBehaviour {

    /// <summary>
    /// The Rigidbody2D object of the Main Character game object.
    /// </summary>
    private Rigidbody2D rbody;

    /// <summary>
    /// The animator object that moves the Main Character game object.
    /// </summary>
    private Animator anim;

    /// <summary>
    /// The speed that the character moves.
    /// </summary>
    private static float speed;

    /// <summary>
    /// Allows the main character's speed to be altered for a certain period of time.
    /// </summary>
    public static int timer;

    /// <summary>
    /// The method that is called when the script is executed.
    /// </summary>
    void Start()
    {
        // Get the Rigidbody2D and Animator objects associated with the main character.
        rbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        speed = 2.0f;
        timer = 0;
    }

    /// <summary>
    /// This is called at a fixed rate and moves the character in a direction chosen by the user.
    /// </summary>
    void FixedUpdate()
    {
        // Check the timer. If it is not zero, alter the speed.
        if(timer < 0) // The main character should still be slow.
        {
            timer++;
            speed = 1.5f;
        }
        else if(timer > 0) // The main character should still move fast.
        {
            timer--;
            speed = 4.0f;
        }
        else // The effects of the berry have worn off. Reset the main character's speed.
        {
            speed = 2.0f;
        }

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Create a Vector2 object that gets the horizontal and vertical input from the user.

        if (movement != Vector2.zero) // If the user is pressing a movement button on the input device.
        {
            anim.SetBool("isWalking", true); // Update the Animator object's boolean.
            anim.SetFloat("input_x", movement.x); // Set the horizontal movement value.
            anim.SetFloat("input_y", movement.y); // Set the vertical movement value.
        }
        else // If there is no movement input from the user.
        {
            anim.SetBool("isWalking", false); // Update the Animator object's boolean.
        }

        rbody.MovePosition(rbody.position + (movement * speed)); // Move the main character in the direction.
    } // Close FixedUpdate().
}
