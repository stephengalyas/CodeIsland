using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.SceneManagement;

public class ForgotPassword : MonoBehaviour {

    public Texture backgroundTexture;

    public GUIStyle logo;
    public GUIStyle backgroundScroll;
    public GUIStyle forgotPassword;
    public GUIStyle cancelButton;

    public float placementXForStrings;

    public string userName = "Enter Username";
    public string email = "Enter Email";

    void OnGUI()
    {
        //Display our background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        //Background
        GUI.Label(new Rect(Screen.width * .3f, Screen.height * .1f, Screen.width * .4f, Screen.height * .9f), "", backgroundScroll);

        //Logo
        GUI.Label(new Rect(Screen.width * .29f, Screen.height * .02f, Screen.width * .43f, Screen.height * .35f), "", logo);

        //Username and email textboxes
        userName = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .44f, Screen.width * .2f, Screen.height * .08f), userName, 20);
        email = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .54f, Screen.width * .2f, Screen.height * .08f), email, 40);



        //Displays our buttons: new Rect(how far in, how far down, width of button, height of button)
        //Displays button when we have textures
        if (GUI.Button(new Rect(Screen.width * .51f, Screen.height * .82f, Screen.width * .12f, Screen.height * .1f), "", forgotPassword))
        {
            GetPassword(); // Get the password.
        }

        if (GUI.Button(new Rect(Screen.width * .37f, Screen.height * .82f, Screen.width * .12f, Screen.height * .1f), "", cancelButton))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private bool GetPassword()
    {
        // First, load the XML file and create a variable to hold the password.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("userAcctConfig");
        string password = System.String.Empty;

        try
        {
            doc.LoadXml(asset.text);
        }
        catch (System.Exception exc)
        {
            UnityEditor.EditorUtility.DisplayDialog("Cannot Access Save Data", "The program cannot access the save data. Error message: " +
                                                    exc.Message + "\n\nPlease try again later.", "Okay");
            return false; // Return to the caling method.
        }

        // Second, find the node in the file that is associated with the user account.
        XmlNodeList list = doc.SelectNodes("/config/account");
        foreach (XmlNode node in list)
        {
            if (node.Attributes["userName"].Value == userName) // We've found the account.
            {
                password = node["password"].InnerText.ToString(); // Store the password.
            }
        }

        if (System.String.IsNullOrEmpty(password)) // We did not find the password.
        {
            UnityEditor.EditorUtility.DisplayDialog("Account Does Not Exist", "That account does not exist!", "Okay");
        }
        else // The account exists.
        {
            UnityEditor.EditorUtility.DisplayDialog("Password", "The password for this account is: " + password, "Okay");
        }

        return true; // Success. Return true.
    } // Close GetPassword().
}
