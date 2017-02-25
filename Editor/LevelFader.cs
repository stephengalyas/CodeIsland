using UnityEngine;
using System.Collections;

/// <summary>
/// This class creates a fade effect when the user is being transported between levels.
/// </summary>
public class LevelFader : MonoBehaviour {
    //------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The black image to be displayed when transitioning between levels.
    /// </summary>
    public Texture2D fadeOutTexture;

    /// <summary>
    /// The fade in/out speed.
    /// </summary>
    public float speed = 0.5f;

    /// <summary>
    /// The depth required to draw the image on top of the level.
    /// </summary>
    private int depth = -1000;

    /// <summary>
    /// The opacity of the image.
    /// </summary>
    private float alpha = 0.9f;

    /// <summary>
    /// The direction of fading (-1 = fade-in; 1 = fade-out).
    /// The number corresponds to the SCENE, not the black image.
    /// </summary>
    private int direction = -1;

    /// <summary>
    /// A boolean to track the fade status.
    /// </summary>
    public bool contFad = true;
    //-----------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// A method built in to the Unity engine that deals with modifying GUI elements of the game.
    /// </summary>
    void OnGUI()
    {
        if (contFad == true) // We want the fading to continue.
        {
            // Update the alpha value (convert it to a time). The direction and speed of the fade affect its value.
            alpha += direction * speed * Time.deltaTime;

            // Place a clamp on the value so it cannot exceed a range of 0-1.
            alpha = Mathf.Clamp01(alpha);

            // Update the color of the GUI element. Alpha is the opacity of the object.
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); // RGB colors are necessary.
            GUI.depth = depth; // Update the depth of the image.
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture); // Set the size of the texture and draw it.

            if(alpha == 1 || alpha == -1) // Stop fading.
            {
                contFad = false;
            }
        }
    }

    /// <summary>
    /// This method begins the fade effect.
    /// </summary>
    /// <param name="dir">The direction of the fade (1 = out; -1 = in).</param>
    /// <returns>The speed of the fade (for determining when to load another scene).</returns>
    public void Fade(int dir, float fadeSpeed)
    {
        direction = dir; // Update the fade direction.
        speed = fadeSpeed;
        contFad = true;
    }

    void OnLevelWasLoaded()
    {
        Fade(-1, 0.5f); // When the level loads, have it slowly fade in.
    }
}
