/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: December 11, 2016            Published on: December 19 21, 2016
 * This class is responsible for handling collisions involving the goblin.
 * Depending on the location, different events occur.
 * Requires GobDialogueManager.
*/

using UnityEngine;
using System.Collections;

public class GobCollisionHandler : MonoBehaviour {

    //--------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The goblin game object.
    /// </summary>
    public GameObject goblin;

    /// <summary>
    /// The animator associated with the goblin game object.
    /// </summary>
    private Animator anim;

    /// <summary>
    /// The dialogue box manager.
    /// </summary>
    public GameObject dialogueBox;
    //--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Instantiates the game objects when the level is loaded.
    /// </summary>
    void Start()
    {
        anim = goblin.GetComponent<Animator>(); // Get the animator component.
    }

    /// <summary>
    /// Manages the goblin's collisions.
    /// </summary>
    /// <param name="coll">The collider object that the goblin collides with.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
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
    }
}
