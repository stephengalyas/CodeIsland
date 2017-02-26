/* Team Python          Last edit made by: Stephen Galyas           Created on: December 1, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This is the god script for the Cave level.
*/

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

    /// <summary>
    /// Initializes the variables.
    /// </summary>
    void Start () {

    }
}
