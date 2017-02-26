using UnityEngine;
using System;
using System.Xml;
using System.IO;
using System.Collections;

public class InstructionsScriptCave1 : MonoBehaviour {

    public GameObject dialogueManager;
    public GameObject dirtCollider;

    /// <summary>
    /// The XML file containing the text.
    /// </summary>
    private XmlDocument scriptConfig;

    private string xmlPath;

    private string[] dialogue;

    private TextAsset xmlAsset;
    void Start()
    {
        scriptConfig = new XmlDocument();
        //DirectoryInfo path = new DirectoryInfo(Directory.GetCurrentDirectory()); // The current directory of the project.
        //xmlPath = path.FullName + @"/Assets/Configs/Character_Dialogues.xml"; // The path to the config file.
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

    void OnTriggerExit2D(Collider2D coll)
    {
        dialogueManager.GetComponent<TextBoxScriptCave1>().theText.text = String.Empty;
        dialogueManager.GetComponent<TextBoxScriptCave1>().textBox.SetActive(false);
    }
}
