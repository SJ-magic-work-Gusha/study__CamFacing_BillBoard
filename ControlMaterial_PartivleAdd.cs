/************************************************************
note
	照明の表現は、
		Standard/Fade
	で行っていて、Light shadftのmaterialについても、当初、これを使っていた。
	しかし、
		- Blackの時にLight shaft形状の影ができてしまう
		- Light shaftは、チリ、ホコリに光が当たって見える現象であり、Bloomの効果は、必要ない
	と言う理由から、
		Particles/Additive
	をtest.
	
	この場合、本scriptのように、_TintColorをControlすること。
	
	
	> Design視点での結果
		Particles/Additiveの方が、より自然に見えるので、こちらを採用.
		これに伴い、Make_LightShaftImage(oF)で作成するtextureは、より濃いめが最適であることもわかった。
			具体的には、
				exe 5(!b_Random) + 5(!b_Random) + 5(b_Random)
			くらいやっても大丈夫。
			あとは適宜、調整。
************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************
************************************************************/
public class ControlMaterial_PartivleAdd : MonoBehaviour {
	[SerializeField] GameObject GO;
	Material sharedMaterial;

	// Use this for initialization
	void Start () {
		sharedMaterial = GO.GetComponent<Renderer>().sharedMaterial;
	}
	
	/******************************
	******************************/
	void test () {
		float T = 3.0f;
		float val = 1.0f * (Mathf.Sin(2.0f * Mathf.PI * Time.time / T) + 1.0f) / 2.0f;
		// label =	string.Format("{0:0.000000}",	val);
		
		/********************
		********************/
		Color color = new Color(val, 0, 0, 1.0f);
		Set_TintColor(ref sharedMaterial, ref color);
	}
	
	/******************************
	******************************/
	void Update () {
		// test();
	}
	
	/******************************
	description
		colorのapplyは、以下のどちらでもok.
		後者の方が柔軟.
			sharedMaterial.color = color;
			sharedMaterial.SetColor("_Color", color);
	******************************/
	void SetColor(ref Material material, ref Color color){
		// material.color = color;
		material.SetColor("_Color", color);
	}
	
	/******************************
	description
		colorのapplyは、以下のどちらでもok.
		後者の方が柔軟.
			material.color = color;
			material.SetColor("_Color", color);
	******************************/
	void Set_TintColor(ref Material material, ref Color color){
		material.SetColor("_TintColor", color);
	}
	
}
