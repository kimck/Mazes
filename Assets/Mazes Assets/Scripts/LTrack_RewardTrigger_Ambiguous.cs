using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RewardTrigger_Ambiguous : MonoBehaviour {
	
	public static string context = "'None'";
	public static string outcome = "'None'";
	public static float runningtrialtime = 0;
	public GameObject player;
	public static float nextmaze;
	public static bool morphflag = false;
	public static bool lastmazeflag = true;
	public AudioClip s_reward; // assign these sounds in consol
	public AudioClip s_restart;
	public bool firstplayflag = true;
	public int nextpos;
	public static bool ambiguousflag = false;
	
	// For imaging with morphing/switching
	public static int all_trial_num=0;
	public int currentrun = 1;
	//public int[] run1 = {0,0,3,1,4,1,2,0,3,0,1,2,4,1,0,3,1,0,4,1,2,0,3,1,4,1,0,1,2,0,3,4,0,1,2};
	//public int[] run2 = {1,2,0,4,3,0,1,2,1,0,1,4,3,0,2,0,1,3,4,1,2,0,1,3,1,0,2,4,1,0,1,0,3,4,0};
	//public int[] run3 = {0,4,1,2,1,0,3,4,0,1,2,0,1,0,3,1,4,2,1,0,3,0,4,1,2,0,1,3,0,2,4,0,3,1,1};
	
	public int[] run1 = {0,0,1,4,1,0,0,1,4,1,0,1,0,4,1,0,1,4,1,0,1,0,4,0,1};
	public int[] run2 = {1,0,4,0,1,1,0,1,4,0,0,1,4,1,0,1,1,0,4,1,0,1,0,4,0};
	public int[] run3 = {0,4,1,1,0,4,0,1,0,1,0,1,4,1,0,0,4,1,0,1,0,4,0,1,1};
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		
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
			audio.loop=true;
			audio.clip=s_restart;
			audio.Play ();
		}
		else if (nextmaze == 1) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward'";
			all_trial_num=all_trial_num+1;
			audio.loop=true;
			audio.clip=s_reward;
			audio.Play ();
		}
	}
	
 	void OnTriggerEnter (Collider col) {	
		outcome = "'correct'";
	}
	
	System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
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
			morphflag=false;
			ambiguousflag = false;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart'";
			all_trial_num=all_trial_num+1;
			audio.loop=true;
			audio.clip=s_restart;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 1) {
			morphflag=false;
			ambiguousflag = false;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward'";
			all_trial_num=all_trial_num+1;
			audio.loop=true;
			audio.clip=s_reward;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 2) {
			morphflag = true;
			ambiguousflag = false;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart_ambiguous'";
			all_trial_num=all_trial_num+1;
			audio.loop=true;
			audio.clip=s_restart;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 3) {
			morphflag = true;
			ambiguousflag = false;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward_ambiguous'";
			all_trial_num=all_trial_num+1;
			audio.loop=true;
			audio.clip=s_reward;
			audio.Play ();
			audio.volume = 1;
		}
		
		else if (nextmaze == 4) {
			morphflag = true;
			ambiguousflag = true;
			nextpos=rand.Next (0,2);
			if (nextpos ==0) {
				Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
				Vector3 originalRotation = new Vector3(0,90,0);
				player.transform.position = originalPosition;
		       	player.transform.eulerAngles = originalRotation;
				context = "'ambiguous_reward'";
				all_trial_num=all_trial_num+1;
				audio.loop=true;
				audio.clip=s_reward;
				audio.Play ();
				audio.volume = 1;
			}
			else if (nextpos ==1) {
				Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
				Vector3 originalRotation = new Vector3(0,90,0);
				player.transform.position = originalPosition;
		        player.transform.eulerAngles = originalRotation;
				context = "'ambiguous_restart'";
				all_trial_num=all_trial_num+1;
				audio.loop=true;
				audio.clip=s_restart;
				audio.Play ();
				audio.volume = 1;
			}
		}
		
		runningtrialtime=0;	
	}
	
	// Update is called once per frame
	void Update () {
		runningtrialtime=runningtrialtime+Time.deltaTime;
		print (all_trial_num);
		
		if (morphflag == true && nextmaze !=4){
			if (lastmazeflag == true){
				// fade out starting volume
				audio.volume = 0.7F;
			}
			else if (lastmazeflag == false){
				// fade in
				if (player.transform.position.z <100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_reward;
					audio.Play ();
					audio.volume=0.3F;
					firstplayflag=false;
				}
				else if (player.transform.position.z <100 && firstplayflag == false) {
					audio.volume=0.3F;
				}
				//fade in
				else if (player.transform.position.z > 100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_restart;
					audio.Play ();
					audio.volume=0.3F;
					firstplayflag = false;
				}
				else if (player.transform.position.z > 100 && firstplayflag == false) {
					audio.volume=0.3F;
				}
			}
		}
		
		else if (morphflag == true && nextmaze ==4){
			if (lastmazeflag == true){
				// fade out starting volume
				audio.volume = 0.5F;
			}
			else if (lastmazeflag == false){
				// fade in
				if (player.transform.position.z <100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_reward;
					audio.Play ();
					audio.volume=0.5F;
					firstplayflag=false;
				}
				else if (player.transform.position.z <100 && firstplayflag == false) {
					audio.volume=0.5F;
				}
				//fade in
				else if (player.transform.position.z > 100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_restart;
					audio.Play ();
					audio.volume=0.5F;
					firstplayflag = false;
				}
				else if (player.transform.position.z > 100 && firstplayflag == false) {
					audio.volume=0.5F;
				}
			}
		}
		
		if (outcome == "'restart'") {
			audio.Stop ();
		}
		
		if (outcome == "'correct'") {
			StartCoroutine (DelayRestartMaze(5.0f));
			audio.Stop ();
			lastmazeflag=true;
			firstplayflag = true;
		}
	}
}

