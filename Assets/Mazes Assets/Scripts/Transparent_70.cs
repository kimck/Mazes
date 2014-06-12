using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Transparent_70 : MonoBehaviour {

	public GameObject player;
	public float newAlpha;
	
	void Start() {
		player = GameObject.Find ("LiveMouseFPSController");
	}

    void Update() {
		
		if (LTrack_RewardTrigger_Ambiguous.morphflag == true && LTrack_RewardTrigger_Ambiguous.ambiguousflag == false){
			newAlpha = 0.7F;
		}
		else if (LTrack_RewardTrigger_Ambiguous.morphflag == true && LTrack_RewardTrigger_Ambiguous.ambiguousflag == true){
			newAlpha=0.5F;
		}
		else if (LTrack_RewardTrigger_Ambiguous.morphflag == false){
			newAlpha=1F;
		}
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha);
    }
}

