using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    /// <summary>
    /// Stores the position data, scale data, and rotation data of an object. This is the object that the camera follows.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The speed of the main camera.
    /// </summary>
    //public float cam_speed = 1f;

    /// <summary>
    /// The Camera object of the level.
    /// </summary>
    Camera myCam;

    public Vector3 maxCamPos;
    public Vector3 minCamPos;
    public Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    public bool bounds;
    
    // Use this for initialization
	void Start () {

        myCam = GetComponent<Camera>(); // Get the reference to the camera.
	}
	
	//void Update()
 //   {
 //       myCam.orthographicSize = (Screen.height / 2f); // Update the size of the camera. Maintains the aspect ratio of the game on multiple platforms. IMPORTANT FOR OUR GAME.
 //   }

    // Update is called once per frame
    void FixedUpdate () {

        //if (target) // If the object exists.
        //{
            //Vector3 temp = Vector3.Lerp(transform.position, target.position, cam_speed) +(new Vector3(0, 0, -10)); // "Transform" is a characteristic of the Camera object.
            // Lerp = linear interpolation. Defines the "step" from one number to another (such as 0.2 --> 0, 0.2, 0.4, . . .).
            // Lerp(from, to, how fast). We add -10 so the camera does not float (literally) into the level.
            // Adjust the "0cam_speed" value to increase/decrease the speed at which the camera follows the character.

            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);

            if(bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x),
                                     Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
                                     Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z));
        }

	}
}
