/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class handles the dialogue to be displayed for the Forest level.
*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

/// <summary>
/// This class is used throughout the Forest level to show/hide dialogue boxes and to populate the boxes with text.
/// </summary>
public class TextBoxScriptForest : MonoBehaviour
{
    //-------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The textbox that will display the information.
    /// </summary>
    public GameObject textBox;

    /// <summary>
    /// The text to contain the information.
    /// </summary>
    public Text theText;

    /// <summary>
    /// The current line of text being displayed.
    /// </summary>
    private int currLine;

    /// <summary>
    /// The lines of text to be displayed.
    /// </summary>
    public string[] textLines;

    /// <summary>
    /// The sound that is played when the user loads the next line of text.
    /// </summary>
    public AudioSource clickSound;

    /// <summary>
    /// The backgound music on the level.
    /// </summary>
    public AudioSource bkgdMusic;

    /// <summary>
    /// This is used to prevent the user from accidently skipping over text.
    /// </summary>
    private int timer;
    //[---------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the variables for the level.
    /// </summary>
    public void Start()
    {
        timer = 0; // Set the timer to its default value.
        theText.text = String.Empty;
        textBox.SetActive(false);
        currLine = 0; // Default value.
        textLines = new string[0];
    }


    /// <summary>
    /// Check for user input (at a fixed rate).
    /// </summary>
    void FixedUpdate()
    {
        if (textLines.Length == 0) // Empty list.
        {
            // Do nothing.
        }

        else // There is text to be displayed.
        {
            if (currLine < textLines.Length-1) // There are still lines to be displayed.
            {
                theText.text = textLines[currLine]; // Update the text in the textbox.

                if (Input.GetKey(KeyCode.Space) && timer >= 60) // The user wants to get the next line and we have paused the game so the user does not skip over any text.
                {
                    timer = 0; // Reset the timer.
                    bkgdMusic.Pause(); // Pause waves sound.
                    clickSound.Play(); // Play the dialogue sound.
                    bkgdMusic.UnPause(); // Resume the waves.

                    currLine++; // Increase the index for the current line of text to be displayed.
                } // Close if(space button is pressed)
                else
                {
                    timer++; // Increase the timer.
                }
            } // Close if(more lines to be displayed)

            else // Done displaying dialogue.
            {
                textBox.SetActive(false);
                currLine = 0;
                textLines = new string[0];
            }
        } // Close else block.
    } // Close Update().

    /// <summary>
    /// Displays the dialogue box text associated with a game object.
    /// </summary>
    /// <param name="textLines">The text to be displayed.</param>
    public void Display(string[] textLines)
    {
        this.textLines = null; // Remove all references to the previous set of text that was displayed.
        this.textLines = textLines; // Save a reference to the text to be displayed.
        this.currLine = 0; // Set the counter to the beginning of the text.

        textBox.SetActive(true); // Display the textbox.
        theText.text = this.textLines[0]; // Display the first line of text.
    }
}
