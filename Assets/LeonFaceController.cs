using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityChan
{
	public class LeonFaceController : MonoBehaviour
	{
		public AnimationClip[] animations;
		public Animator anim;
		public float delayWeight;
		public bool isKeepFace = false;


		public static float MicLoudness;

		private string _device;

		// custom keys

		private AutoBlink autoBlinkInfo;
		public bool _autoBlinkON;

		private LeonGesture gestureInfo;
		public bool _realBackgroundON;

		private OVRLipSyncMicInput mouthSyncInfo;
		//public string[] faceMapping = new string[10];

		public KeyCode smile;
		public KeyCode angry;
		public KeyCode eyeClose;


		void Start ()
		{
			anim = GetComponent<Animator> ();
			autoBlinkInfo = GetComponent<AutoBlink> ();
			gestureInfo = GameObject.Find ("EventSystem").GetComponent<LeonGesture> ();
			mouthSyncInfo = GetComponent<OVRLipSyncMicInput> ();

		}

		//void OnGUI ()
		//{
		//	GUILayout.Box ("Face Update", GUILayout.Width (170), GUILayout.Height (25 * (animations.Length + 2)));
		//	Rect screenRect = new Rect (10, 25, 150, 25 * (animations.Length + 1));
		//	GUILayout.BeginArea (screenRect);
		//	foreach (var animation in animations) {
		//		if (GUILayout.RepeatButton (animation.name)) {
		//			anim.CrossFade (animation.name, 0);
		//		}
		///	}
		//	isKeepFace = GUILayout.Toggle (isKeepFace, " Keep Face");
		//	GUILayout.EndArea ();
		//}

		float current = 0;

		bool  _isSpeaking = false;
		public bool  _speakMode = false;
		bool _longPress = false;

		void Update ()
		{
			// levelMax equals to the highest normalized value power 2, a small number because < 1
			// pass the value to a static var so we can access it from anywhere


				current = 0;
				//MicLoudness = LevelMax ();
				//Debug.Log (MicLoudness);

			//if (MicLoudness > -50) {

			//	_speakMode = true;

			//} else {
			//	_speakMode = false;
			//}
				

			if (Input.GetKeyDown (KeyCode.KeypadMultiply)) {

				anim.CrossFade ("JMP00", 0);

			}

			//Debug.Log (_isSpeaking);

			//if (_isSpeaking) {
				//anim.CrossFade ("MTH_O", 0);
			//}
				
			if (Input.GetKeyDown (KeyCode.KeypadPeriod))
				_longPress = !_longPress;

			if (_longPress) {
				
				if (Input.GetKey (KeyCode.KeypadEnter) || Input.GetKey (KeyCode.Space)) {
					isKeepFace = true;
				} else {
					isKeepFace = false;
				}

			} else {
				
				if (Input.GetKeyDown (KeyCode.KeypadPlus))
					isKeepFace = !isKeepFace;

			}


			if (Input.GetKeyDown (KeyCode.KeypadMultiply)) _autoBlinkON = !_autoBlinkON;
			if (Input.GetKeyDown (KeyCode.KeypadMinus)) _realBackgroundON = !_realBackgroundON;

			autoBlinkInfo.isActive = _autoBlinkON;
			gestureInfo.realScene.SetActive (_realBackgroundON);


			//if (Input.GetMouseButton (0)) {
			//	current = 1;
			//} else if (!isKeepFace) {
			//	current = Mathf.Lerp (current, 0, delayWeight);
			//}

			if (Input.GetKeyDown (eyeClose)) anim.CrossFade ("eye_close@unitychan", 0);
			else if (Input.GetKeyDown (smile)) anim.CrossFade ("smile1@unitychan", 0);
			else if (Input.GetKeyDown (angry)) anim.CrossFade ("angry1@unitychan", 0);

			if (Input.anyKeyDown) {

				current = 1;

			} else if (!isKeepFace) {
				
				current = Mathf.Lerp (current, 0, delayWeight);
			}

			anim.SetLayerWeight (1, current);
		}


		//アニメーションEvents側につける表情切り替え用イベントコール
		public void OnCallChangeFace (string str)
		{   
			int ichecked = 0;
			foreach (var animation in animations) {
				if (str == animation.name) {
					ChangeFace (str);
					break;
				} else if (ichecked <= animations.Length) {
					ichecked++;
				} else {
					//str指定が間違っている時にはデフォルトで
					str = "default@unitychan";
					ChangeFace (str);
				}
			} 
		}

		void ChangeFace (string str)
		{
			isKeepFace = true;
			current = 1;
			anim.CrossFade (str, 0);
		}



		/// <summary>
		/// VOLUME CONTROL
		/// </summary>


 
        //mic initialization
        void InitMic(){
			if (Microphone.devices.Length > 0) {
				Debug.Log ("microphone ON" + Microphone.devices.Length );
			} else {
				Debug.Log ("microphone OFF");
			}
            if(_device == null) _device = Microphone.devices[0];
			_clipRecord = Microphone.Start(_device, true, 999, 44100);
        }
 
        void StopMicrophone()
        {
            Microphone.End(_device);
        } 
 
        AudioClip _clipRecord = new AudioClip();
        int _sampleWindow = 128;
 
        //get data from microphone into audioclip
        float LevelMax()
        {
            float levelMax = 0;
            float[] waveData = new float[_sampleWindow];
            int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
            if (micPosition < 0) return 0;
            _clipRecord.GetData(waveData, micPosition);
            // Getting a peak on the last 128 samples
            for (int i = 0; i < _sampleWindow; i++) {
                float wavePeak = waveData[i] * waveData[i];
                if (levelMax < wavePeak) {
                    levelMax = wavePeak;
                }
            }

			float db = 20 * Mathf.Log10 (Mathf.Abs (levelMax));

            return db;
        }
 
 
 
        bool _isInitialized;
        // start mic when scene starts
        void OnEnable()
        {
            InitMic();
            _isInitialized=true;
        }

		//void OnGUI ()
		//{
		//	Rect rect1 = new Rect (10, Screen.height - 40, 400, 30);
		//	_speakMode = GUI.Toggle (rect1, _speakMode, "Speaking Mode");
		//}


	}
}
