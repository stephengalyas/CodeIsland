/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: March 17, 2017            Published on: N/A
 * This class is responsible for handling collisions involving the goblin.
 * Depending on the location, different events occur.
 * Requires GobDialogueManager.
*/

using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the collision events for the Goblin on the island.
/// </summary>
public class GobBeachCollHand : MonoBehaviour {

    //--------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The goblin game object.
    /// </summary>
    public GameObject goblin;

    /// <summary>
    /// The dialogue box manager.
    /// </summary>
    public GameObject dialogueBox;

	/// <summary>
	/// A counter used to catch accidental user input.
	/// </summary>
	private int timer = 50;

	/// <summary>
	/// Tracks if the user is able to interact with the goblin.
	/// </summary>
	private bool collided = false;
	//--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Instantiates the game objects when the level is loaded.
    /// </summary>
    void Start()
    {
        // Does nothing.
    }

	/// <summary>
	/// When the player collides with the goblin, allow an interaction to begin.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerEnter2D(Collider2D coll)
	{
		collided = true;
	}

    /// <summary>
    /// Waits for the user to begin the interaction.
    /// </summary>
    /// <param name="coll">The collider object that the goblin collides with.</param>
    void OnTriggerStay2D(Collider2D coll)
    {
		if (coll.gameObject.name == "Player" && Input.GetKey (KeyCode.Space) && collided) { // The user wishes to speak to the goblin.

			// Don't allow the player to move until the NPC is done interacting with the main character.
			PlayerGod.canMove = false;

			DialogueManager dm = dialogueBox.GetComponent<DialogueManager> ();

			if (GobBeachGod.visited == false) {
				dm.Display (GobBeachDM.first_visit);
				GobBeachGod.visited = true; // Update the tracker.
			} else {
				dm.Display (GobBeachDM.other_visit);
			}

			collided = false; // The dialogue is displaye. Deactivate the dialogue manager instantiation until the user re-enters the collision.
		}
	} // Close OnTriggerStay2D().

		/*********************** OLD CODE ********************************
		// The goblin has collided with the main character.
        if (coll.gameObject.name == "Player")
        {
            anim.SetBool("gobIsWalking", false); // Tell the goblin animator that the sprite is not moving.
            TextBoxScriptBeach display = dialogueBox.GetComponent<TextBoxScriptBeach>();
            display.Display(GobDialogueManager.gobTextMC, true);
        }

        // The goblin has collided with the "AutoStop" object.
        else if (coll.gameObject.name == "AutoStop") // Goblin is off the screen.
        {
            anim.SetBool("gobIsWalking", false); // Tell the goblin animator that the sprite is not moving.
            TextBoxScriptBeach display = dialogueBox.GetComponent<TextBoxScriptBeach>();
            display.Display(GobDialogueManager.explanation, false); // Display the explanation text.
        }
		******************************************************************/
}
