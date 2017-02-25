/* Team Python          Last edit made by: Stephen Galyas           Created on: December 1, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This script manages the dialogue to be displayed if the main character tries to advance in the level without completing a step.
*/

using System;
using UnityEngine;
using System.Collections;

public class BoundaryTextScriptCave1 : MonoBehaviour {

    /// <summary>
    /// The dialogue manager.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// Enables the dialogue if the main character collides with the boundary.
    /// </summary>
    /// <param name="coll">The collider.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Player") // The player cannot advance.
        {
            string[] message = { "Hmm. You seem to have missed something . . .\n\n Read the instrustions on the wall before continuing." };
            dialogueManager.GetComponent<TextBoxScriptCave1>().Display(message);
        }
    }

    /// <summary>
    /// When the main character leaves the collision, remove the dialogue.
    /// </summary>
    /// <param name="coll">The collider.</param>
    void OnTriggerExit2D(Collider2D coll)
    {
        dialogueManager.GetComponent<TextBoxScriptCave1>().textBox.SetActive(false);
        dialogueManager.GetComponent<TextBoxScriptCave1>().theText.text = String.Empty;
    }
}
