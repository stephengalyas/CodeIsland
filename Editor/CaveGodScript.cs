using UnityEngine;
using System; // To update date/time information.
using System.Xml; // To read/write config information.
using System.IO; // To access the config information.
using System.Collections;
/// <summary>
/// This class has access to all game objects to be modified in the game.
/// Booleans are used to track the progress of the level.
/// The booleans, time, and score are loaded from an XML configuration file.
/// </summary>
public class CaveGodScript : MonoBehaviour {
    //---------------------------------------------General XML configuration variables-------------------------------------------------------------------------
    /// <summary>
    /// The game's configuration information.
    /// </summary>
    private XmlDocument config;

    /// <summary>
    /// The path of the XML file.
    /// </summary>
    private string xmlPath;

    /// <summary>
    /// The total time spend on this level.
    /// </summary>
    private DateTime levelTime;

    /// <summary>
    /// The time that the user loaded the level.
    /// </summary>
    private DateTime startTime;

    /// <summary>
    /// The player's score for this level.
    /// </summary>
    public int levelScore;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    //---------------------------------------------------------------Game object references--------------------------------------------------------------------
    /// A reference to the main character object.
    /// </summary>
    public GameObject mainChar;

    /// <summary>
    /// The fader object.
    /// </summary>
    public GameObject fader;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------


    //---------------------------------------------------------------Level progress booleans-------------------------------------------------------------------
    /// <summary>
    /// Tracks the first step of the level.
    /// </summary>
    public static bool enterDiaFin = false;

    /// <summary>
    /// Tracks the second step of the level.
    /// </summary>
    public static bool instrRead = false;

    /// <summary>
    /// Tracks the third step of the level.
    /// </summary>
    public static bool allTorchesDown = false;

    /// <summary>
    /// Tracks the fourth step of the level.
    /// </summary>
    public static bool gotKey = false;

    /// <summary>
    /// Tracks the fifth step of the level.
    /// </summary>
    public static bool doorUnlock = false;

    public static int torchCount = 0;

    private TextAsset xmlAsset;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    // Prepares the level for the user.
    void Start () {

        config = new XmlDocument();
        //InitVars(); // Initialize the variables.
        fader.GetComponent<LevelFader>().Fade(-1, 0.5f);
    }

    /// <summary>
    /// Initializes the variables associated with this level.
    /// </summary>
    //private void InitVars()
    //{
    //    xmlAsset = (TextAsset)Resources.Load("Character_Dialogues");
    //    // Instantiate the XML object.
    //    try
    //    {
    //        //DirectoryInfo path = new DirectoryInfo(Directory.GetCurrentDirectory()); // The current directory of the project.
    //        //xmlAsset.text = path.FullName + @"/Assets/Configs/God_config.xml"; // The path to the config file.
            

    //        //Stream data = File.Open(xmlAsset.text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite); // Get the file data.

    //        config.LoadXml(xmlAsset.text);
    //    }
    //    catch (XmlException exc) // Informs the user if the file could not be loaded.
    //    {
    //        //UnityEditor.EditorUtility.DisplayDialog("Error Loading Configuration File", "Error loading the configuration file. Error message:\n\n" + exc.Message, "OK"); // Display an error message.
    //        Application.Quit(); // Quit the application.
    //    }

    //    try // Update general information.
    //    {
    //        XmlNodeList genInfo = config.SelectSingleNode("/config/levelTwo/general").ChildNodes; // Get the list of child nodes

    //        // Update main character position.
    //        float xPos = Convert.ToSingle(genInfo[0].InnerText);
    //        float yPos = -(Convert.ToSingle(genInfo[1].InnerText));
    //        if (xPos != 0 && yPos != 0) // There is valid save data available.
    //        {
    //            mainChar.transform.position = new Vector3(xPos, yPos, 0);
    //        }
    //    }
    //    catch(System.Exception exc) // Error. Display the error message and exit the program.
    //    {
    //        //UnityEditor.EditorUtility.DisplayDialog("Error Getting Positions", "Error loading the character positions. Error message:\n\n" + exc.Message, "OK"); // Display an error message.
    //        Application.Quit();
    //    }

        //try // Update time and score.
        //{
        //    startTime = DateTime.Now; // Get the current time.
        //    levelTime = Convert.ToDateTime(config.SelectSingleNode("/config/levelTwo/general/time").InnerText); // Get the playtime.
        //    levelScore = Convert.ToInt32(config.SelectSingleNode("/config/levelTwo/general/score").InnerText); // Get the score.
        //}

