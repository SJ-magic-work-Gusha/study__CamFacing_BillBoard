/************************************************************
■参考
	Github
		https://github.com/SJ-magic-study-unity/study__UnityFixFramerate
		
	UnityでFPSを設定する方法
		http://unityleaning.blog.fc2.com/blog-entry-2.html
************************************************************/
using UnityEngine;
using System.Collections;

/************************************************************
************************************************************/
public class setFrameRate : MonoBehaviour {
	
	[SerializeField]	int FrameRate = 60;
	
	KeyCode Key_DispFrameRate = KeyCode.F;
	private string label = "saijo";
	bool b_DispFrameRate = false;
	
	void Awake() { 
		QualitySettings.vSyncCount = 0; // Don't Sync
		Application.targetFrameRate = FrameRate;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(Key_DispFrameRate)){
			b_DispFrameRate = !b_DispFrameRate;
		}
		
		float fps = 1.0f / Time.deltaTime;
		label = string.Format("{0:000.0}", (int)(fps + 0.5f));
	}
	
	/****************************************
	****************************************/
	void OnGUI()
	{
		// GUI.color = Color.white;
		GUI.color = Color.red;
		
		/********************
		********************/
		if(b_DispFrameRate) GUI.Label(new Rect(50, 20, 500, 30), label);
	}
}
