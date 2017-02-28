using UnityEngine;
using System.Collections;

public class TransporterScriptCave1 : MonoBehaviour {

    /// <summary>
    /// A reference to the hole object on CaveLevel1.
    /// </summary>
    public GameObject hole;

    /// <summary>
    /// A reference to the stairs object on CaveLevel2.
    /// </summary>
    public GameObject stairsDown;

    /// <summary>
    /// A reference to the stairs object on CaveLevel3.
    /// </summary>
    public GameObject stairsUp;

    /// <summary>
    /// A reference to the ladder object on CaveLevel2.
    /// </summary>
    public GameObject ladder;

    /// <summary>
    /// The first part of the cave.
    /// </summary>
    public GameObject cave1;

    /// <summary>
    /// The second part of the cave.
    /// </summary>
    public GameObject cave2;

    /// <summary>
    /// The third part of the cave.
    /// </summary>
    public GameObject cave3;

    /// <summary>
    /// A reference to the Camera object.
    /// </summary>
    public Camera cam;

    private int wait = 0;

    void Update()
    {
        if(wait < 150)
        {
            wait++;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (wait == 150)
        {
            if (coll.gameObject.name == "holeTransport")
            {
                this.transform.position = new Vector3(-3222f, 1446f, 0);
                cam.GetComponent<CameraFollowCave1>().map = cave2;
                this.gameObject.GetComponent<Animator>().SetFloat("input_y", -1f);
                coll = null;
                wait = 0;
            }

            else if (coll.gameObject.name == "ladderTransport")
            {
                this.transform.position = new Vector3(106f, -239f, 0);
                cam.GetComponent<CameraFollowCave1>().map = cave1;
                this.gameObject.GetComponent<Animator>().SetFloat("input_y", -1f);
                coll = null;
                wait = 0;
            }

            else if (coll.gameObject.name == "stairDownTransport")
            {

                Debug.Log("Going down!");
                this.transform.position = new Vector3(955f, 2147f, 0);
                cam.GetComponent<CameraFollowCave1>().map = cave3;
                this.gameObject.GetComponent<Animator>().SetFloat("input_y", -1f);
                coll = null;
                wait = 0;
            }

            else if (coll.gameObject.name == "stairUpTransport")
            {
                Debug.Log("Going up!");
                this.transform.position = new Vector3(-1356f, 1198f, 0);
                cam.GetComponent<CameraFollowCave1>().map = cave2;
                this.gameObject.GetComponent<Animator>().SetFloat("input_x", -1f);
                coll = null;
                wait = 0;
            }
        }
    }
}
