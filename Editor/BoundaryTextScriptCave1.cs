using System;
using UnityEngine;
using System.Collections;

public class BoundaryTextScriptCave1 : MonoBehaviour {

    public GameObject dialogueManager;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Player") // The player cannot advance.
        {
            string[] message = { "Hmm. You seem to have missed something . . .\n\n Read the instrustions on the wall before continuing." };
            dialogueManager.GetComponent<TextBoxScriptCave1>().Display(message);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        dialogueManager.GetComponent<TextBoxScriptCave1>().textBox.SetActive(false);
        dialogueManager.GetComponent<TextBoxScriptCave1>().theText.text = String.Empty;
    }
}
