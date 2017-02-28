using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text theText;

    public TextAsset textFiles;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    //public PlayerController player;

	// Use this for initialization
	void Start ()
    {
        //player = FindObjectOfType<PlayerController>();

        if(textFiles != null)
            textLines = textFiles.text.Split('\n');    

        if(endAtLine == 0)
            endAtLine = textLines.Length - 1;

	}
    
    /// <summary>
    /// Updates the script as spacebar/backspace are pressed. We NEED to get this to a good spot. Big bug
    /// </summary>
    void Update ()
    {
        if(currentLine <= endAtLine)
            theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Space)) { //This is a hack. IndexOutOfRangeException needs to be handled
            if(currentLine != endAtLine + 1)
                currentLine++; 
                                
        }
        else if (Input.GetKeyDown(KeyCode.Backspace)){
            if (currentLine != 0)
                currentLine--;
        }

        if (currentLine == endAtLine + 1)
        {
            textBox.SetActive(false);
            theText.text = "";
        } else
        {
            textBox.SetActive(true);
        }
    }
	
}
