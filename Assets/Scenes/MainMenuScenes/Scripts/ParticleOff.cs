using UnityEngine;
using System.Collections;

public class ParticleOff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem> ().Stop ();
	}
}
