using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;
using System.IO;
using System.Collections;

/// <summary>
/// This class is used globally to show/hide dialogue boxes and to populate the boxes with text.
/// </summary>
public class TextBoxScriptCave3Key : MonoBehaviour
{
    //-------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The XML file containing the text.
    /// </summary>
    private XmlDocument scriptConfig;

    private string xmlPath;

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
    private string userAnswer = "_____! ______ __.";

    /// <summary>
    /// The answer to the problem.
    /// </summary>
    private string answer = "5fg4j685fg5u7x";

    /// <summary>
    /// The sound that is played when the user loads the next line of text.
    /// </summary>
    public AudioSource clickSound;

    private TextAsset xmlAsset;
    //[---------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        scriptConfig = new XmlDocument();
        //DirectoryInfo path = new DirectoryInfo(Directory.GetCurrentDirectory()); // The current directory of the project.
        //xmlPath = path.FullName + @"/Assets/Configs/Character_Dialogues.xml"; // The path to the config file.
        xmlAsset = (TextAsset)Resources.Load("Character_Dialogues");
        scriptConfig.LoadXml(xmlAsset.text);

        string unParsed = String.Empty;

        // Get the text.
        XmlNodeList nodes = scriptConfig.SelectNodes("/parent/beginner/levelTwoCave1/chest/body");
        foreach (XmlNode node in nodes)
        {
            unParsed += node.InnerText + '*';
        }

        textLines = unParsed.Split('*');

        theText.text = String.Empty;
        textBox.SetActive(false);
        currLine = 0;
        inputField.image.enabled = false; // Hide the textbox.
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
            if (currLine == 0) // Puzzle time!
            {
                theText.text = textLines[currLine];
                //userAnswer = GUI.TextField(new Rect(new Vector2(410f, -628f), new Vector2(200, 50)), userAnswer);

                userAnswer = inputField.text;
                if(userAnswer == answer) // The answer is correct.
                {
                    inputField.image.enabled = false; // Hide the textbox.
                    inputField.text = String.Empty; // Remove the text.
                    inputField.caretWidth = 0; // Hide the caret.
                    currLine = 1;
                }
            }

            else // Keep preparing for the puzzle.
            {
                if (currLine <= textLines.Length-2) // There are still lines to be displayed.
                {
                    theText.text = textLines[currLine]; // Update the text in the textbox.

                    if (Input.GetKey(KeyCode.Space)) // The user wants to get the next line.
                    {
                        //waves.Pause(); // Pause waves sound.
                        clickSound.Play(); // Play the dialogue sound.
                        //waves.UnPause(); // Resume the waves.

                        currLine++; // Increase the index for the current line of text to be displayed.
                        System.Threading.Thread.Sleep(50); // Pause the game for 200 milliseconds so the user does not accidently
                                                           // skip over any text.
                    } // Close if(space button is pressed)
                } // Close if(more lines to be displayed)

                else
                {
                    textBox.SetActive(false);
                    currLine = 0;
                    textLines = new string[0];
                    //BeachGodScript.dialogueBoxFinished = true;
                    Destroy(this); // Remove this script.
                }
            }
        } // Close else block.
    } // Close Update().

    /// <summary>
    /// Displays the dialogue box text associated with a game object.
    /// </summary>
    /// <param name="textLines">The text to be displayed.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            this.currLine = 0; // Set the counter to the beginning of the text.
            textBox.SetActive(true); // Display the textbox.
            theText.text = this.textLines[0]; // Display the first line of text.

            inputField.image.enabled = true; // Show the textbox.
        }
    }
}
