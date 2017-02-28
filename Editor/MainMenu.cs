/* Team Python          Last edit made by: Stephen Galyas           Created on: November 7, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class creates and manages the user's interaction with the Main Menu.
 * If the user logs in with a valid username and password, the last non-completed level is loaded.
*/

///<summary>
///The class that manages the system's main menu.
///</summary>
using UnityEngine;
using UnityEditor;
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
            if (userName != null && password != null) // The user wants to login in to the system.
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
        for(int i=0; i<accts.Count; i++) // For each account.
        {
            if((userNameTemp == accts[i].Attributes["userName"].Value) && (passwordTemp == accts[i].ChildNodes[3].InnerText))
            {
                // We found the entered account.
                found = true;

                // Check Beach level progress.
                if (!System.Convert.ToBoolean(accts[i]["beachComp"].InnerText.ToString())) // The beach level has not been completed; load the level.
                {
                    SceneManager.LoadScene("Beach");
                }

                // Check Cave level progress.
                else if (!System.Convert.ToBoolean(accts[i]["caveComp"].InnerText.ToString()))
                {
                    SceneManager.LoadScene("Cave1");
                }

                // Check Forest level progress.
                else if (!System.Convert.ToBoolean(accts[i]["forestComp"].InnerText.ToString()))
                {
                    SceneManager.LoadScene("Forest1");
                }

                else // The user has compelted all levels.
                {
                    EditorUtility.DisplayDialog("Game Completed", "Congratulations! You have completed all levels of the game!\n\n" +
                                            "If you would like to play again, please contact the support team.", "Okay");
                }
            }
        } // Close the for loop.

        if (!found) // The entered user account was not found.
        {
            EditorUtility.DisplayDialog("Could Not Find Account", "The username/password entered is incorrect. Please try again.", "Okay");

            // Reset the values.
            userName = "Username";
            password = "Password";
        }
    } // Close LogInLoadLevel().
}
