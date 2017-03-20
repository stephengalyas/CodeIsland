/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: December 11, 2016            Published on: December 19 21, 2016
 * This class is responsible for the automatic movement of the goblin.
 * Depending on the main character's progress, the goblin will move to a different location on the level.
 * Requires GobDialogueManager, provides to GobCollisionHandler.
 * 
 * THIS CLASS IS NO LONGER USED.
*/

using UnityEngine;
using System.Collections;

public class GobMovement : MonoBehaviour {

    /// <summary>
    /// The main character object that interacts with the goblin.
    /// </summary>
    public GameObject mainChar;

    /// <summary>
    /// The location that the goblin moves to when the main character is done interacting with it.
    /// </summary>
    public GameObject autoMove;

    /// <summary>
    /// The goblin that contains this script.
    /// </summary>
    private GameObject goblin;

    /// <summary>
    /// The Animator object for the goblin.
    /// </summary>
    private Animator anim;

    /// <summary>
    /// The goblin's Rigidbody2D object.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Initializes the GameObject's attributes when the level is loaded.
    /// </summary>
    void Start()
    {
        goblin = this.gameObject;
        anim = goblin.GetComponent<Animator>();
        rb = goblin.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Depending on the progress of the main character, this decides where the goblin is to move.
    /// </summary>
    void FixedUpdate ()
    {
        // if (MCGodScriptBeach.gobAtPlayer == false) // The goblin has not reached the main character.
        // {
        //     // Keep moving the goblin toward the main character until it has reached the character's location.
        //     MCGodScriptBeach.gobAtPlayer = MoveToLocation(mainChar.transform.position.x, mainChar.transform.position.y);
        // }
		// 
        // else if (MCGodScriptBeach.dialogueBoxFinished == true) // The goblin has reached the character and is done talking to the main character.
        // {
        //     if (MCGodScriptBeach.gobOffMap == false) // The goblin has not moved off of the map.
        //     {
        //         // Make the goblin walk off the map.
        //         MCGodScriptBeach.gobOffMap = MoveToLocation(autoMove.transform.position.x, autoMove.transform.position.y);
        //     }
        // }
    }

    /// <summary>
    /// Updates the position of the goblin related to another GameObject.
    /// </summary>
    private bool MoveToLocation(float xCoor, float yCoor)
    {
        // Get the distances between the objects.
        float xDistance = goblin.transform.position.x - xCoor; // Get the horizontal distance.
        float yDistance = goblin.transform.position.y - yCoor; // Get the vertical distance.

        // Create floats for the Vector2 object that is used when moving the goblin object.
        float xAxis = 0f;
        float yAxis = 0f;

        // Update the vertical animation of the goblin. THIS IS DONE SECOND.
        if (Mathf.Abs(yDistance) > 80)
        {
            anim.SetBool("gobIsWalking", true); // Tell the goblin animator that the sprite is moving.

            if (yDistance < 0) // The goblin is below the main character.
            {
                anim.SetFloat("gob_x", 0f); // The goblin is not moving in the horizontal direction.
                anim.SetFloat("gob_y", 1f); // Update the direction that the goblin moves.
                yAxis = 1f; // This will be used when transforming the goblin.
            }
            else // The goblin is above the main character.
            {
                anim.SetFloat("gob_x", 0f); // The goblin is not moving in the horizontal direction.
                anim.SetFloat("gob_y", -1f); // Update the direction that the goblin moves.
                yAxis = -1f; // This will be used when transforming the goblin.
            }

            rb.MovePosition(rb.position + (new Vector2(xAxis, yAxis) * 2f)); // Update the goblin's position.
            return false;
        }
        // Update horizontal animation of the goblin. THIS IS DONE FIRST.
        else if (Mathf.Abs(xDistance) >= 10)
        {
            anim.SetBool("gobIsWalking", true); // Tell the goblin animator that the sprite is moving.

            if (xDistance < 0) // The goblin is to the left of the player.
            {
                anim.SetFloat("gob_x", 1f); // Update the direction that the goblin moves.
                anim.SetFloat("gob_y", 0f); // The goblin is not moving in the vertical direction.
                xAxis = 1f; // This will be used when transforming the goblin.
            }
            else // The goblin is to the right of the player.
            {
                anim.SetFloat("gob_x", -1f); // Update the direction that the goblin moves.
                anim.SetFloat("gob_y", 0f); // The goblin is not moving in the vertical direction.
                xAxis = -1f; // This will be used when transforming the goblin.
            }

            rb.MovePosition(rb.position + (new Vector2(xAxis, yAxis) * 2f)); // Move the goblin.
            return false;
        }

        else // The goblin is in its position.
        {
            anim.SetBool("gobIsWalking", false); // Tell the goblin animator that the sprite is not moving.
            rb.MovePosition(rb.position + (new Vector2(0, 0) * 2f)); // Halt the goblin.
            return true;
        }
    }
}
