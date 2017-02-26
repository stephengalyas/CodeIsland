using UnityEngine;
using System.Collections;

public class ChestCollisionScript : MonoBehaviour {

	//cavelevel1 objects
	public GameObject L1_dirt2;
	public GameObject L1_mountains1;
	public GameObject L1_mountains2;
	public GameObject L1_base_out_atlas1;
	public GameObject L1_mountains3;
	public GameObject L1_hole;
	public GameObject L1_CaveDoor;
	public GameObject L1_terrain;
	public GameObject L1_mountains4;
	public GameObject L1_obj_misk_atlas;


	//cavelever2 objects
	public GameObject L2_dirt2;
	public GameObject L2_terrain_atlas;
	public GameObject L2_mountains1;
	public GameObject L2_water;
	public GameObject L2_terrain_atlas2;
	public GameObject L2_base_out_atlas;
	public GameObject L2_cement;
	public GameObject L2_cementstair;
	public GameObject L2_mountains2;
	public GameObject L2_dirt22;
	public GameObject L2_mountains3;
	public GameObject L2_mountains4;
	public GameObject L2_terrain_atlas3;
	public GameObject L2_base_out_atlas2;

	// Use this for initialization
	void Start () {
        
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "Player")
		{
            CaveGodScript.doorUnlock = true;

			//UPDATE RENDERER.
			Shader diffuseShader = Shader.Find("Sprites/Diffuse");

			//update shader on CaveLevel1
			L1_dirt2.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_mountains1.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_mountains2.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_base_out_atlas1.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_mountains3.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_hole.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_CaveDoor.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_terrain.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_mountains4.GetComponent<Renderer>().material.shader = diffuseShader;
			L1_obj_misk_atlas.GetComponent<Renderer>().material.shader = diffuseShader;

			//update shader on CaveLevel2
			L2_dirt2.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_terrain_atlas.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_mountains1.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_water.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_terrain_atlas2.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_base_out_atlas.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_cement.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_cementstair.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_mountains2.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_dirt22.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_mountains3.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_mountains4.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_terrain_atlas3.GetComponent<Renderer>().material.shader = diffuseShader;
			L2_base_out_atlas2.GetComponent<Renderer>().material.shader = diffuseShader;



		}
	}
}
