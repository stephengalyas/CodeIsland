/* Team Python          Last edit made by: Stephen Galyas           Created on: December 1, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This script allows the main character to move around the level.
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControllerCave1 : MonoBehaviour {
    
    /// <summary>
    /// The fader object.
    /// </summary>
    public GameObject fader;
        
    /// <summary>
    /// The Rigidbody2D object of the Player object.
    /// </summary>
    Rigidbody2D rbody;

    /// <summary>
    /// The animator object that moves the Player object.
    /// </summary>
    private Animator anim;

    /// <summary>
    /// The dialogue manager.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// The method that is called when the script is executed.
    /// </summary>
    void Start () {
        
        // Get the Rigidbody2D and Animator objects associated with the main character.
        rbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        //control = this.GetComponent<CharacterController>();
        //control.detectCollisions = true;
	}
	
	/// <summary>
    /// This is called before rendering a frame.
    /// </summary>
	void FixedUpdate () {

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
        rbody.MovePosition(rbody.position + (movement * 2)); // Moves one meter per 60 frames, not one meter per frame.
	}

    /// <summary>
    /// This method manages what to do when the main character collides with an object.
    /// </summary>
    /// <param name="coll">The collider object.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "CaveDoor" )//&& CaveGodScript.doorUnlock == true) // The user has completed the level.
        {
            fader.GetComponent<LevelFader>().Fade(1, 0.5f);
            SceneManager.LoadScene(5); // Load the final scene.
        }

        else if(coll.gameObject.name == "OpeningCredits") // Display opening credits.
        {
            string[] intro = new string[4];
            intro[0] = "Hey! You were supposed to follow the goblin! What happened?";
            intro[1] = "It appears that the code for the controls was wrong! Every direction you pressed pointed you to the cave.";
            intro[2] = "It is important to make sure that you know what your code is doing. But how can you remind yourself of what all that complex code does?";
            intro[3] = "Maybe some time in the cave will teach you. Find the key and escape the cave. Good luck!";
            Destroy(coll.gameObject);
            dialogueManager.GetComponent<TextBoxScriptCave1>().Display(intro, false);
        }
    }
}
