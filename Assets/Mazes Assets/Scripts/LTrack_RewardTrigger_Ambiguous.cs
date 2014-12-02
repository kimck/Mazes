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
	public static int nextmaze;
	public static bool lastmazeflag = true;
	public AudioClip s_reward; // assign these sounds in consol
	public AudioClip s_restart;
	public bool firstplayflag = true;
	public int nextpos;
	public static bool ambiguousflag = false;
	//System.Random rand = new System.Random();
	//static int Range(int min, int max);

	
	// For imaging with morphing/switching
	public static int all_trial_num=0;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		
		// Use this for pseudorandom assignment of mazes during imaging/switching
		//nextmaze=rand.Next (0,2);
		nextmaze=UnityEngine.Random.Range(0,2);
		
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
		Transparent_Reward.pick_flag=false;
	}
		
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
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
//			ambiguousflag = false;
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	        player.transform.eulerAngles = originalRotation;
//			context = "'restart'";
//			all_trial_num=all_trial_num+1;
//			audio.loop=true;
//			audio.clip=s_restart;
//			audio.Play ();
//			audio.volume = 1;
//		}
//		else if (nextmaze == 1) {
//			ambiguousflag = false;
//			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
//			Vector3 originalRotation = new Vector3(0,90,0);
//			player.transform.position = originalPosition;
//	       	player.transform.eulerAngles = originalRotation;
//			context = "'reward'";
//			all_trial_num=all_trial_num+1;
//			audio.loop=true;
//			audio.clip=s_reward;
//			audio.Play ();
//			audio.volume = 1;
//		}
		
		if (nextmaze == 2) {
			ambiguousflag = true;
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
			ambiguousflag = true;
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
		
		runningtrialtime=0;	
	}
	
	// Update is called once per frame
	void Update () {
		runningtrialtime=runningtrialtime+Time.deltaTime;
		//print (ambiguousflag);
		print (all_trial_num);
		
		if (ambiguousflag == true){
			if (lastmazeflag == true){ // if the previous maze was also reward context
				// volume of main sound
				audio.volume = 1F-Transparent_Reward.otherAlpha;
			}
			else if (lastmazeflag == false){ // if the previous maze was restart context
				// volume of other sound (if restart_ambiguous)
				if (player.transform.position.z <100 && firstplayflag == true) {
					audio.loop=true;
					audio.clip=s_reward;
					audio.Play ();
					audio.volume=Transparent_Reward.otherAlpha;
					firstplayflag=false;
					//print(audio.volume);
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
					//print(audio.volume);
				}
				else if (player.transform.position.z > 100 && firstplayflag == false) {
					// volume of restart sound (if reward_ambiguous)
					audio.volume=Transparent_Reward.otherAlpha;
				}
			}
		}
		
		if (outcome == "'restart'") {
			audio.Stop ();
			firstplayflag = true;
		}
		
		if (outcome == "'correct'") {
			StartCoroutine (DelayRestartMaze(5.0f));
			audio.Stop ();
			lastmazeflag=true;
			firstplayflag = true;
		}
	}
}

