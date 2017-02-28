/* Team Python          Last edit made by: Stephen Galyas           Created on: December 1, 2016         Last modified on: December 18, 2016            Published on: December 19 21, 2016
 * This script creates torches to be placed in the Cave.
*/

using UnityEngine;
using System.Collections;

public class TorchCreatorScriptCave1 : MonoBehaviour {

    /// <summary>
    /// A reference to the "light" which is attached to a torch.
    /// </summary>
    public Texture lightCookie;

    int torchNameCounter = 1;

    public Sprite torchSprite;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKey(KeyCode.P) && CaveGodScript.torchCount > 0) // The player wishes to place a torch on the level.
        {
            GameObject lightGameObject = new GameObject("NewLight" +torchNameCounter);
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.type = LightType.Spot;
            lightComp.cookie = lightCookie;
            lightComp.range = 100;
            lightComp.spotAngle = 100;
            lightComp.intensity = 8;
            lightComp.bounceIntensity = 0;
            lightGameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,-60);

            GameObject torchGameObject = new GameObject("NewTorch" + torchNameCounter);
            torchGameObject.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteR = torchGameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = torchSprite;
            torchGameObject.transform.localScale = new Vector3(100, 100, 1);

            torchGameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
            spriteR.sortingOrder = 9;

            torchNameCounter++;
            CaveGodScript.torchCount--;
            System.Threading.Thread.Sleep(150);


        }
	}
}
