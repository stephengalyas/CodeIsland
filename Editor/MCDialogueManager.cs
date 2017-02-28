/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * The purpose of this class is to manage the dialogue used by the Main Character.
 * This class is responsible for reading the dialogue from an XML file and making it available to the other classes that require the use of the
 * dialogue.
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Xml;
using System.Collections;

public class MCDialogueManager : MonoBehaviour {

    /// <summary>
    /// The XML document containing the dialogue text for the main character.
    /// </summary>
    private static XmlDocument doc;

    /// <summary>
    /// An array containing the main character's introduction to the level.
    /// </summary>
    public static string[] mcIntroText = new string[10];

    /// <summary>
    /// An array containing the closing text for the level.
    /// </summary>
    public static string[] mcClosingText = new string[10];

    /// <summary>
    /// This is used to read the XML file.
    /// </summary>
    private static TextAsset xmlAsset;

    /// <summary>
    /// The dialogue manager.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// Instantiates the variables and gets the dialogue when the level is loaded.
    /// </summary>
    void Start ()
    {
        // Get the dialogue from the file.
        GetDialogue();
    }

    /// <summary>
    /// Gets the dialogue for the main character.
    /// </summary>
    public static bool GetDialogue()
    {
        // Prepare the XML data.
        doc = new XmlDocument();
        xmlAsset = (TextAsset)Resources.Load("Character_Dialogues");

        // These are used to split up the inner text in the XML file.
        string intro = String.Empty;
        string conclusion = String.Empty;

        // Load the data from the XML file.
        try
        {
            doc.LoadXml(xmlAsset.text);
        }
        catch(Exception)
        {
            EditorUtility.DisplayDialog("Cannot Read Dialogue", "It seems that we cannot load the dialogue for the characters. " +
                                        "Please exit the game and try playing the level again.", "Okay");
            return false;
        }

        // Get the introduction dialogue from the file.
        XmlNodeList introNodes = doc.SelectNodes("/parent/beginner/levelThree/MC/introduction/body");
        foreach (XmlNode node in introNodes)
        {
            intro += node.InnerText + "*";
        }
        introNodes = null; // Remove the reference to the list (memory management).

        // Get the conclusion dialogue from the file.
        XmlNodeList concNodes = doc.SelectNodes("/parent/beginner/levelThree/MC/conclusion/body");
        foreach(XmlNode node in concNodes)
        {
            conclusion += node.InnerText + "*";
        }
        concNodes = null; // Remove the reference to the list (memory management).

        // Parse the text into arrays.
        mcIntroText = intro.Split('*');
        mcClosingText = conclusion.Split('*');

        // Done getting dialogue. Return true.
        return true;
    }
}
