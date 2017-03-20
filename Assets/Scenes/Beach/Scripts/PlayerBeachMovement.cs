/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: March 17, 2017           Published on: N/A
 * This class handles the movement of the main character.
 * The update method uses booleans to check if the character will move via user input or a special predefined path.
 * Uses the MCGodScriptBeach class.
*/

using UnityEngine;
using System.Collections;

public class PlayerBeachMovement : MonoBehaviour {

    //---------------------------------------------------------------Game object components-------------------------------------------------------------------
    /// <summary>
    /// The Rigidbody2D object of the main character.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// The animator object that moves the main character.
    /// </summary>
    private Animator anim;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Instantiates the objects needed to move the character.
    /// </summary>
    void Start()
    {
        // Get the Rigidbody2D and Animator objects associated with the main character.
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }
        
    /// <summary>
    /// This method is called at a fixed rate and contains the code to move the character.
    /// </summary>
    void FixedUpdate ()
    {
		if (PlayerGod.canMove == true) {
			MoveToLocation (); // Move the character.
		}
    }

	/// <summary>
	/// Default code to move the character.
	/// </summary>
	private bool MoveToLocation()
	{
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

		//control.Move(new Vector3(movement.x, movement.y, 0) * 1.5f);
		rb.MovePosition(rb.position + (movement * 2)); // Moves one meter per 60 frames, not one meter per frame.

		return true;
	} // Close MoveToLocation().

	/// <summary>
    /// Moves the main character to a particular location on the screen.
    /// </summary>
    /// <param name="xCoor">The x-coordinate that the character is to move to.</param>
    /// <param name="yCoor">The y-coordinate that the character is to move to.</param>
    /// <returns>True if the character has reached the destination. Otherwise, returns false.</returns>
    private bool MoveToLocation(float xCoor, float yCoor)
    {
        // Get the distances between the objects.
        float xDistance = this.gameObject.transform.position.x - xCoor; // Get the horizontal distance.
        float yDistance = this.gameObject.transform.position.y - yCoor; // Get the vertical distance.

        // Create floats for the Vector2 object that is used when moving the goblin object.
        float xAxis = 0f;
        float yAxis = 0f;

        // Update horizontal animation of the main character. THIS IS DONE FIRST.
        if (Mathf.Abs(xDistance) >= 10)
        {
            anim.SetBool("isWalking", true); // Tell the main character animator that the sprite is moving.

            if (xDistance < 0) // The main characer is to the left of the object.
            {
                anim.SetFloat("input_x", 1f); // Update the direction that the main character moves.
                anim.SetFloat("input_y", 0f); // The main character is not moving in the vertical direction.
                xAxis = 1f; // This will be used when transforming the main character.
            }
            else // The main character is to the right of the player.
            {
                anim.SetFloat("input_x", -1f); // Update the direction that the main character moves.
                anim.SetFloat("input_y", 0f); // The main character is not moving in the vertical direction.
                xAxis = -1f; // This will be used when transforming the main character.
            }

            rb.MovePosition(rb.position + (new Vector2(xAxis, yAxis) * 2f)); // Move the main character.
            return false;
        }

        // Update the vertical animation of the main character. THIS IS DONE SECOND.
        else if (Mathf.Abs(yDistance) > 10)
        {
            anim.SetBool("isWalking", true); // Tell the main character animator that the sprite is moving.

            if (yDistance < 0) // The main character is below the main character.
            {
                anim.SetFloat("input_x", 0f); // The main character is not moving in the horizontal direction.
                anim.SetFloat("input_y", 1f); // Update the direction that the main character moves.
                yAxis = 1f; // This will be used when transforming the main character.
            }
            else // The main character is above the main character.
            {
                anim.SetFloat("input_x", 0f); // The main character is not moving in the horizontal direction.
                anim.SetFloat("input_y", -1f); // Update the direction that the main character moves.
                yAxis = -1f; // This will be used when transforming the main character.
            }

            rb.MovePosition(rb.position + (new Vector2(xAxis, yAxis) * 2f)); // Update the main character's position.
            return false;
        }
        else // The main character is in its position.
        {
            anim.SetBool("isWalking", false); // Tell the main character animator that the sprite is not moving.
            rb.MovePosition(rb.position + (new Vector2(0, 0) * 2f)); // Halt the main character.
            return true;
        }
    }
}
