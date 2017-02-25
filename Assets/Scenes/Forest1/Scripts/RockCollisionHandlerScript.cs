/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class manages the collision events with the main character.
 * Depending on the direction the user is moving, the object moves in the executing direction.
*/

using UnityEngine;
using System.Collections;

public class RockCollisionHandlerScript : MonoBehaviour {

    /// <summary>
    /// Used to tell the user if they are not able to move the object.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// The transform object on the rock.
    /// </summary>
    private Transform trans;
    
    /// <summary>
    /// The Rigidbody2D object of the Rock game object.
    /// </summary>
    private Rigidbody2D rbody;

    /// <summary>
    /// Get the RigidBody2D object associated with the rock when the level loads.
    /// </summary>
    void Start ()
    {
        rbody = this.gameObject.GetComponent<Rigidbody2D>();
        trans = this.gameObject.GetComponent<Transform>();       	
	}

    /// <summary>
    /// As the main character keeps colliding with the rock, move it.
    /// </summary>
    /// <param name="coll">The collider.</param>
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && MCGodScriptForest.raspberryEnabled == false) // The player is colliding with the rock but not big enough to move it.
        {
            string[] message = { "You need special powers to move this rock!." };
            dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
        }

        else if (coll.gameObject.name == "Player" && MCGodScriptForest.raspberryEnabled == true) // The player is colliding with the rock and big enough to move it.
        {
            rbody.MovePosition(rbody.position + (new Vector2(2f, 0f))); // Move the rock.
        }

        else
        {
            // Do nothing.
        }
    }
}
