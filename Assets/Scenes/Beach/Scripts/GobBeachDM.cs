/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: March 17, 2017            Published on: N/A
 * The purpose of this class is to manage the dialogue used by the Goblin on the beach.
 * This class is responsible for reading the dialogue from an XML file and making it available to the other classes that require the use of the
 * dialogue.
*/

using UnityEngine;
using System;
using System.Xml;
using System.Collections;

/// <summary>
/// The dialogue manager for the goblin on the beach.
/// </summary>
public class GobBeachDM: MonoBehaviour {

    /// <summary>
    /// The XML document containing the dialogue text for the goblin.
    /// </summary>
    private XmlDocument doc;

    /// <summary>
    /// The dialogue to be displayed when the player first interacts with the goblin.
    /// </summary>
	public static string[] first_visit;

    /// <summary>
    /// The dialogue to be displayed after the player has already interacted with the goblin.
    /// </summary>
    public static string[] other_visit;

    /// <summary>
    /// This is used to read the XML file.
    /// </summary>
    private TextAsset xmlAsset;

    /// <summary>
    /// This will call a separate method which gets the dialogue for the goblin.
    /// </summary>
    void Start ()
    {
        // Prepare the XML data.
        doc = new XmlDocument();
        xmlAsset = (TextAsset)Resources.Load("dia_gob_beach");

        // Get the dialogue from the file.
        GetDialogue();
	}

    /// <summary>
    /// Gets the dialogue for the goblin.
    /// </summary>
    private bool GetDialogue()
    {
        // These are used to split up the inner text in the XML file.
        string first_RAW = String.Empty;
        string other_RAW = String.Empty;

        // Load the data from the XML file.
        try
        {
            doc.LoadXml(xmlAsset.text);
        }
        catch(Exception exc)
        {
            Debug.Log("Cannot Read Dialogue --> It seems that we cannot load the dialogue for the characters. " +
                                       "Please exit the game and try playing the level again.");
			Debug.Log(exc.Message);
			Debug.Log(exc.StackTrace);
            return false;
        }

        // Get the dialogue for the first visit.
        XmlNodeList firstVisitNodes = doc.SelectNodes("/goblin/novisit/body");
		foreach(XmlNode node in firstVisitNodes)
        {
			first_RAW += node.InnerText + "*";
        }

        // Get the dialogue for the second visit.
		XmlNodeList otherVisitNodes = doc.SelectNodes("/goblin/nextvisit/body");
		foreach (XmlNode nodeB in otherVisitNodes)
		{
			other_RAW += nodeB.InnerText + "*";
		}

        // Parse the text.
        first_visit = first_RAW.Split('*');
        other_visit = other_RAW.Split('*');

        // Done getting dialogue. Lose reference to XML file and return true.
		doc = null; // Allows the Garbage Collector to free up space.
        return true;
    }

}
