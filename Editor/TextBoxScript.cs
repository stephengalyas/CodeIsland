using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

/// <summary>
/// This class is used globally to show/hide dialogue boxes and to populate the boxes with text.
/// </summary>
public class TextBoxScript : MonoBehaviour {
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
    /// The last index in the array of strings to be displayed.
    /// </summary>
    private int endLine;

    /// <summary>
    /// The lines of text to be displayed.
    /// </summary>
    public string[] textLines;

    /// <summary>
    /// The sound that is played when the user loads the next line of text.
    /// </summary>
    public AudioSource clickSound;

    /// <summary>
    /// The waves sound.
    /// </summary>
    public AudioSource waves;
    //[---------------------------------------------------------------------------------------------------------------------------

    // Use this for initialization
    public void Start () {

        theText.text = String.Empty;
        textBox.SetActive(false);
        currLine = 0;
        textLines = new string[0];

        // Get the files to be read.
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
            if (currLine <= endLine) // There are still lines to be displayed.
            {
                theText.text = textLines[currLine]; // Update the text in the textbox.

                if (Input.GetKey(KeyCode.Space)) // The user wants to get the next line.
                {
                    waves.Pause(); // Pause waves sound.
                    clickSound.Play(); // Play the dialogue sound.
                    waves.UnPause(); // Resume the waves.

                    currLine++; // Increase the index for the current line of text to be displayed.
                    System.Threading.Thread.Sleep(50); // Pause the game for 200 milliseconds so the user does not accidently
                                                        // skip over any text.
                } // Close if(space button is pressed)
            } // Close if(more lines to be displayed)

            else if(currLine > endLine) // No more lines to be displayed.
            {
                textBox.SetActive(false);
                currLine = 0;
                endLine = 0;
                textLines = new string[0];
            }
            else
            {
                // Do nothing.
            }
        } // Close else block.
    } // Close Update().

    /// <summary>
    /// Displays the dialogue box text associated with a game object.
    /// </summary>
    /// <param name="textLines">The text to be displayed.</param>
    public void Display(string[] textLines, int startLine, int endLine)
    {
        this.textLines = null; // Remove all references to the previous set of text that was displayed.
        this.textLines = textLines; // Save a reference to the text to be displayed.
        this.currLine = startLine; // Set the counter to the beginning of the text.
        this.endLine = endLine; // Set the counter to the beginning of the text.

        textBox.SetActive(true); // Display the textbox.
        theText.text = this.textLines[0]; // Display the first line of text.
    }
}
