/************************************************************
URLs
	[Quaternion Basics]
		■四元数と三次元空間における回転
			https://mathtrain.jp/quaternion
			
		■クォータニオン(四元数)
			https://wgld.org/d/webgl/w031.html
		
	[Cam Face Billboard]
		■Local Axis LookAt
			https://forum.unity.com/threads/local-axis-lookat.445054/
			
			Contents
				同じ悩みを解決しているやりとり。非常に参考になった
			
		■ビルボードで常にカメラの方に向く木を作る
			http://nn-hokuson.hatenablog.com/entry/2017/03/24/211211
			
			Contents
				transform.LookAt を使った方法。惜しい。
			
		■シンプルなビルボードを実現する
			http://neareal.net/index.php?ComputerGraphics%2FUnity%2FTips%2FSimpleBillboard
			
			Contents
				同じく、惜しい
			
	[Fake Volumetric Light in Unity]
		そもそも解決したい課題は、これだった。
		Queries are "Volmetric Light, Light shaft, Sun shaft"
	
		■Faking Volume Light Tutorial in Unity 3d	
			https://www.youtube.com/watch?v=Wnf4VeDT7yY
	
			contents
				3:00-
				using boillboard.
************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************
************************************************************/
public class CamFacing_BillBoard : MonoBehaviour {
	/****************************************
	****************************************/
	private Transform _transform;
	private Transform _cameraTransform;
	private Quaternion _initialRotation;
	
	private bool b_rotX = false;


	/****************************************
	****************************************/
	
	/******************************
	******************************/
	void Awake()
	{
		_transform = transform;
		_cameraTransform = Camera.main.transform;
		_initialRotation = _transform.localRotation;
	}

	/******************************
	******************************/
	void Start () {
		
	}
	
	/******************************
	******************************/
	void Update()
	{
		/********************
		********************/
		if(Input.GetKeyDown(KeyCode.Alpha0))		b_rotX = true;
		else if(Input.GetKeyDown(KeyCode.Alpha1))	b_rotX = false;
		
		/********************
		********************/
		Vector3 LastTransform_fwd = _transform.forward;
		Quaternion LastTransform_rot = _transform.rotation;
		
		/********************
		********************/
		float T = 10.0f;
		float rot_x;
		float rot_z;
		
		if(b_rotX){
			rot_x = 60 * Mathf.Cos(2 * Mathf.PI * (Time.realtimeSinceStartup - 0.0f/360f * T) / T);
			rot_z = 0; // 60 * Mathf.Sin(2 * Mathf.PI * (Time.realtimeSinceStartup - 0.0f/360f * T) / T);
		}else{
			rot_x = 0; // 60 * Mathf.Cos(2 * Mathf.PI * (Time.realtimeSinceStartup - 0.0f/360f * T) / T);
			rot_z = 60 * Mathf.Sin(2 * Mathf.PI * (Time.realtimeSinceStartup - 0.0f/360f * T) / T);
		}
		
		Quaternion AnimRot = Quaternion.Euler(rot_x, 0, rot_z);
		
		// _initialRotationで、座標系をSceneViewで調整したinitialの姿勢へ
		// AnimRotで、initialの姿勢からさらにAnimate
		_transform.localRotation = _initialRotation * AnimRot;
		
		/********************
		以下、Globalで計算
		********************/
		Vector3 direction = _cameraTransform.position - _transform.position;
		float dot = Vector3.Dot(direction, _transform.forward);
		
		Vector3 fwd;
		if(dot == 0){
			fwd = LastTransform_fwd; // 射影してfwd vectorを算出することができないので、前回値を使用.
		}else{
			fwd = Vector3.ProjectOnPlane(direction, _transform.up);
		}
		Quaternion q_Final = Quaternion.LookRotation(fwd, _transform.up); // forwardとupを決めると、(直交)座標系は1意に決まる.
		
		// _transform.rotation = q_Final; // Globalで計算しているので、localRotation ではない点に注意.
		_transform.rotation = Quaternion.Slerp(LastTransform_rot, q_Final, 0.4f); // Globalで計算しているので、localRotation ではない点に注意.
	}
}
