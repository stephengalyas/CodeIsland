using UnityEngine;
using System.Collections;

public class ParticleLayerSort : MonoBehaviour {
	

	public string LayerName = "LayerName";

	public void Start(){

		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = LayerName;
	}


}
