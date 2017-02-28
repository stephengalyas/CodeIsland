/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: December 11, 2016            Published on: December 19 21, 2016
 * This class manages the load/save functionality for the main character on the Beach level.
 * Uses MCGodScriptBeach.
*/

using System;
using System.Xml;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class MCLoadSaveBeach : MonoBehaviour {

    /// <summary>
    /// The object used to load and save data (once the game has been published).
    /// </summary>
    private static TextAsset data;

    /// <summary>
    /// The XML document containing the save data.
    /// </summary>
    private static XmlDocument xmlDoc;

    /// <summary>
    /// The path of the XML file.
    /// </summary>
    private static string path;

	/// <summary>
    /// Instantiate the objects, throw an error if save data cannot be loaded.
    /// </summary>
	void Start ()
    {
        xmlDoc = new XmlDocument();
        data = (TextAsset)Resources.Load("MCConfigBeach");

        // Save the file path (IN THE BUILD FOLDER).
        path = Application.dataPath + "/Resources/MCConfigBeach.xml";
	}

    /// <summary>
    /// This method loads the save data for the beach level.
    /// </summary>
    /// <param name="skip">Used to bypass load/save process.</param>
    /// <param name="testing">Used for testing. If true, displays popups of progress.</param>
    /// <returns>Returns true if data is successfully loaded. Otherwise, returns false.</returns>
    public static bool LoadProgress(bool skip = false, bool testing = false)
    {

        if(skip == true) // Testing. Skip loading config data and load default values.
        {
            if(testing == true) // Testing. Let the programmer know we are skipping this process.
            {
                EditorUtility.DisplayDialog("Skipping Load Progress From File Process", "Using default values.", "Play Game"); // Display message.
            }
            return false; // Skip this.
        }

        else // Not testing.
        {
            // Load the config file.
            try
            {
                xmlDoc.LoadXml(data.text);
                if(testing == true) // Testing the method.
                {
                    EditorUtility.DisplayDialog("Load Config File", "Success", "Load Time and Score"); // Display message.
                }
            } // Close try block.
            catch (XmlException exc) // Error accessing XML file.
            {
                if(testing == true)
                {
                    EditorUtility.DisplayDialog("ERROR", "Could not load XML file. Error message: \n\n" + exc.Message, "Load Defaults"); // Display message.
                }
                else // Not testing. Display user-friendly error message.
                {
                    EditorUtility.DisplayDialog("Error Loading Progress", "Your progress could not be loaded. Please exit the game and try again.\n\n" +
                                                "If you complete the first level again, you may lose your original progress", "Okay");                      // Display message.
                }

                return false; // Return and load default values.
            } // Close catch block.

            // Load general level data.
            try
            {
                XmlNodeList nodes = xmlDoc.SelectSingleNode("/config/general").ChildNodes; // Get all child nodes that contain general data.

                // Load the data.
                double minutes = Convert.ToDouble(nodes[0].InnerText);
                int score = Convert.ToInt32(nodes[1].InnerText);

                // Update time and score.
                MCGodScriptBeach.levelTime = DateTime.Now.Date; // Sets the date to today, BUT A TIME OF 00:00:00.
                MCGodScriptBeach.levelTime.AddMinutes(minutes); // Add minutes to the DateTime object.
                MCGodScriptBeach.startTime = DateTime.Now;

                MCGodScriptBeach.levelScore = score;

                if (testing == true) // Testing. Display status.
                {
                    EditorUtility.DisplayDialog("Load Time and Score", "Success", "Load Progress Bools");
                }
            } // Close try block.
            catch (Exception exc) // Error loading data from config file.
            {
                if (testing == true)
                {
                    EditorUtility.DisplayDialog("ERROR", "Could not load time and score. Error message: \n\n" + exc.Message, "Load Defaults"); // Display message.
                }
                else // Not testing. Display user-friendly error message.
                {
                    EditorUtility.DisplayDialog("Error Loading Progress", "Your progress could not be loaded. Please exit the game and try again.\n\n" +
                                                "If you complete the first level again, you may lose your original progress", "Okay");                      // Display message.
                }

                return false; // Return and load default values.
            } // Close catch block.


            MCGodScriptBeach.completed = false;
            MCGodScriptBeach.gobAtPlayer = false;
            MCGodScriptBeach.dialogueBoxFinished = false;
            MCGodScriptBeach.gobOffMap = false;
            MCGodScriptBeach.playerAtCave = false;

            // All data successfully loaded. Return true.
            return true;

        } // Close else (not testing).
    } // Close LoadProgress().

    /// <summary>
    /// Saves the user's progress.
    /// </summary>
    /// <param name="skip">Used to bypass load/save process.</param>
    /// <param name="testing">Used for testing. If true, displays popups of progress.</param>
    /// <returns>Returns true if successful. Otherwise, returns false.</returns>
    public static bool SaveProgress(bool skip = false, bool testing = false)
    {
        if(skip == true) // We want to skip the save process.
        {
            if(testing == true) // Testing. Inform the programmer that we are skipping this process.
            {
                EditorUtility.DisplayDialog("Skipping Save Process", "Progress will not be saved to the file.", "Next Level"); // Display message.
            }

            return false;
        } // Close if testing.

        else // Save progress to the main character's configuration file.
        {
            // Try making a backup of the file.
            // Backup();

            // Load XML file.
            try
            {
                xmlDoc.LoadXml(data.text);
                if (testing == true) // Testing. Display progress.
                {
                    EditorUtility.DisplayDialog("Load XML File", "Success", "Save Time and Score");
                }
            }

            catch (XmlException exc)
            {
                if (testing == true) // Testing. Display detailed error message.
                {
                    EditorUtility.DisplayDialog("ERROR", "Could not load XML file.\n\nError message: " + exc.Message, "Next Level");
                }
                else // Display user-friendly error message.
                {
                    EditorUtility.DisplayDialog("Could Not Save Progress", "Your progress could not be saved." +
                                                "You may coninue to play the game, but your progress may not be saved.", "Okay");
                }
                return false;
            }

            // Save time and score.
            try
            {
                // Calculate elapsed time.
                TimeSpan elapsedTime = DateTime.Now - MCGodScriptBeach.startTime; // Calculate total time spent playing the level.
                DateTime newLvlTime = MCGodScriptBeach.levelTime.AddMinutes(elapsedTime.TotalMinutes); // Add the elapsed time to the loaded time
                                                                                                       // and store it in a new DateTime object.
                TimeSpan calcTime = newLvlTime - DateTime.Now.Date; // Recalculate the total number of minutes spent playing the level.
                                                                    // DateTime.Now.Date contains time 00:00:00.
                xmlDoc.SelectSingleNode("/config/general/time").InnerText = calcTime.TotalMinutes.ToString(); // Save minutes.

                xmlDoc.SelectSingleNode("/config/general/score").InnerText = MCGodScriptBeach.levelScore.ToString();

                xmlDoc.Save(path); // Save the config file BACK TO THE RESOURCES FOLDER. This is independent of whether or not the game has been built. 

                if (testing == true) // Testing. Display progress.
                {
                    EditorUtility.DisplayDialog("Save Time and Score", "Success", "Save Progress Bools");
                }
            } // Close try block.
            catch (Exception exc) // Error saving data to config file.
            {
                if (testing == true)
                {
                    EditorUtility.DisplayDialog("ERROR", "Could not save time and score. Error message: \n\n" + exc.Message, "Load Defaults"); // Display message.
                }
                else // Not testing. Display user-friendly error message.
                {
                    EditorUtility.DisplayDialog("Could Not Save Progress", "Your progress could not be saved." +
                                                "You may coninue to play the game, but your progress may not be saved.", "Okay");                     // Display message.
                }

                return false; // Return and load default values.
            } // Close catch block.

            // Save progress in Accounts config file.
            //xmlDoc = null;
            //data = null;

            //xmlDoc = new XmlDocument();
            //data = (TextAsset)Resources.Load("userAcctsConfig");
            //xmlDoc.LoadXml(data.text);

            


            // Saving complete. Return true.
            return true;
        } // Close else block.
    }

    /// <summary>
    /// Makes a backup of the configuration file. This is a preventative measure in case the file becomes corrupted.
    /// </summary>
    /// <returns>Returns true if the backup is successful. Otherwise, returns false.</returns>
    private static bool Backup()
    {
        // Get the path of the file.
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
        string path = dir.FullName + @"/Assets/Resources/";

        // Make the copy.
        try
        {
            System.IO.File.Copy(path + "MCConfigBeach.xml", path + "MCConfigBeach_BACKUP.xml", true);
            return true;
        }

        catch(System.Exception) // Could not copy the file.
        {
            return false;
        }
    } // Close Backup().
}
