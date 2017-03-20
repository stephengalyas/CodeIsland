/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: December 11, 2016            Published on: December 19 21, 2016
 * This is the "god" script for the Main Character on the Beeach.
 * This class will contain all public variables that may be accessed throughout the level, such as booleans to track the character's progress and the score.
 * Depends on MCMovement and MCLoadSave.
 * 
 * THIS CLASS IS CURRENTLY NOT BEING USED.
*/

using System;
using UnityEngine;
using System.Collections;

public class PlayerBeachGod : MonoBehaviour {

    //---------------------------------------------------------------Level progress booleans-------------------------------------------------------------------
    /// <summary>
    /// Tracks the first step of the level.
    /// </summary>
    public static bool gobAtPlayer;

    /// <summary>
    /// Tracks the second step of the level.
    /// </summary>
    public static bool dialogueBoxFinished;

    /// <summary>
    /// Tracks the third step of the level.
    /// </summary>
    public static bool gobOffMap;

    /// <summary>
    /// Tracks the fourth step of the level.
    /// </summary>
    public static bool playerAtCave;

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
    void Start () {
		// Do nothing.
    }
}
