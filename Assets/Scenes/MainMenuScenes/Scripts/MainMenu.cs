/* Team Python          Last edit made by: Stephen Galyas           Created on: November 7, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class creates and manages the user's interaction with the Main Menu.
 * If the user logs in with a valid username and password, the last non-completed level is loaded.
*/

///<summary>
///The class that manages the system's main menu.
///</summary>
using UnityEngine;
//using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Xml;

public class MainMenu : MonoBehaviour
{

    //-----------------------------------------------------GUI Elements----------------------------------------------------
    /// <summary>
    /// The texture of the Main Menu window.
    /// </summary>
    public Texture backgroundTexture;

    /// <summary>
    /// The logo on the main menu.
    /// </summary>
    public GUIStyle logo;

    /// <summary>
    /// The background style of the main menu.
    /// </summary>
    public GUIStyle backgroundScroll;

    /// <summary>
    /// The login button on the main menu.
    /// </summary>
    public GUIStyle login;

    /// <summary>
    /// The signup button on the main menu.
    /// </summary>
    public GUIStyle signup;

    /// <summary>
    /// The forgot password button on the main menu.
    /// </summary>
    public GUIStyle forgotPassword;

    /// <summary>
    /// The placement value for the strings on the GUI.
    /// </summary>
    public float placementXForStrings;

    /// <summary>
    /// The username.
    /// </summary>
    public string userName = "Username";

    /// <summary>
    /// The password.
    /// </summary>
    public string password = "Password";

    /// <summary>
    /// Error Message Thrown and Error Style
    /// </summary>
    public string errorMessage = "";
    public GUIStyle errorStyle = new GUIStyle();
    //---------------------------------------------------------------------------------------------------------------------
        
    /// <summary>
    /// This method is called several times per frame and updates the GUI display of the Main Menu.
    /// </summary>
    void OnGUI()
    {
        // Display our background texture.
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        // Display background.
        GUI.Label(new Rect(Screen.width * .3f, Screen.height * .1f, Screen.width * .4f, Screen.height * .9f), "", backgroundScroll);

        // Display logo.
        GUI.Label(new Rect(Screen.width * .29f, Screen.height * .02f, Screen.width * .43f, Screen.height * .35f), "", logo);

        // Create the username and password textboxes. Get the data from the user.
        userName = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .44f, Screen.width * .2f, Screen.height * .08f), userName, 20);
        password = GUI.PasswordField(new Rect(Screen.width * placementXForStrings, Screen.height * .54f, Screen.width * .2f, Screen.height * .08f), password, "*"[0], 20);

        errorStyle.fontSize = 20;
        GUI.Label(new Rect(Screen.width * placementXForStrings, Screen.height * .64f, Screen.width * .2f, Screen.height * .08f), errorMessage, errorStyle);

        // Displays our buttons: new Rect (how far in, how far down, width of button, height of button).
        // Displays button when we have textures.
        if (Input.GetKey(KeyCode.Return)) // The user wants to log in to the system.
        {
            if (userName != null && password != null) // The user tries to log in to the system.
            {
                LogInLoadLevel(); // Tries logging in to the system. If successful, loads the specific level.
            }
        }

        if (GUI.Button(new Rect(Screen.width * .438f, Screen.height * .7f, Screen.width * .12f, Screen.height * .1f), "", login))
        {
            if (CheckPassword())
            {
                errorMessage = "Invalid Username/Password. Please try again.";   
            }
            else
            { 
                LogInLoadLevel(); // Tries logging in to the system. If successful, loads the specific level.
            }
        }

        // The user clicked the "Sign Up" button.
        if (GUI.Button(new Rect(Screen.width * .51f, Screen.height * .82f, Screen.width * .12f, Screen.height * .1f), "", signup))
        {
            SceneManager.LoadScene("SignUpPage"); // Load the signup scene.
        }

        // The user clicked the "Forgot Password" button.
        if (GUI.Button(new Rect(Screen.width * .37f, Screen.height * .82f, Screen.width * .12f, Screen.height * .1f), "", forgotPassword))
        {
            SceneManager.LoadScene("ForgotPassword"); // Load the forgot password scene.
        }
    } // Close OnGUI().

    private bool CheckPassword()
    {
        errorMessage = "";
        // First, load the XML file and create a variable to hold the password.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("userAcctConfig");
        string passwordCheck = System.String.Empty;

        try
        {
            doc.LoadXml(asset.text);
        }
        catch (System.Exception exc)
        {
            errorMessage = "Sorry, " + exc.Message + " is your error";
            return false; // Return to the caling method.
        }

        // Second, find the node in the file that is associated with the user account.
        XmlNodeList list = doc.SelectNodes("/config/account");
        foreach (XmlNode node in list)
        {
            if (node.Attributes["userName"].Value == userName) // We've found the account.
            {
                passwordCheck = node["password"].InnerText.ToString(); // Store the password.
            }
        }

        if (System.String.IsNullOrEmpty(passwordCheck)) // We did not find the password.
        {
            return true;
        }
        else // The account exists.
        {
            return false;
        }
    }

    private void LogInLoadLevel()
    {
        bool found = false; // Tracks whether or not the account is found.

        // Store the username and password before clearing the field above.
        string userNameTemp = userName;
        string passwordTemp = password;

        // Get the XML document and reference the two account nodes.
        TextAsset asset = (TextAsset)Resources.Load("userAcctConfig");
        XmlDocument config = new XmlDocument();
        config.LoadXml(asset.text);

        XmlNodeList accts = config.SelectNodes("/config/account"); // Count how many accounts are stored on the computer.
        XmlNode acctNode = null; // A reference to the account node.

        for(int i=0; i<accts.Count; i++) // For each account.
        {
            if((userNameTemp == accts[i].Attributes["userName"].Value) && (passwordTemp == accts[i].ChildNodes[3].InnerText))
            {
                found = true;
                accts[i].Attributes["active"].Value = "true"; // Mark this account as the active one.
                acctNode = accts[i]; // We found the account. Store the reference.
            }
            else // This is not the correct account. Make sure it is not active.
            {
                accts[i].Attributes["active"].Value = "false";
            }
        } // Close the for loop.

        if(found) // We found a valid account. Load the correct level.
        {
            // Check Beach level progress.
            if (!System.Convert.ToBoolean(acctNode["beachComp"].InnerText.ToString())) // The beach level has not been completed; load the level.
            {
                SceneManager.LoadScene("Beach");
            }

            // Check Cave level progress.
            else if (!System.Convert.ToBoolean(acctNode["caveComp"].InnerText.ToString()))
            {
                SceneManager.LoadScene("Cave1");
            }

            // Check Forest level progress.
            else if (!System.Convert.ToBoolean(acctNode["forestComp"].InnerText.ToString()))
            {
                SceneManager.LoadScene("Forest1");
            }
        }

        if (!found) // The entered user account was not found.
        { 
            // Reset the values.
            userName = "Username";
            password = "Password";
        }
    } // Close LogInLoadLevel().
}
