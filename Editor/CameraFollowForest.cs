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

    float mapX;
    float mapY;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    float vertExtent;
    float ratio;
    float horzExtent;

    // Use this for initialization
    void Start()
    {
        myCam = GetComponent<Camera>(); // Get the reference to the camera.        
    }

    void Update()
    {
        vertExtent = myCam.orthographicSize;
        ratio = Convert.ToSingle(Screen.width) / Convert.ToSingle(Screen.height);
        horzExtent = vertExtent * ratio;

        Rect mapSize = map.GetComponent<Tiled2Unity.TiledMap>().GetMapRect();

        // Calculations assume map is position at the origin
        minX = mapSize.min.x + horzExtent;
        maxX = mapSize.max.x - horzExtent;
        maxY = mapSize.max.y - vertExtent;
        minY = mapSize.min.y + vertExtent;

        Vector3 v3 = target.transform.position;
        v3.z = -10f;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        myCam.transform.position = v3;
    }
}