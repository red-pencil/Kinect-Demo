using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonGesture : MonoBehaviour {


	private CubeGestureListener gestureListener;

	new public GameObject avatarEyeL;
	new public GameObject avatarEyeR;

	public bool _eyeChangeWithKeys;
	public bool _eyeChangeWithGestures;

	public GameObject realScene;
	public bool _realSceneON;

	// Use this for initialization
	void Start () {

		gestureListener = CubeGestureListener.Instance;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(!gestureListener)
			return;

		_realSceneON = realScene.activeSelf;

		if(_eyeChangeWithKeys)
		{
			if(Input.GetKeyDown(KeyCode.Keypad1))
				eyeBlinkLeft();
			else if(Input.GetKeyDown(KeyCode.Keypad3))
				eyeBlinkRight();
			else if(Input.GetKeyDown(KeyCode.Keypad0))
				realScene.SetActive(!_realSceneON);
		}

		if(_eyeChangeWithGestures && gestureListener)
		{
			if(gestureListener.IsSwipeLeft())
				eyeBlinkLeft();
			else if(gestureListener.IsSwipeRight())
				eyeBlinkRight();
		}

	}

	private void eyeBlinkLeft() {
		avatarEyeL.SetActive (!avatarEyeL.activeSelf);
	}

	private void eyeBlinkRight() {
		avatarEyeR.SetActive (!avatarEyeR.activeSelf);
	}

}
