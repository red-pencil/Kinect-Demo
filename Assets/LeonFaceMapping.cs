using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class LeonFaceMapping : MonoBehaviour {


	public enum players {unitychan_player_1, unitychan_player_2, player_0};
	public players matchedPlayer;

	private UnityChan.LeonFaceController conPlayer;

	public KeyCode[] faces;

	//public KeyCode smile1;
	//public KeyCode smile2;
	//public KeyCode confuse;
	//public KeyCode sap;
	//public KeyCode angry1;
	//public KeyCode angry2;
	//public KeyCode distract1;
	//public KeyCode distract2;
	//public KeyCode eyeClose;
	//public KeyCode superised;
	//public KeyCode mouthA;
	//public KeyCode mouthE;
	//public KeyCode mouthI;
	//public KeyCode mouthO;
	//public KeyCode mouthU;


	// Use this for initialization
	void Start () {

		string playerName = matchedPlayer.ToString();

		conPlayer = GameObject.Find (playerName).GetComponent<UnityChan.LeonFaceController> ();

	}
	
	// Update is called once per frame
	void Update () {


		int keyOrder;
		foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKey (vKey)) {
				Debug.Log (vKey); 
				keyOrder = Array.IndexOf (faces, vKey);
				//Debug.Log (keyOrder);

				if (keyOrder < 0) return;

				conPlayer.anim.CrossFade (conPlayer.animations [keyOrder].name, 0);

			}
		}



		
	}
}
