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
        MCGodScriptBeach.levelScore += 20; // Update the score.
        fader.GetComponent<LevelFader>().Fade(1, 0.3f); // Fade out.
        MCLoadSaveBeach.SaveProgress(true, true); // Save the user's progress.
        SceneManager.LoadScene(5); // Load the cave level.
    }

}
