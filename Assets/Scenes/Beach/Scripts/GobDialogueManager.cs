/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: December 11, 2016            Published on: December 19 21, 2016
 * The purpose of this class is to manage the dialogue used by the Goblin.
 * This class is responsible for reading the dialogue from an XML file and making it available to the other classes that require the use of the
 * dialogue.
 * Depends on GoblinGodScriptBeach, required by GobCollisionHandler.
*/

using UnityEngine;
using System;
using System.Xml;
using System.Collections;

public class GobDialogueManager : MonoBehaviour {

    /// <summary>
    /// The XML document containing the dialogue text for the goblin.
    /// </summary>
    private static XmlDocument doc;

    /// <summary>
    /// An array containing the goblin's dialogue text with the main character.
    /// </summary>
    public static string[] gobTextMC;

    /// <summary>
    /// The text that explains the purpose for decoding the goblin's message.
    /// </summary>
    public static string[] explanation;

    /// <summary>
    /// This is used to read the XML file.
    /// </summary>
    private static TextAsset xmlAsset;

    /// <summary>
    /// This will call a separate method which gets the dialogue for the goblin.
    /// </summary>
    void Start ()
    {
        // Prepare the XML data.
        doc = new XmlDocument();
        xmlAsset = (TextAsset)Resources.Load("Character_Dialogues");

        // Get the dialogue from the file.
        GetDialogue();
	}

    /// <summary>
    /// Gets the dialogue for the goblin.
    /// </summary>
    private static bool GetDialogue()
    {
        // These are used to split up the inner text in the XML file.
        string unParsed = String.Empty;
        string expUnParsed = String.Empty;

        // Load the data from the XML file.
        try
        {
            doc.LoadXml(xmlAsset.text);
        }
        catch(Exception)
        {
            Debug.Log("Cannot Read Dialogue --> It seems that we cannot load the dialogue for the characters. " +
                                       "Please exit the game and try playing the level again.");
            return false;
        }

        // Get the dialogue from the file.
        XmlNodeList gobTextNodes = doc.SelectNodes("/parent/beginner/levelOne/goblin/body");
        for (int i = 0; i < 4; i++) // Get the first set of dialogue lines.
        {
            unParsed += gobTextNodes[i].InnerText + "*";
        }

        // Store the rest of the dialogue (the explanation) into another array.
        for (int j = 4; j < 7; j++)
        {
            expUnParsed += gobTextNodes[j].InnerText + "*";
        }

        // Parse the text.
        gobTextMC = unParsed.Split('*');
        explanation = expUnParsed.Split('*');

        // Done getting dialogue. Return true.
        return true;
    }

}
