using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

/// <summary>
/// This class is used to explain to the user the importance of decoding the goblin's message.
/// </summary>
public class TextBoxScriptCave1 : MonoBehaviour {
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
    /// The textbox to display the torch count.
    /// </summary>
    public GameObject torchTextBox;

    /// <summary>
    /// The torch count text.
    /// </summary>
    public Text torchText;

    /// <summary>
    /// The torch image.
    /// </summary>
    public Image torchImage;

    /// <summary>
    /// A reference to the key image.
    /// </summary>
    public Image keyImage;

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
    /// Prevents the user from accidently clicking through dialogue.
    /// </summary>
    private int timer;

    private bool visible = false;
    //[---------------------------------------------------------------------------------------------------------------------------

    // Use this for initialization
    public void Start () {

        theText.text = String.Empty;
        textBox.SetActive(false);
        currLine = 0;
        textLines = new string[0];


        keyImage.color = new Color(255f, 255f, 255f, 0f);
        torchTextBox.SetActive(false);
        torchText.text = String.Empty;
        torchImage.color = new Color(255f, 255f, 255f, 0f);

        timer = 0;
	}

    /// <summary>
    /// Check for user input (at a fixed rate).
    /// </summary>
    void FixedUpdate()
    {
        if (visible == true)
        {
            torchText.text = "x" + CaveGodScript.torchCount;
        }

        if(CaveGodScript.doorUnlock == true) // We have obtained the key.
        {
            // Hide torch image.
            torchImage.color = new Color(255f, 255f, 255f, 0f);

            // Show key image.
            keyImage.color = new Color(255f, 255f, 255f, 255f);
            CaveGodScript.torchCount = 1;
        }

        if (textLines.Length == 0) // Empty list.
        {
            // Do nothing.
        }

        else // There is text to be displayed.
        {
            if (currLine <= endLine) // There are still lines to be displayed.
            {
                theText.text = textLines[currLine]; // Update the text in the textbox.

                if (Input.GetKey(KeyCode.Space) && timer >= 60) // The user wants to get the next line.
                {
                    timer = 0;
                    //waves.Pause(); // Pause waves sound.
                    clickSound.Play(); // Play the dialogue sound.
                    //waves.UnPause(); // Resume the waves.

                    currLine++; // Increase the index for the current line of text to be displayed.
                    System.Threading.Thread.Sleep(50); // Pause the game for 200 milliseconds so the user does not accidently
                                                        // skip over any text.
                } // Close if(space button is pressed)
                else
                {
                    timer++;
                }
            } // Close if(more lines to be displayed)

            else // No more lines to be displayed.
            {
                textBox.SetActive(false);
                currLine = 0;
                endLine = 0;
                textLines = new string[0];
            }
        } // Close else block.
    } // Close Update().

    /// <summary>
    /// Displays the dialogue box text associated with a game object.
    /// </summary>
    /// <param name="textLines">The text to be displayed.</param>
     public void Display(string[] textLines, bool torchDisplay = false)
    {
        if(torchDisplay == true)
        {
            torchTextBox.SetActive(true);
            torchImage.color = new Color(255f, 255f, 255f, 255f);
            visible = true;
        }

        this.textLines = null; // Remove all references to the previous set of text that was displayed.
        this.textLines = textLines; // Save a reference to the text to be displayed.
        this.currLine = 0; // Set the counter to the beginning of the text.
        this.endLine = textLines.Length-1;

        textBox.SetActive(true); // Display the textbox.
        theText.text = this.textLines[0]; // Display the first line of text.
    }
}
