using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Transparent : MonoBehaviour {

    public float fadeLerpConstant=1F;
	public GameObject player;
	public float t;
	
	void Start() {
		player = GameObject.Find ("LiveMouseFPSController");
	}

    void Update() {
		t=(133.3F-player.transform.position.x)/133.3F;
		//renderer.material.color = Color.Lerp(renderer.material.color, Color.clear, fadeLerpConstant);
		//float newAlpha = Mathf.Lerp(renderer.material.color.a, 0F, t * fadeLerpConstant);
		float newAlpha = t;
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);
    }
}

