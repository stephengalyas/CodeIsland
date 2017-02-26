/* Team Python          Last edit made by: Stephen Galyas           Created on: November 7, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This is the dialogue manager for the Beach level.
 * All dialogue to be displayed by all NPCs uses this class.
*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

/// <summary>
/// This class is used globally to show/hide dialogue boxes and to populate the boxes with text.
/// </summary>
public class TextBoxScriptBeach : MonoBehaviour
{
    //-------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The textbox that will display the information.
    /// </summary>
    public GameObject textBox;

    /// <summary>
    /// The textbox to get user input.
    /// </summary>
    public InputField inputField;

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
    /// The user's answer.
    /// </summary>
    private string userAnswer = String.Empty;

    /// <summary>
    /// The answer to the puzzle.
    /// </summary>
    private string answer = "hello! follow me.";

    /// <summary>
    /// The sound that is played when the user loads the next line of text.
    /// </summary>
    public AudioSource clickSound;

    /// <summary>
    /// The waves sound.
    /// </summary>
    public AudioSource waves;

    /// <summary>
    /// This value is used to determine if the user needs to input data (to solve a puzzle).
    /// </summary>
    private bool userInput;

    /// <summary>
    /// Used to prevent the user from accidently iterating through the dialogue.
    /// </summary>
    private int timer;
    //[---------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the variables.
    /// </summary>
    public void Start()
    {
        theText.text = String.Empty;
        textBox.SetActive(false); // Hides the textbox.
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
        if (textLines.Length == 0) // Empty list.
        {
            // Do nothing.
        }

        else // There is text to be displayed.
        {
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

            else // Keep preparing for the puzzle.
            {
                if (currLine < textLines.Length-1) // There are still lines to be displayed.
                {
                    theText.text = textLines[currLine]; // Update the text in the textbox.

                    if (Input.GetKey(KeyCode.Space)) // The user wants to get the next line and the timer has paused the user's input.
                    {
                        timer = 0; // Reset the timer.
                        waves.Pause(); // Pause waves sound.
                        clickSound.Play(); // Play the dialogue sound.
                        waves.UnPause(); // Resume the waves.

                        currLine++; // Increase the index for the current line of text to be displayed.
                        System.Threading.Thread.Sleep(50); // Pause the game for 200 milliseconds so the user does not accidently
                                                           // skip over any text.
                    } // Close if(space button is pressed)
                    else
                    {
                        timer++; // Keep increasing the timer.
                    }
                } // Close if(more lines to be displayed)

                else // Done displaying dialogue.
                {
                    textBox.SetActive(false);
                    currLine = 0;
                    textLines = new string[0];
                    MCGodScriptBeach.dialogueBoxFinished = true;
                }
            }
        } // Close else block.
    } // Close Update().

    /// <summary>
    /// Displays the dialogue box text associated with a game object.
    /// </summary>
    /// <param name="textLines">The text to be displayed.</param>
    public void Display(string[] textLines, bool userInput)
    {
        this.textLines = null; // Remove all references to the previous set of text that was displayed.
        this.textLines = textLines; // Save a reference to the text to be displayed.
        this.currLine = 0; // Set the counter to the beginning of the text.

        textBox.SetActive(true); // Display the textbox.
        theText.text = this.textLines[0]; // Display the first line of text.

        this.userInput = userInput;
    }
}
