using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RestartTrigger_Ambiguous : MonoBehaviour {
	
	public GameObject player;
	public AudioClip s_reward; // assign these sounds in consol
	public AudioClip s_restart;
	public bool firstplayflag = true;
	public int nextpos;
	public static float nextmaze;
	
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
	}
	
 	void OnTriggerEnter (Collider col) {	
		LTrack_RewardTrigger_Ambiguous.outcome = "'restart'";
	}
	
	System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//    	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
		// Use this for pseudorandom assignment of mazes during imaging/switching
		if (currentrun == 1){
			nextmaze=run1[LTrack_RewardTrigger_Ambiguous.all_trial_num];
		}
		else if (currentrun == 2){
			nextmaze=run2[LTrack_RewardTrigger_Ambiguous.all_trial_num];
		}
		else if (currentrun == 3){
			nextmaze=run3[LTrack_RewardTrigger_Ambiguous.all_trial_num];
		}
		
		// Place player in proper maze
		if (nextmaze == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Ambiguous.context = "'restart'";
			LTrack_RewardTrigger_Ambiguous.morphflag = false;
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = false;
			LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
			audio.loop=true;
			audio.clip=s_restart;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 1) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Ambiguous.context = "'reward'";
			LTrack_RewardTrigger_Ambiguous.morphflag = false;
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = false;
			LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
			audio.loop=true;
			audio.clip=s_reward;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 2) {
			LTrack_RewardTrigger_Ambiguous.morphflag = true;
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = false;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Ambiguous.context = "'restart_ambiguous'";
			LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
			audio.loop=true;
			audio.clip=s_restart;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 3) {
			LTrack_RewardTrigger_Ambiguous.morphflag = true;
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = false;
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger_Ambiguous.context = "'reward_ambiguous'";
			LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
			audio.loop=true;
			audio.clip=s_reward;
			audio.Play ();
			audio.volume = 1;
		}
		else if (nextmaze == 4) {
			LTrack_RewardTrigger_Ambiguous.morphflag = true;
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = true;
			nextpos=rand.Next (0,2);
			if (nextpos ==1) {
				Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
				Vector3 originalRotation = new Vector3(0,90,0);
				player.transform.position = originalPosition;
		       	player.transform.eulerAngles = originalRotation;
				LTrack_RewardTrigger_Ambiguous.context = "'ambiguous_reward'";
				LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
				audio.loop=true;
				audio.clip=s_reward;
				audio.Play ();
				audio.volume = 1;
			}
			else if (nextpos ==0) {
				LTrack_RewardTrigger_Ambiguous.morphflag = true;
				LTrack_RewardTrigger_Ambiguous.ambiguousflag = true;
				Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
				Vector3 originalRotation = new Vector3(0,90,0);
				player.transform.position = originalPosition;
		        player.transform.eulerAngles = originalRotation;
				LTrack_RewardTrigger_Ambiguous.context = "'ambiguous_restart'";
				LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
				audio.loop=true;
				audio.clip=s_restart;
				audio.Play ();
				audio.volume = 1;
			}
		}
		
		LTrack_RewardTrigger_Ambiguous.runningtrialtime=0;
	}
	
	// Update is called once per frame
	void Update () {
		if (LTrack_RewardTrigger_Ambiguous.morphflag == true && nextmaze !=4) {
			if (LTrack_RewardTrigger_Ambiguous.lastmazeflag == true){
				// play other sound at 30% volume
				if (player.transform.position.z <100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_reward;
					audio.Play ();
					audio.volume=0.3F;
					firstplayflag = false;
				}
				else if (player.transform.position.z <100 && firstplayflag == false) {
					audio.volume=0.3F;
				}
				// play other sound at 30% opacity
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
			else if (LTrack_RewardTrigger_Ambiguous.lastmazeflag == false){
				//fade out
				audio.volume = 0.7F;
			}
		}
		
		else if (LTrack_RewardTrigger_Ambiguous.morphflag == true && nextmaze ==4) {
			if (LTrack_RewardTrigger_Ambiguous.lastmazeflag == true){
				// play other sound at 30% volume
				if (player.transform.position.z <100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_reward;
					audio.Play ();
					audio.volume=0.5F;
					firstplayflag = false;
				}
				else if (player.transform.position.z <100 && firstplayflag == false) {
					audio.volume=0.5F;
				}
				// play other sound at 30% opacity
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
			else if (LTrack_RewardTrigger_Ambiguous.lastmazeflag == false){
				//fade out
				audio.volume = 0.5F;
			}
		}
		
		if (LTrack_RewardTrigger_Ambiguous.outcome == "'correct'") {
			audio.Stop ();
		}
		
		if (LTrack_RewardTrigger_Ambiguous.outcome == "'restart'") {
			StartCoroutine (DelayRestartMaze(5.0f));
			audio.Stop ();
			LTrack_RewardTrigger_Ambiguous.lastmazeflag=false;
			firstplayflag=true;
		}
	}
}

