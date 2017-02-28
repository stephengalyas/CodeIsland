/* Team Python          Last edit made by: Stephen Galyas           Created on: December 11, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This class manages the movement of the Camera so it follows the Main Character.
*/

using UnityEngine;
using System;
using System.Collections;

public class CameraFollowForest : MonoBehaviour {

    /// <summary>
    /// Stores the position data, scale data, and rotation data of an object. This is the object that the camera follows.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The beginning sublevel.
    /// </summary>
    public GameObject map;

    /// <summary>
    /// The Camera object of the level.
    /// </summary>
    Camera myCam;

    // Camera values.
    float mapX;
    float mapY;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    float vertExtent;
    float ratio;
    float horzExtent;

    /// <summary>
    /// Get the Camera object from the level.
    /// </summary>
    void Start()
    {
        myCam = GetComponent<Camera>(); // Get the reference to the camera.        
    }

    /// <summary>
    /// Updates the Camera so it moves with the main character.
    /// </summary>
    void Update()
    {
        // Get the sizes.
        vertExtent = myCam.orthographicSize;
        ratio = Convert.ToSingle(Screen.width) / Convert.ToSingle(Screen.height);
        horzExtent = vertExtent * ratio;

        Rect mapSize = map.GetComponent<Tiled2Unity.TiledMap>().GetMapRect();

        // Calculations assume map is position at the origin
        minX = mapSize.min.x + horzExtent;
        maxX = mapSize.max.x - horzExtent;
        maxY = mapSize.max.y - vertExtent;
        minY = mapSize.min.y + vertExtent;

        // Update the Vector.
        Vector3 v3 = target.transform.position;
        v3.z = -10f;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        myCam.transform.position = v3;
    }
}