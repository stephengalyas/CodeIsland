/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class is responsible for creating a new local account.
 * All XML files are modified to include save data for the new account (only if the account does not already exist).
 * By separating this code from the script that manages the GUI, we can better test its reliability.
 * This class provides to SignUpPage and requires information from SignUpPage.
*/

using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Xml;

public class CreateNewAccountScript : MonoBehaviour {

    /// <summary>
    /// Checks if the new account already exists on the local machine.
    /// </summary>
    /// <param name="userName">The username of the account to be checked.</param>
    /// <returns>Returns true if the account already exists, otherwise returns false.</returns>
    public static bool CheckAccount(string userName)
    {
        // Access the Account Manager document.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("userAcctConfig");
        doc.LoadXml(asset.text);

        // Check if the account already exists.
        XmlNodeList list = doc.SelectNodes("/config/account");
        foreach (XmlNode node in list)
        {
            if(node.Attributes["userName"].Value == userName) // The account already exists.
            {
                return true;
            }
        }

        return false; // We have searched all local accounts and the new account does not already exist.
    } // Close CheckAccount().

    /// <summary>
    /// Updates the account configuration file to include the new account.
    /// Precondition: the account does not already exist.
    /// </summary>
    /// <param name="data">The new account information.</param>
    /// <param name="admin">An optional parameter. If true, displays detailed errors.</param>
    /// <returns>Returns true if the file was successfully updated, otherwise false.</returns>
    public static bool UpdateAcctFile(string[] data, bool admin = false)
    {
        // First, store local references to the information in the array and the path of the XML file to be updated.
        string firstName = data[0];
        string lastName = data[1];
        string email = data[2];
        string userName = data[3];
        string password = data[4];
        string path = Application.dataPath + "/Resources/userAcctConfig.xml";

        // Second, load the configuration file into an XML object.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("userAcctConfig");

        try
        {
            doc.LoadXml(asset.text);
        }
        catch(Exception exc) // Error accessing the save file.
        {
            string message = "Error: the game could not access the save file.\n\nMessage: " + exc.Message;
            if(admin == true) // An administrator is testing the software. Add detailed information to the error message.
            {
                message += "\n\nLine number: 57. Stack trace: " + exc.StackTrace;
            }
           // EditorUtility.DisplayDialog("Error Accessing Save File", message, "Okay"); // Display the error message.
            return false;
        }

        // Third, create the XML elements to be added to the document.
        XmlElement newParent = doc.CreateElement("account");
        XmlElement fname = doc.CreateElement("firstName");
        XmlElement lname = doc.CreateElement("lastName");
        XmlElement eadd = doc.CreateElement("email");
        XmlElement uname = doc.CreateElement("userName");
        XmlElement upass = doc.CreateElement("password");
        XmlElement student = doc.CreateElement("student");
        XmlElement beach = doc.CreateElement("beachComp");
        XmlElement cave = doc.CreateElement("caveComp");
        XmlElement forest = doc.CreateElement("forestComp");

        // Fourth, store a reference to the root node of the file.
        XmlNode root = doc.DocumentElement;

        // Fifth, insert the information into the new nodes.
        try
        {
            XmlAttribute att = doc.CreateAttribute("userName");
            att.Value = userName;
            XmlAttribute att2 = doc.CreateAttribute("active");
            att2.Value = "false";
            newParent.Attributes.Append(att); // Add the attribute to the new "account" node.
            newParent.Attributes.Append(att2); // Add the second attribute.
            fname.InnerText = firstName;
            lname.InnerText = lastName;

            XmlCDataSection info = doc.CreateCDataSection(email);
            eadd.AppendChild(info); // Add the CDataSection to the node.
            uname.InnerText = userName;
            upass.InnerText = password;
            student.InnerText = "True";
            beach.InnerText = "False";
            cave.InnerText = "False";
            forest.InnerText = "False";
        }
        catch(Exception exc)
        {
            string message = "Error: the save file could not be prepared to add the new account.\n\nMessage: " + exc.Message;
            if (admin == true) // An administrator is testing the software. Add detailed information to the error message.
            {
                message += "\n\nLines 93-107. Stack trace: " + exc.StackTrace;
            }
           // EditorUtility.DisplayDialog("Error Preparing Save File", message, "Okay"); // Display the error message.
            return false;
        }

        try
        {
            // Sixth, append the new nodes to the new root.
            newParent.AppendChild(fname);
            newParent.AppendChild(lname);
            newParent.AppendChild(uname);
            newParent.AppendChild(upass);
            newParent.AppendChild(eadd);
            newParent.AppendChild(student);
            newParent.AppendChild(beach);
            newParent.AppendChild(cave);
            newParent.AppendChild(forest);

            // Seventh, append this data to the root of the document and save the changes.
            root.AppendChild(newParent);
            doc.Save(path);
        }
        catch(Exception exc)
        {
            string message = "Error: the save file could not be updated with the new account information.\n\nMessage: " + exc.Message;
            if (admin == true) // An administrator is testing the software. Add detailed information to the error message.
            {
                message += "\n\nLines 121-135. Stack trace: " + exc.StackTrace;
            }
            //EditorUtility.DisplayDialog("Error Preparing Save File", message, "Okay"); // Display the error message.
            return false;
        }

        // Eighth, the process is complete. Return true.
        return true;
    } // Close UpdateAcctInfo().

