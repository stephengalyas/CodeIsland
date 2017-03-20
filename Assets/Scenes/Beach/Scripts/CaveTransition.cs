/* Team Python          Last edit made by: Stephen Galyas           Created on: November 11, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * The class manages the main character's transition from the Beach level to the Cave level.
 * The player's progress is saved before the next level is loaded.
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This class transitions the game from the Beach level to the Cave level.
/// </summary>
public class CaveTransition : MonoBehaviour {

    /// <summary>
    /// The object containing the fade script.
    /// </summary>
    public GameObject fader; // The fader object.

    /// <summary>
    /// A reference to the map, which contains the god script.
    /// </summary>
    public GameObject map1;

    /// <summary>
    /// Manages the collision with the cave entrance.
    /// </summary>
    /// <param name="coll">The collision.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        // First, save the player's progress.
        //MCLoadSaveBeach.SaveProgress(true, true); // Save the user's progress.

        // Second, save the complete status in the Accounts config file.
        SceneManager.LoadScene(4); // Load the cave level.
    }

}
