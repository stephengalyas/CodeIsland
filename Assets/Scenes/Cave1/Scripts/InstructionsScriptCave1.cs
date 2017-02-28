/* Team Python          Last edit made by: Stephen Galyas           Created on: December 1, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This script displays the introduction note to the player on the Cave level.
*/

using UnityEngine;
using System;
using System.Xml;
using System.IO;
using System.Collections;

public class InstructionsScriptCave1 : MonoBehaviour {
    /// <summary>
    /// The dialogue manager.
    /// </summary>
    public GameObject dialogueManager;

    /// <summary>
    /// A collider.
    /// </summary>
    public GameObject dirtCollider;

    /// <summary>
    /// The XML file containing the text.
    /// </summary>
    private XmlDocument scriptConfig;

    /// <summary>
    /// The path of the XML file.
    /// </summary>
    private string xmlPath;

    /// <summary>
    /// The dialogue to be displayed.
    /// </summary>
    private string[] dialogue;

    /// <summary>
    /// This is needed to read the XML fie.
    /// </summary>
    private TextAsset xmlAsset;

    /// <summary>
    /// Gets the dialogue from the XML file.
    /// </summary>
    void Start()
    {
        scriptConfig = new XmlDocument();
        xmlAsset = (TextAsset)Resources.Load("Character_Dialogues");
        scriptConfig.LoadXml(xmlAsset.text);

        string unParsed = String.Empty;

        // Get the text.
        XmlNodeList nodes = scriptConfig.SelectNodes("/parent/beginner/levelTwoCave1/instructions/body");
        foreach(XmlNode node in nodes)
        {
            unParsed += node.InnerText + "*";
        }

        dialogue = unParsed.Split('*');
    }

    /// <summary>
    /// Manages the main character's collision with the colliders at the beginning of the level.
    /// </summary>
    /// <param name="coll">The collider.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            CaveGodScript.torchCount = 20; // Update the torch count.

            // Let them continue on in the level.
            Destroy(dirtCollider.GetComponent<BoxCollider2D>());
            Destroy(dirtCollider.GetComponent<EdgeCollider2D>());

            dialogueManager.GetComponent<TextBoxScriptCave1>().Display(dialogue, true);
        }
    }

    /// <summary>
    /// When the main character leaves the collision, hide the dialogue.
    /// </summary>
    /// <param name="coll">The collider.</param>
    void OnTriggerExit2D(Collider2D coll)
    {
        dialogueManager.GetComponent<TextBoxScriptCave1>().theText.text = String.Empty;
        dialogueManager.GetComponent<TextBoxScriptCave1>().textBox.SetActive(false);
    }
}
