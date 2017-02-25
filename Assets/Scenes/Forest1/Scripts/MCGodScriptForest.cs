/* Team Python          Last edit made by: Stephen Galyas           Created on: December 18, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This is the "god" script for the Main Character on the Forest level.
 * This class will contain all booleans that are used to alter the character.
 * For this level, the booleans will make the player move faster or slower and flip the controls.
 * Provides to MCMovementForest and MCCollisionHandlerForest.
*/

using System;
using UnityEngine;
using System.Collections;

public class MCGodScriptForest : MonoBehaviour {

    //---------------------------------------------------------------Level progress booleans-------------------------------------------------------------------
    /// <summary>
    /// If this is true, the MC's speed is decreased by half.
    /// </summary>
    public static bool cherryEnabled;

    /// <summary>
    /// If this is true, the MC's speed is doubled.
    /// </summary>
    public static bool blueberryEnabled;

    /// <summary>
    /// If this is true, the MC's size decreases and can get through tight areas.
    /// </summary>
    public static bool strawberryEnabled;

    /// <summary>
    /// If this is true, the MC's size is increased and the MC can push objects.
    /// </summary>
    public static bool raspberryEnabled;

    /// <summary>
    /// If this is true, the user can exit the level by not being seen.
    /// </summary>
    public static bool mushroomEnabled;

    /// <summary>
    /// The total time spend on this level.
    /// </summary>
    public static DateTime levelTime;

    /// <summary>
    /// The time that the user loaded the level.
    /// </summary>
    public static DateTime startTime;

    /// <summary>
    /// The player's score for this level.
    /// </summary>
    public static int levelScore;

    /// <summary>
    /// Tracks whether or not the user has completed the level.
    /// </summary>
    public static bool completed;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// This method instantiates all associated objects when the level is loaded. 
    /// </summary>
    void Start()
    {
        // Get progress from save file.
        bool status = MCLoadSaveForest.LoadProgress(true, true); // Get the status of loading progress from the config file.
        if (status == false) // Error loading save data. Set default values.
        {
            cherryEnabled = false;
            blueberryEnabled = false;
            strawberryEnabled = false;
            raspberryEnabled = false;
            mushroomEnabled = false;
            levelScore = 0;
            completed = false;
            levelTime = DateTime.Now.Date;
        }
        startTime = DateTime.Now; // Get the current time.
    }
}

