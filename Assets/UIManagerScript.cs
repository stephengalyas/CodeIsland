using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour {

	public GameObject CodePanel;
	private Animator codePanelAnimator;
	// Use this for initialization
	void Start () {
		codePanelAnimator = CodePanel.GetComponent<Animator> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Tab) && codePanelAnimator.GetCurrentAnimatorStateInfo (0).IsName ("CodePanelClosed")) {
			codePanelAnimator.Play ("CodePanelSlideInLeft");
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			PlayerGod.canMove = false;
			System.Threading.Thread.Sleep (200);
		}
		if (Input.GetKey (KeyCode.Tab) && codePanelAnimator.GetCurrentAnimatorStateInfo (0).IsName ("CodePanelOpen")) {
			codePanelAnimator.Play ("CodePanelSlideOutRight");
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			PlayerGod.canMove = true;
			System.Threading.Thread.Sleep (200);
		}

		
	}
}
