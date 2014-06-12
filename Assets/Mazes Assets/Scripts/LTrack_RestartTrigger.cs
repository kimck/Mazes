using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RestartTrigger : MonoBehaviour {
	
	public GameObject player;
	public float nextmaze;
	public AudioClip s_reward; // assign these sounds in consol
	public AudioClip s_restart;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
	}
	
 	void OnTriggerEnter (Collider col) {
    	LTrack_RewardTrigger.outcome = "'restart'";
	}
	
	System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//    	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		audio.Stop ();
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
		nextmaze=rand.Next (0,2);
		if (nextmaze ==0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger.context = "'restart'";
			LTrack_RewardTrigger.all_trial_num=LTrack_RewardTrigger.all_trial_num+1;
			audio.loop=true;
			audio.clip=s_restart;
			audio.Play ();
		}
		else {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger.context = "'reward'";
			LTrack_RewardTrigger.all_trial_num=LTrack_RewardTrigger.all_trial_num+1;
			audio.loop=true;
			audio.clip=s_reward;
			audio.Play ();
		}
		
		LTrack_RewardTrigger.runningtrialtime=0;
	}
	
	// Update is called once per frame
	void Update () {
		if (LTrack_RewardTrigger.outcome == "'restart'") {
			StartCoroutine (DelayRestartMaze(5.0f));
		}
		if (LTrack_RewardTrigger.outcome == "'correct'") {
			audio.Stop ();
		}
	}
}

