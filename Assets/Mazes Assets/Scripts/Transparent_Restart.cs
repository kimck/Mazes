using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Transparent_Restart : MonoBehaviour {

	public GameObject player;
	public float newAlpha;
	
	void Start() {
		player = GameObject.Find ("LiveMouseFPSController");
	}

    void Update() {
		
		if (LTrack_RewardTrigger_Ambiguous.ambiguousflag == false){
			newAlpha = 0.0F;
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);
		}
		else if (LTrack_RewardTrigger_Ambiguous.ambiguousflag == true){
			newAlpha=Transparent_Reward.otherAlpha;
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);
		}
    }
}

