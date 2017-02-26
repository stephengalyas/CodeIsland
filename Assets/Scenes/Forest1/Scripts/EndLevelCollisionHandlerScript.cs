/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class manages the end of level collision on the Forest level.
 * If the main character has not eaten the appropriate berry, the guard will spot it and tell it to go away.
 * This dialogue is also managed here (in order to condense classes).
 * Once the main character eats the berry, the collider associated with this game object will disappear.
*/

using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Xml;

public class EndLevelCollisionHandlerScript : MonoBehaviour
{
    /// <summary>
    /// The dialogue manager for the level.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// The XML document containing the dialogue text for the collider (aka the guard) at the end of the level.
    /// </summary>
    private static XmlDocument doc;

    /// <summary>
    /// An array containing the collider's (aka the guard's) warning to the main character.
    /// </summary>
    public static string[] guardText;

    /// <summary>
    /// This is used to read the XML file.
    /// </summary>
    private static TextAsset xmlAsset;


    /// <summary>
    /// Instantiates the variables and gets the dialogue when the level is loaded.
    /// </summary>
    void Start()
    {
        doc = new XmlDocument();
        xmlAsset = (TextAsset)Resources.Load("Character_Dialogues");
        doc.LoadXml(xmlAsset.text);

        GetDialogue(); // Get the dialogue from the file.
    }

    /// <summary>
    /// This checks if the main character has eaten the mushroom. If so, destroy this object.
    /// </summary>
    void FixedUpdate()
    {
        if(MCGodScriptForest.mushroomEnabled == true)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Gets the dialogue for the main character.
    /// </summary>
    private static bool GetDialogue()
    {
        // These are used to split up the inner text in the XML file.
        string text = String.Empty;

        // Load the data from the XML file.
        try
        {
            doc.LoadXml(xmlAsset.text);
        }
        catch (Exception)
        {
           // EditorUtility.DisplayDialog("Cannot Read Dialogue", "It seems that we cannot load the dialogue for the characters. " +
                                       // "Please exit the game and try playing the level again.", "Okay");
            return false;
        }

        // Get the introduction dialogue from the file.
        XmlNodeList guardNodes = doc.SelectNodes("/parent/beginner/levelThree/guard/endOfLevel/body");
        foreach (XmlNode node in guardNodes)
        {
            text += node.InnerText + "*";
        }
        guardNodes = null; // Remove the reference to the list (memory management).

        // Parse the text into arrays.
        guardText = text.Split('*');

        // Done getting dialogue. Return true.
        return true;
    }

    /// <summary>
    /// When the main character collides with the collider, determine if it is visible. If so, display a warning.
    /// </summary>
    /// <param name="coll">The collider.</param>
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && MCGodScriptForest.mushroomEnabled == false) // The player is hidden and can escape the level.
        {
            dialogueManager.GetComponent<TextBoxScriptForest>().Display(guardText); // Display the warning.
        }
    }
}
