/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class handles the Main Character's collisions with the berries.
 * Requires MCMovementScriptForest and MCGodScriptForest.
*/

using UnityEngine;
using System.Collections;

public class MCCollisionHandlerScriptForest : MonoBehaviour {

    /// <summary>
    /// This stops the user from continuing until they eat the berry.
    /// </summary>
    public GameObject invisibleCollider;

    /// <summary>
    /// This stops the user from ending the level until they eat the mushroom.
    /// </summary>
    public GameObject endInvisibleCollider;
    
    /// <summary>
    /// This is used to display information about berries.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// The player object.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The Transform object of the main character.
    /// </summary>
    private Transform mcSize;

    /// <summary>
    /// Used to determine if we need to shrink the main character.
    /// </summary>
    public static bool shrinkOne = false;

    /// <summary>
    /// Used to determine if we need to shrink the main character back to its original size.
    /// </summary>
    public static bool shrinkTwo = false;

    /// <summary>
    /// Used to determine if we need to enlarge the main character back to its regular size.
    /// </summary>
    public static bool growOne = false;
    
    /// <summary>
    /// Used to determine if we need to enlarge the main character to a very large size.
    /// </summary>
    public static bool growTwo = false;

    /// <summary>
    /// Gets the main character's Transform object when the level is loaded.
    /// </summary>
    void Start()
    {
        mcSize = this.gameObject.GetComponent<Transform>();
    }

    /// <summary>
    /// This is called once per frame 
    /// </summary>
    void FixedUpdate()
    {
        if (shrinkOne == true) // We want to keep shrinking the player.
        {
            if (mcSize.localScale.x > 180 && mcSize.localScale.y > 208) // We want to shrink the size of the player.
            {
                mcSize.localScale = new Vector3(mcSize.localScale.x - 5, mcSize.localScale.y - 5, mcSize.localScale.z);
            }
            else // Stop shrinking and reset values (for growing).
            {
                shrinkOne = false;
            }
        } // Close if(shrinking == true).

        else if(growOne == true) // We want to enlarge the main character back to its original size.
        {
            if(mcSize.localScale.x < 360 && mcSize.localScale.y < 416) // We want to continue enlarging the character.
            {
                mcSize.localScale = new Vector3(mcSize.localScale.x + 5, mcSize.localScale.y + 5, mcSize.localScale.z);
            }
            else // The main character is back to its normal size. Stop enlarging the object.
            {
                growOne = false;
            }
        } // Close if(growing == true).

        else if(shrinkTwo == true) // We want to shrink the main character back to its normal size.
        {
            if(mcSize.localScale.x > 360 && mcSize.localScale.y > 416) // We want to continue shrinking the character.
            {
                mcSize.localScale = new Vector3(mcSize.localScale.x - 5, mcSize.localScale.y - 5, mcSize.localScale.z);
            }
            else // We have reached the character's normal size. Stop shrinking.
            {
                shrinkTwo = false;
            }
        } // Close if(shrinkTwo == true).

        else if(growTwo == true) // We want to enlarge the main character back to twice its normal size.
        {
            if(mcSize.localScale.x < 720 && mcSize.localScale.y < 832) // Keep enlarging the character.
            {
                mcSize.localScale = new Vector3(mcSize.localScale.x + 5, mcSize.localScale.y + 5, mcSize.localScale.z);
            }
            else // We have reached the desire size. Stop enlarging the character.
            {
                growTwo = false;
            }
        } // Close if(growTwo == true);

        else
        {
            // Do nothing (maintain the same size).
        }
    } // Close Update().