        //catch (XmlException exc) // If there is an error reading the information.
        //{
        //    //UnityEditor.EditorUtility.DisplayDialog("Error Getting Time and Score", "Error loading level playtime and score. Error message:\n\n" + exc.Message, "OK"); // Display an error message.
        //    Application.Quit();
        //}
    //}

    /// <summary>
    /// Saves the user's progress.
    /// This method should be called before loading a new scene.
    /// </summary>
    //public void OnExit(bool skip = true)
    //{
        // Create vectors to update the positions.
        //Vector3 mainCharPos;

        //if (skip) // ONLY USE THESE VALUES IF THE USER HAS ENTERED THE CAVE.
        //{
        //    mainCharPos = new Vector3(1195, -1828, 0);
        //}

        //else // Reset the player to the beginning of the level.
        //{
        //    //mainChar
        //}

        //try // Save general informaton.
        //{
        //    config.LoadXml(xmlAsset.text); // Reload the XML file.

        //    XmlNodeList genInfo = config.SelectSingleNode("/config/levelOne/general").ChildNodes;
            //genInfo[0].InnerText = mainCharPos.x.ToString();
            //genInfo[1].InnerText = mainCharPos.y.ToString();
            //genInfo[2].InnerText = goblinPos.x.ToString();
            //genInfo[3].InnerText = goblinPos.y.ToString();

            // Calculate new time.
        //    TimeSpan change = (DateTime.Now - startTime); // Get the time span.
        //    DateTime newTime = levelTime.Add(change); // Save the new time.
        //    genInfo[4].InnerText = newTime.ToString("h:mm:ss"); // Save the time in the specified format.

        //    genInfo[5].InnerText = levelScore.ToString();

        //    config.Save(xmlAsset.text); // Save the XML file.
        //}

        //catch(XmlException exc)
        //{
            //UnityEditor.EditorUtility.DisplayDialog("Error Saving XML File", "Error saving XML file. Error message:\n\n" + exc.Message, "OK"); // Display an error message.
        //    Application.Quit();
        //}

        //try // Save level progress.
        //{
        //    config.Load(xmlAsset.text); // Reload the XML file.

        //    XmlNodeList progress = config.SelectSingleNode("/config/levelOne/progress").ChildNodes;
            //progress[0].InnerText = gobAtPlayer.ToString();
            //progress[1].InnerText = dialogueBoxFinished.ToString();
            //progress[2].InnerText = gobOffMap.ToString();
            //progress[3].InnerText = playerAtCave.ToString();

        //    config.Save(xmlAsset.text); // Save the XML file.
        //}

        //catch (XmlException exc)
        //{
        //    //UnityEditor.EditorUtility.DisplayDialog("Error Saving XML File", "Error saving XML file. Error message:\n\n" + exc.Message, "OK"); // Display an error message.
        //    Application.Quit();
        //}

        //try // Update global data.
        //{
        //    config.Load(xmlAsset.text); // Reload the XML file.

        //    // Update total time.
        //    DateTime last = Convert.ToDateTime(config.SelectSingleNode("/config/totals/totTime").InnerText);
        //    last.AddMinutes(DateTime.Now.Minute - startTime.Minute); // Get minutes.
        //    last.AddSeconds(DateTime.Now.Second - startTime.Second); // Get seconds
        //    config.SelectSingleNode("/config/totals/totTime").InnerText = last.ToString("h:mm:ss");

        //    // Update total score.
        //    float totScore = Convert.ToSingle(config.SelectSingleNode("/config/totals/totScore").InnerText);
        //    totScore += levelScore;
        //    config.SelectSingleNode("/config/totals/totScore").InnerText = totScore.ToString();
        //}

        //catch (XmlException exc)
        //{
        //    //UnityEditor.EditorUtility.DisplayDialog("Error Saving XML File", "Error saving XML file. Error message:\n\n" + exc.Message, "OK"); // Display an error message.
        //    Application.Quit();
        //}
    //}
    
    /// <summary>
    /// Save the character's coordinates (to be loaded when the game starts.
    /// </summary>
    //void OnApplicationQuit()
    //{
    //    //OnExit(false); // Call the OnExit() method.
    //}
}
