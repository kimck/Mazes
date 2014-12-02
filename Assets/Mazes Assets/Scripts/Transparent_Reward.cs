using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Transparent_Reward: MonoBehaviour {

	public GameObject player;
	public float newAlpha_reward;
	public static float otherAlpha;
	//System.Random rand = new System.Random();
	//static int Range(int min, int max);
	public static bool pick_flag=false;
	
	void Start() {
		player = GameObject.Find ("LiveMouseFPSController");
	}

    void Update() {
		//print (LTrack_RewardTrigger_Ambiguous.ambiguousflag);
		if (LTrack_RewardTrigger_Ambiguous.ambiguousflag == true && pick_flag == false){
		//if (LTrack_RewardTrigger_Ambiguous.ambiguousflag == true){
			//newAlpha_reward=rand.Next (50,100)/100F;
			newAlpha_reward=UnityEngine.Random.Range(60,100)/100F;
			//print (LTrack_RewardTrigger_Ambiguous.ambiguousflag);
			//print (newAlpha_reward);
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha_reward);
			otherAlpha=1.0F-newAlpha_reward;
			pick_flag = true;
		}
		
		else if (LTrack_RewardTrigger_Ambiguous.ambiguousflag == false){
			newAlpha_reward=1.0F;
			otherAlpha=0.0F;
			//print (LTrack_RewardTrigger_Ambiguous.ambiguousflag);
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, newAlpha_reward);
		}
		//print (newAlpha_reward);
    }
}

