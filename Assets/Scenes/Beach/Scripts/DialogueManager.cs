/* Team Python          Last edit made by: Stephen Galyas           Created on: November 7, 2016         Last modified on: March 17, 2017            Published on: N/A
 * This class is used to display character interactions in a textbox.
 * All dialogue to be displayed by any NPC uses this class.
*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

/// <summary>
/// This class is used globally to show/hide spoken interactions with NPCs via a textbox at the bottom of the screen.
/// </summary>
public class DialogueManager : MonoBehaviour
{
	//------------------------ Variables needed to display text ------------------------------------
    /// <summary>
    /// The textbox that will display the information.
    /// </summary>
    public GameObject panel;

    /// <summary>
    /// The text to display the information.
    /// </summary>
    public Text dispText;

    /// <summary>
    /// The current line of text being displayed.
    /// </summary>
    private int currLine;

    /// <summary>
    /// The lines of text to be displayed.
    /// </summary>
    public string[] textLines;

    /// <summary>
    /// This value is used to determine if the user needs to input data (to solve a puzzle).
    /// </summary>
    private bool userInput;

    /// <summary>
    /// Used to prevent the user from accidently iterating through the dialogue.
    /// </summary>
    private int timer;
	//------------------------------------------------------------------------------------------------

	//------------------------ Variables needed to get user input ------------------------------------
	/// <summary>
	/// The textbox to get user input.
	/// </summary>
	public InputField inputField;

	/// <summary>
	/// The user's answer.
	/// </summary>
	private string userAnswer = String.Empty;

	/// <summary>
	/// The answer to the puzzle.
	/// </summary>
	private string answer = String.Empty;
	//------------------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the variables.
    /// </summary>
    public void Start()
    {
        dispText.text = String.Empty;
        panel.SetActive(false); // Hides the textbox.
        currLine = 0;
        textLines = new string[0];
        inputField.image.enabled = false; // Hide the textbox.
        userInput = false;
        timer = 0;
    }

    /// <summary>
    /// Check for user input (at a fixed rate).
    /// </summary>
    void FixedUpdate()
    {
        if (textLines.Length != 0) // There is text to be displayed.
        {
			if (currLine < textLines.Length-1) // There are still lines to be displayed.
			{
				dispText.text = textLines[currLine]; // Update the text in the textbox.

				if (Input.GetKey(KeyCode.Space) && timer >= 50) // The user wants to get the next line and the timer has paused the user's input.
				{
					timer = 0; // Reset the timer.
					currLine++; // Increase the index for the current line of text to be displayed.
				} // Close if(space button is pressed)
				else // We still need to catch accidental user input.
				{
					if(timer < 50)
					{
						timer++; // Keep increasing the timer.
					}
				}
			} // Close if(more lines to be displayed)

			else // Done displaying dialogue.
			{
				panel.SetActive(false);
				currLine = 0;
				textLines = new string[0];
				// MCGodScriptBeach.dialogueBoxFinished = true; --> OLD CODE.
				PlayerGod.canMove = true; // Allow the player to move again.
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

        panel.SetActive(true); // Display the textbox.
        dispText.text = this.textLines[0]; // Display the first line of text.

        this.userInput = userInput;
    }

	/******************* OLD CODE **************************
			if (currLine == 2 && userInput == true) // Puzzle time!
            {
                inputField.image.enabled = true; // Show the textbox.

                theText.text = textLines[0] + "\n\n" + textLines[2];

                userAnswer = inputField.text;
                if(userAnswer.ToLower() == answer) // The answer is correct.
                {
                    inputField.image.enabled = false; // Hide the textbox.
                    inputField.text = String.Empty; // Remove the text.
                    inputField.caretWidth = 0; // Hide the caret.
                    currLine = 3;
                    userInput = false;
                }
			}
	 ********************************************************/
}
