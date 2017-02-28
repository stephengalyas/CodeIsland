using UnityEngine;
using System.Collections;

public class CanvasLayerSort: MonoBehaviour {


	public string LayerName = "LayerName";

	public void Start(){

		GetComponent<Canvas>().sortingLayerName = LayerName;
	}


}
