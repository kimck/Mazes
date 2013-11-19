using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RestartTrigger_Morphing : MonoBehaviour {
	
	public GameObject player;
	public float nextmaze;
	public int currentrun = 1;
	public int[] run1 = {0,0,3,1,1,2,0,3,0,1,2,1,0,3,1,0,1,2,0,3,1,1,0,1,2,0,3,0,1,2};
	public int[] run2 = {1,2,0,3,0,1,2,1,0,1,3,0,2,0,1,3,1,2,0,1,3,1,0,2,1,0,1,0,3,0};
	public int[] run3 = {0,1,2,1,0,3,0,1,2,0,1,0,3,1,2,1,0,3,0,1,2,0,1,3,0,2,0,3,1,1};

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
	}
	
 	void OnTriggerEnter (Collider col) {	
    	if (LTrack_RewardTrigger_Morphing.morphflag == true){
			LTrack_RewardTrigger_Morphing.outcome = "'correct'";
			LTrack_RewardTrigger_Morphing.morphflag = false;
		}
		else if (LTrack_RewardTrigger_Morphing.morphflag == false){
			LTrack_RewardTrigger_Morphing.outcome = "'restart'";
		}
	}
	
	//System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//    	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
//		// Use this for random context assignment		
//		nextmaze=rand.Next (0,2);
//		if (nextmaze == 0) {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	        player.transform.eulerAngles = originalRotation;
//			LTrack_RewardTrigger.context = "'restart'";
//		}
//		else {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	       	player.transform.eulerAngles = originalRotation;
//			LTrack_RewardTrigger.context = "'reward'";
//		}
		
		// Use this for pseudorandom assignment of mazes during imaging/switching
		if (currentrun == 1){
			nextmaze=run1[LTrack_RewardTrigger_Morphing.all_trial_num];
		}
		else if (currentrun == 2){
			nextmaze=run2[LTrack_RewardTrigger_Morphing.all_trial_num];
		}
		else if (currentrun == 3){
			nextmaze=run3[LTrack_RewardTrigger_Morphing.all_trial_num];
		}
		
		// Place player in proper maze
		if (nextmaze == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Morphing.context = "'restart'";
			LTrack_RewardTrigger_Morphing.all_trial_num=LTrack_RewardTrigger_Morphing.all_trial_num+1;
		}
		else if (nextmaze == 1) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Morphing.context = "'reward'";
			LTrack_RewardTrigger_Morphing.all_trial_num=LTrack_RewardTrigger_Morphing.all_trial_num+1;
		}
		else if (nextmaze == 2) {
			LTrack_RewardTrigger_Morphing.morphflag = true;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Morphing.context = "'restart_morph'";
			LTrack_RewardTrigger_Morphing.all_trial_num=LTrack_RewardTrigger_Morphing.all_trial_num+1;
		}
		else if (nextmaze == 3) {
			LTrack_RewardTrigger_Morphing.morphflag = true;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Morphing.context = "'reward_morph'";
			LTrack_RewardTrigger_Morphing.all_trial_num=LTrack_RewardTrigger_Morphing.all_trial_num+1;
		}
		
		LTrack_RewardTrigger_Morphing.runningtrialtime=0;
	}
	
	// Update is called once per frame
	void Update () {		
		if (LTrack_RewardTrigger_Morphing.outcome == "'restart'" || LTrack_RewardTrigger_Morphing.outcome == "'incorrect'") {
			LTrack_RewardTrigger_Morphing.runningtrialtime=0;
			StartCoroutine (DelayRestartMaze(5.0f));
		}
	}
}