    /// <summary>
    /// Updates the Beach level configuration file for the new user account.
    /// </summary>
    /// <param name="userName">The username of the new account.</param>
    /// <returns>True if the process is success, otherwise false.</returns>
    public static bool UpdateBeachLevelFile(string userName)
    {
        // First, create the XmlDocument and TextAsset objects.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("MCConfigBeach");

        // Second, call the private method to update the file.
        return UpdateLevelFile(doc, asset, "MCConfigBeach.xml", userName);
    }

    /// <summary>
    /// Updates the Cave level configuration file for the new user account.
    /// </summary>
    /// <param name="userName">The username of the new account.</param>
    /// <returns>True if the process is success, otherwise false.</returns>
    public static bool UpdateCaveLevelFile(string userName)
    {
        // First, create the XmlDocument and TextAsset objects.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("MCConfigCave");

        // Second, call the private method to update the file.
        return UpdateLevelFile(doc, asset, "MCConfigCave.xml", userName);
    }

    /// <summary>
    /// Updates the Forest level configuration file for the new user account.
    /// </summary>
    /// <param name="userName">The username of the new account.</param>
    /// <returns>True if the process is success, otherwise false.</returns>
    public static bool UpdateForestLevel(string userName)
    {
        // First, create the XmlDocument and TextAsset objects.
        XmlDocument doc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("MCConfigForest");

        // Second, call the private method to update the file.
        return UpdateLevelFile(doc, asset, "MCConfigForest.xml", userName);
    }

    /// <summary>
    /// Updates the level config file for the new account.
    /// </summary>
    /// <param name="doc">The XmlDocument object for the level.</param>
    /// <param name="asset">Used to load/save the XML file.</param>
    /// <param name="fileName">The file name of the XML file.</param>
    /// <param name="userName">The username of the new account.</param>
    /// <param name="admin">An optional parameter. If true, detailed error messages are presented to the programmer.</param>
    /// <returns></returns>
    private static bool UpdateLevelFile(XmlDocument doc, TextAsset asset, string fileName, string userName, bool admin=false)
    {
        // First, get the path of the configuration file.
        string path = Application.dataPath + "/Resources/" + fileName;

        try
        {
            doc.LoadXml(asset.text);
        }
        catch(Exception exc)
        {
            string message = "Error: the configuration files could not be updated with the new account information.\n\nMessage: " + exc.Message;
            if (admin == true) // An administrator is testing the software. Add detailed information to the error message.
            {
                message += "\n\nLines 164-166. Stack trace: " + exc.StackTrace;
            }
            //EditorUtility.DisplayDialog("Error Preparing Save File", message, "Okay"); // Display the error message.
            return false;
        }

        // Second, create the new nodes.
        XmlElement newParent = doc.CreateElement("general");
        XmlAttribute attr = doc.CreateAttribute("userName");
        XmlElement time = doc.CreateElement("time");
        XmlElement score = doc.CreateElement("score");

        // Third, get a reference to the root of the document.
        XmlElement root = doc.DocumentElement;

        // Fourth, append the data to the new nodes.
        try
        {
            attr.Value = userName;
            newParent.Attributes.Append(attr);
            time.InnerText = "0";
            score.InnerText = "0";
        }
        catch (Exception exc)
        {
            string message = "Error: the configuration files could not be updated with the new account information.\n\nMessage: " + exc.Message;
            if (admin == true) // An administrator is testing the software. Add detailed information to the error message.
            {
                message += "\n\nLines 189-194. Stack trace: " + exc.StackTrace;
            }
            //EditorUtility.DisplayDialog("Error Preparing Save File", message, "Okay"); // Display the error message.
            return false;
        }

        // Fifth, append the new nodes to the file and save the file.
        try
        {
            newParent.AppendChild(time);
            newParent.AppendChild(score);
            root.AppendChild(newParent);

            doc.Save(path);
        }
        catch(Exception exc)
        {
            string message = "Error: the configuration files could not be updated with the new account information.\n\nMessage: " + exc.Message;
            if (admin == true) // An administrator is testing the software. Add detailed information to the error message.
            {
                message += "\n\nLines 212-218. Stack trace: " + exc.StackTrace;
            }
            //EditorUtility.DisplayDialog("Error Preparing Save File", message, "Okay"); // Display the error message.
            return false;
        }

        // Sixth, the process is complete. Return true.
        return true;
    } 
}
