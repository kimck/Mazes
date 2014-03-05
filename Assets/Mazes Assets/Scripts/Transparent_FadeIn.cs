using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Transparent_FadeIn: MonoBehaviour {

	public GameObject player;
	public float newAlpha;
	
	void Start() {
		player = GameObject.Find ("LiveMouseFPSController");
	}

    void Update() {
		
		if (LTrack_RewardTrigger_Morphing.morphflag == true){
			newAlpha=(player.transform.position.x)/133.3F;
		}
		else if (LTrack_RewardTrigger_Morphing.morphflag == false){
			newAlpha=0.5F/133.3F;
		}
		
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);

    }
}
