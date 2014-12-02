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
	public static int nextmaze;
	//static int Range(int min, int max);
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
	}
	
 	void OnTriggerEnter (Collider col) {	
		LTrack_RewardTrigger_Ambiguous.outcome = "'restart'";
		Transparent_Reward.pick_flag=false;
	}
	
	//System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//    	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
		// Use this for pseudorandom assignment of mazes during imaging/switching
		//nextmaze=rand.Next (2,4);
		nextmaze=UnityEngine.Random.Range(2,4);
		
		// Place player in proper maze
//		if (nextmaze == 0) {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	        player.transform.eulerAngles = originalRotation;
//			LTrack_RewardTrigger_Ambiguous.context = "'restart'";
//			LTrack_RewardTrigger_Ambiguous.ambiguousflag = false;
//			LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
//			audio.loop=true;
//			audio.clip=s_restart;
//			audio.Play ();
//			audio.volume = 1;
//		}
//		else if (nextmaze == 1) {
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	       	player.transform.eulerAngles = originalRotation;
//			LTrack_RewardTrigger_Ambiguous.context = "'reward'";
//			LTrack_RewardTrigger_Ambiguous.ambiguousflag = false;
//			LTrack_RewardTrigger_Ambiguous.all_trial_num=LTrack_RewardTrigger_Ambiguous.all_trial_num+1;
//			audio.loop=true;
//			audio.clip=s_reward;
//			audio.Play ();
//			audio.volume = 1;
//		}
		if (nextmaze == 2) {
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = true;
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
			LTrack_RewardTrigger_Ambiguous.ambiguousflag = true;
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

		
		LTrack_RewardTrigger_Ambiguous.runningtrialtime=0;
	}
	
	// Update is called once per frame
	void Update () {
		if (LTrack_RewardTrigger_Ambiguous.ambiguousflag == true) {
			if (LTrack_RewardTrigger_Ambiguous.lastmazeflag == true){ // if the previous maze was reward context
				// volume of other sound (if restart_ambiguous)
				if (player.transform.position.z <100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_reward;
					audio.Play ();
					audio.volume=Transparent_Reward.otherAlpha;
					firstplayflag = false;
				}
				else if (player.transform.position.z <100 && firstplayflag == false) {
					// volume of other sound (if restart_ambiguous)
					audio.volume=Transparent_Reward.otherAlpha;
				}
				// volume of other sound (if reward_ambiguous)
				else if (player.transform.position.z > 100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_restart;
					audio.Play ();
					audio.volume=Transparent_Reward.otherAlpha;
					firstplayflag = false;
				}
				else if (player.transform.position.z > 100 && firstplayflag == false) {
					audio.volume=Transparent_Reward.otherAlpha;
				}
			}
			else if (LTrack_RewardTrigger_Ambiguous.lastmazeflag == false){ // if the previous maze was restart context
				// volume of main sound
				audio.volume = 1F-Transparent_Reward.otherAlpha;
			}
		}
		
		if (LTrack_RewardTrigger_Ambiguous.outcome == "'correct'") {
			audio.Stop ();
			firstplayflag=true;
		}
		
		if (LTrack_RewardTrigger_Ambiguous.outcome == "'restart'") {
			StartCoroutine (DelayRestartMaze(5.0f));
			audio.Stop ();
			LTrack_RewardTrigger_Ambiguous.lastmazeflag=false;
			firstplayflag=true;
		}
	}
}

