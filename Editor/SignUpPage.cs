/* Team Python          Last edit made by: Stephen Galyas           Created on: November 7, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class allows the user to enter data to create a new account.
 * Only the GUI functionality is managed by this class.
 * This class requires the CreateNewAccountScript class to actually create the account.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignUpPage : MonoBehaviour {

    public Texture backgroundTexture;

    public GUIStyle logo;
    public GUIStyle backgroundScroll;
    public GUIStyle createAccount;
    public GUIStyle cancelButton;

    public float placementXForStrings;

    public string firstName = "First Name";
    public string lastName = "Last Name";
    public string userName = "Username";
    public string password = "Password";
    public string email = "Email Address";

    void OnGUI()
    {
        //Display our background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        //Background
        GUI.Label(new Rect(Screen.width * .3f, Screen.height * .1f, Screen.width * .4f, Screen.height * .9f), "", backgroundScroll);

        //Logo
        GUI.Label(new Rect(Screen.width * .29f, Screen.height * .02f, Screen.width * .43f, Screen.height * .35f), "", logo);

        //Username and password textboxes
        firstName = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .37f, Screen.width * .2f, Screen.height * .06f), firstName, 20);
        lastName = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .45f, Screen.width * .2f, Screen.height * .06f), lastName, 20);
        email = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .53f, Screen.width * .2f, Screen.height * .06f), email, 40);
        userName = GUI.TextField(new Rect(Screen.width * placementXForStrings, Screen.height * .61f, Screen.width * .2f, Screen.height * .06f), userName, 20);
        //Possibly a TextField instead of PasswordField???
        password = GUI.PasswordField(new Rect(Screen.width * placementXForStrings, Screen.height * .69f, Screen.width * .2f, Screen.height * .06f), password, "*"[0], 20);

        if (GUI.Button(new Rect(Screen.width * .51f, Screen.height * .82f, Screen.width * .12f, Screen.height * .1f), "", createAccount))
        {
            UnityEditor.EditorUtility.DisplayDialog("Test", "Test", "Okay");
            CreateAccount(); // Create the new account.
        }

        if (GUI.Button(new Rect(Screen.width * .37f, Screen.height * .82f, Screen.width * .12f, Screen.height * .1f), "", cancelButton))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void CreateAccount()
    {
        // First, check if the account already exists.
        bool checkAcct = CreateNewAccountScript.CheckAccount(userName);
        if(checkAcct == true)
        {
            UnityEditor.EditorUtility.DisplayDialog("Account Already Exists", "That username is already associated with an account. Please " +
                                                    "try using another username", "Okay");
        }
        else // The account does not already exist.
        {
            string[] data = { firstName, lastName, email, userName, password };
            bool stepOne = CreateNewAccountScript.UpdateAcctFile(data); // Update the main account file.
            if(stepOne == true) // The account file was successfully updated.
            {
                bool stepTwo = CreateNewAccountScript.UpdateBeachLevelFile(userName); // Update the Beach file.
                if(stepTwo == true) // Successfully updated beach file.
                {
                    bool stepThree = CreateNewAccountScript.UpdateCaveLevelFile(userName); // Update the cave file.
                    if(stepThree == true) // Successfully updated cave file.
                    {
                        bool finalStep = CreateNewAccountScript.UpdateForestLevel(userName); // Update the Forest file.
                    }
                }

            }

        }
    }
}
