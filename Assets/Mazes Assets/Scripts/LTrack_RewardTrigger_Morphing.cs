using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RewardTrigger_Morphing : MonoBehaviour {
	
	public static string context = "'None'";
	public static string outcome = "'None'";
	public static float runningtrialtime = 0;
	public GameObject player;
	public float nextmaze;
	public static bool morphflag = false;
	
	// For imaging with morphing/switching
	public static int all_trial_num=0;
	public int currentrun = 1;
	public int[] run1 = {0,0,3,1,1,2,0,3,0,1,2,1,0,3,1,0,1,2,0,3,1,1,0,1,2,0,3,0,1,2};
	public int[] run2 = {1,2,0,3,0,1,2,1,0,1,3,0,2,0,1,3,1,2,0,1,3,1,0,2,1,0,1,0,3,0};
	public int[] run3 = {0,1,2,1,0,3,0,1,2,0,1,0,3,1,2,1,0,3,0,1,2,0,1,3,0,2,0,3,1,1};
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		
//		// Use this for random context assignment
//		nextmaze=rand.Next (0,2);
//		if (nextmaze == 1) {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	        player.transform.eulerAngles = originalRotation;
//			context = "'restart'";
//		}
//		else {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	       	player.transform.eulerAngles = originalRotation;
//			context = "'reward'";
//		}
		
		// Use this for pseudorandom assignment of mazes during imaging/switching
		if (currentrun == 1){
			nextmaze=run1[all_trial_num];
		}
		else if (currentrun == 2){
			nextmaze=run2[all_trial_num];
		}
		else if (currentrun == 3){
			nextmaze=run3[all_trial_num];
		}
		
		// Place player in proper maze
		if (nextmaze == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart'";
			all_trial_num=all_trial_num+1;
		}
		else if (nextmaze == 1) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward'";
			all_trial_num=all_trial_num+1;
		}
	}
	
 	void OnTriggerEnter (Collider col) {	
		if (morphflag == true){
    		outcome = "'restart'";
			morphflag = false;
		}
		else if (morphflag == false){
			outcome = "'correct'";
		}
	}
	
	//System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
//		// Use this for random context assignment
//		nextmaze=rand.Next (0,2);
//		if (nextmaze == 1) {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	        player.transform.eulerAngles = originalRotation;
//			context = "'restart'";
//		}
//		else {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	       	player.transform.eulerAngles = originalRotation;
//			context = "'reward'";
//		}
		
		// Use this for pseudorandom assignment of mazes during imaging/switching
		if (currentrun == 1){
			nextmaze=run1[all_trial_num];
		}
		else if (currentrun == 2){
			nextmaze=run2[all_trial_num];
		}
		else if (currentrun == 3){
			nextmaze=run3[all_trial_num];
		}
		
		// Place player in proper maze
		if (nextmaze == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart'";
			all_trial_num=all_trial_num+1;
		}
		else if (nextmaze == 1) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward'";
			all_trial_num=all_trial_num+1;
		}
		else if (nextmaze == 2) {
			morphflag = true;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart_morph'";
			all_trial_num=all_trial_num+1;
		}
		else if (nextmaze == 3) {
			morphflag = true;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward_morph'";
			all_trial_num=all_trial_num+1;
		}
		
		runningtrialtime=0;	
	}
	
	// Update is called once per frame
	void Update () {
		runningtrialtime=runningtrialtime+Time.deltaTime;
		print (player.transform.position.x);
		
		if (outcome == "'correct'") {
			runningtrialtime=0;
			StartCoroutine (DelayRestartMaze(5.0f));
		}
	}
}

