/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class manages the collisions that the Main Character makes with the Wall.
 * If the character has not eaten the strawberry, it will not be allowed to move through the opening.
*/

using UnityEngine;
using System.Collections;

public class WallCollisionHandlerScript : MonoBehaviour {

    /// <summary>
    /// Used to tell the user if they are not able to move the object.
    /// </summary>
    public GameObject dialogueManager;
	
    /// <summary>
    /// Manages what happens when the user collides with the opening.
    /// </summary>
    /// <param name="coll">The collider.</param>
	void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("Entrance");
        if(coll.gameObject.name == "Player" && MCGodScriptForest.strawberryEnabled == false) // The main character has not ate the berry.
        {
            string[] message = { "You need special powers to shrink so you can make it through this opening!." };
            dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
        }
    }
}
