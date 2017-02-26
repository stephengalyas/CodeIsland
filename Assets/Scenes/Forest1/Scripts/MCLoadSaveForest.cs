/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class manages the load/save functionality for the main character on the Forest level.
 * Uses MCGodScriptForest.
*/

using System;
using System.Xml;
using UnityEngine;
//using UnityEditor;
using System.Collections;

public class MCLoadSaveForest: MonoBehaviour {

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
        data = (TextAsset)Resources.Load("MCConfigForest");

        // Save the file path (IN THE BUILD FOLDER).
        path = Application.dataPath + "/Resources/MCConfigForest.xml";
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
                Debug.Log("Skipping Load Progress From File Process --> Using default values."); // Display message.
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
                    Debug.Log("Load Config File --> Success"); // Display message.
                }
            } // Close try block.
            catch (XmlException exc) // Error accessing XML file.
            {
                if(testing == true)
                {
                   Debug.Log("ERROR --> Could not load XML file. Error message: \n\n" + exc.Message); // Display message.
                }
                else // Not testing. Display user-friendly error message.
                {
                    Debug.Log("Error Loading Progress --> Your progress could not be loaded. Please exit the game and try again.\n\n" +
                                               "If you complete the first level again, you may lose your original progress"); // Display message.
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
                MCGodScriptForest.levelTime = DateTime.Now.Date; // Sets the date to today, BUT A TIME OF 00:00:00.
                MCGodScriptForest.levelTime.AddMinutes(minutes); // Add minutes to the DateTime object.
                MCGodScriptForest.startTime = DateTime.Now;

                MCGodScriptForest.levelScore = score;

                if (testing == true) // Testing. Display status.
                {
                    Debug.Log("Load Time and Score --> Success");
                }
            } // Close try block.
            catch (Exception exc) // Error loading data from config file.
            {
                if (testing == true)
                {
                    Debug.Log("ERROR --> Could not load time and score. Error message: \n\n" + exc.Message); // Display message.
                }
                else // Not testing. Display user-friendly error message.
                {
                   Debug.Log("Error Loading Progress --> Your progress could not be loaded. Please exit the game and try again.\n\n" +
                                               "If you complete the first level again, you may lose your original progress");  // Display message.
                }

                return false; // Return and load default values.
            } // Close catch block.

            MCGodScriptForest.blueberryEnabled = false;
            MCGodScriptForest.cherryEnabled = false;
            MCGodScriptForest.mushroomEnabled = false;
            MCGodScriptForest.raspberryEnabled = false;
            MCGodScriptForest.strawberryEnabled = false;

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
                Debug.Log("Skipping Save Process --> Progress will not be saved to the file."); // Display message.
            }

            return false;
        } // Close if testing.

        else // Save progress to the main character's configuration file.
        {
            // Load XML file.
            try
            {
                xmlDoc.LoadXml(data.text);
                if (testing == true) // Testing. Display progress.
                {
                   Debug.Log("Load XML File --> Success");
                }
            }

            catch (XmlException exc)
            {
                if (testing == true) // Testing. Display detailed error message.
                {
                    Debug.Log("ERROR --> Could not load XML file.\n\nError message: " + exc.Message);
                }
                else // Display user-friendly error message.
                {
                   Debug.Log("Could Not Save Progress --> Your progress could not be saved." +
                                                "You may coninue to play the game, but your progress may not be saved.");
                }
                return false;
            }

            // Save time and score.
            try
            {
                // Calculate elapsed time.
                TimeSpan elapsedTime = DateTime.Now - MCGodScriptForest.startTime; // Calculate total time spent playing the level.
                DateTime newLvlTime = MCGodScriptForest.levelTime.AddMinutes(elapsedTime.TotalMinutes); // Add the elapsed time to the loaded time
                                                                                                       // and store it in a new DateTime object.
                TimeSpan calcTime = newLvlTime - DateTime.Now.Date; // Recalculate the total number of minutes spent playing the level.
                                                                    // DateTime.Now.Date contains time 00:00:00.
                xmlDoc.SelectSingleNode("/config/general/time").InnerText = calcTime.TotalMinutes.ToString(); // Save minutes.

                xmlDoc.SelectSingleNode("/config/general/score").InnerText = MCGodScriptForest.levelScore.ToString();

                xmlDoc.Save(path); // Save the config file BACK TO THE RESOURCES FOLDER. This is independent of whether or not the game has been built. 

                if (testing == true) // Testing. Display progress.
                {
                    Debug.Log("Save Time and Score --> Success");
                }
            } // Close try block.
            catch (Exception exc) // Error saving data to config file.
            {
                if (testing == true)
                {
                    Debug.Log("ERROR --> Could not save time and score. Error message: \n\n" + exc.Message); // Display message.
                }
                else // Not testing. Display user-friendly error message.
                {
                   Debug.Log("Could Not Save Progress --> Your progress could not be saved." +
                                               "You may coninue to play the game, but your progress may not be saved.");                     // Display message.
                }

                return false; // Return and load default values.
            } // Close catch block.

            // Save the XML document.
            xmlDoc.Save(path);

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
            System.IO.File.Copy(path + "MCConfigForest.xml", path + "MCConfigForest_BACKUP.xml", true);
            return true;
        }

        catch(System.Exception) // Could not copy the file.
        {
            return false;
        }
    } // Close Backup().
}
