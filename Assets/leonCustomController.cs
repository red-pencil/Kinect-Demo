using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leonCustomController : MonoBehaviour {
	
	new public GameObject avatarEyeL;
	new public GameObject avatarEyeR;

	public GameObject realScene;
	public bool _realSceneON;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//_realSceneON = realScene.GetComponent<ForegroundToRawImage> ().isActiveAndEnabled;
		_realSceneON = realScene.activeSelf;

		if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			
			realScene.SetActive(!_realSceneON);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {

			avatarEyeL.SetActive (!avatarEyeL.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {

			avatarEyeR.SetActive (!avatarEyeR.activeSelf);
		}


	}
}