    /// <summary>
    /// This method is called once per frame when the character is colliding with an object.
    /// </summary>
    /// <param name="coll">The collider object.</param>
	void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("Collided!");
        if (coll.gameObject.name == "strawberry") // The main character is colliding with the blueberry.
        {
            if (Input.GetKey(KeyCode.Space)) // The main character wants to pick up the berry.
            {
                // Shrink the size of the player.
                shrinkOne = true;
                MCGodScriptForest.strawberryEnabled = true;
                Destroy(coll.gameObject); // Destroy the berry.
            }
            else if (Input.GetKey(KeyCode.I)) // The main character wants to get information about the berry.
            {
                string[] message = { "This berry will help you get through tight spaces." };
                dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
            }
        }
        else if (coll.gameObject.name == "regrow") // Done moving through the small space. Regrow the character.
        {
            growOne = true;
            Destroy(coll.gameObject); // Destroy the berry.
        }
        else if (coll.gameObject.name == "raspberry") // The main character needs to be enlarged to push a large object.
        {
            if (Input.GetKey(KeyCode.Space)) // The main character wants to pick up the berry.
            {
                growTwo = true;
                MCGodScriptForest.raspberryEnabled = true;
                Destroy(invisibleCollider);
                Destroy(coll.gameObject); // Destroy the berry.
            }
            else if (Input.GetKey(KeyCode.I)) // The main character wants to get information about the berry.
            {
                string[] message = { "This berry will help you move large objects." };
                dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
            }
        }
        else if (coll.gameObject.name == "reshrink") // Done moving the large object. Reshrink the character.
        {
            shrinkTwo = true;
            Destroy(coll.gameObject); // Destroy the berry.
        }
        else if (coll.gameObject.name == "cherry") // The main character's speed needs to be cut in half.
        {
            if (Input.GetKey(KeyCode.Space)) // The main character wants to pick up the berry.
            {
                MCGodScriptForest.cherryEnabled = true;
                MCMovementScriptForest.timer = -300;
                Destroy(coll.gameObject); // Destroy the berry.
            }
            else if (Input.GetKey(KeyCode.I)) // The main character wants to get information about the berry.
            {
                string[] message = { "This berry will decrease your speed for a few seconds." };
                dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
            }
        }
        else if (coll.gameObject.name == "blueberry") // The main character's speed needs to be doubled.
        {
            if (Input.GetKey(KeyCode.Space)) // The main character wants to pick up the berry.
            {
                MCGodScriptForest.blueberryEnabled = true;
                MCMovementScriptForest.timer = 300;
                Destroy(coll.gameObject); // Destroy the berry.
            }
            else if (Input.GetKey(KeyCode.I)) // The main character wants to get information about the berry.
            {
                string[] message = { "This berry will increase your speed for a few seconds." };
                dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
            }
        }
        else if (coll.gameObject.name == "mushroom") // The main character needs to be invisible.
        {
            if (Input.GetKey(KeyCode.Space)) // The main character wants to pick up the berry.
            {
                MCGodScriptForest.mushroomEnabled = true;
                Destroy(coll.gameObject); // Destroy the berry.
                Destroy(endInvisibleCollider); // Let the user be able to exit the level.
                player.GetComponent<Renderer>().sortingOrder = 0; // Draw the player at the bottom of the map to make it appear as if they are invisible.
            }
            else if (Input.GetKey(KeyCode.I)) // The main character wants to get information about the berry.
            {
                string[] message = { "This will make you invisible. (Don't be afraid to keep moving!" };
                dialogueManager.GetComponent<TextBoxScriptForest>().Display(message); // Display the message.
            }
        }

        else if(coll.gameObject.name == "OpeningCredits") // Display the intro to the level.
        {
            Destroy(coll.gameObject);
            dialogueManager.GetComponent<TextBoxScriptForest>().Display(MCDialogueManager.mcIntroText); // Display the closing message.
        }
    } // Close OnTriggerStay().

    /// <summary>
    /// Manages the final collision, which should only display the final text as soon as the main character hits the collider.
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "EndLvlCollider") // The main character has collided with the final object (ON LAYER 0).
        {
            dialogueManager.GetComponent<TextBoxScriptForest>().Display(MCDialogueManager.mcClosingText); // Display the closing message.
        }
    }
}
