using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RewardTrigger : MonoBehaviour {
	
	public static string context = "'None'";
	public static string outcome = "'None'";
	public static float runningtrialtime = 0;
	public GameObject player;
	public int nextmaze;
	public static int all_trial_num=0;
	public AudioClip s_reward; // assign these sounds in consol
	public AudioClip s_restart;
	public float counter = 0;
	//System.Random rand = new System.Random();
	//public int Range(int min, int max);

	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		//nextmaze=rand.Next (0,2);
		nextmaze=UnityEngine.Random.Range(0,2);
		if (nextmaze==1) {
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
		else {
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
	
	
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		audio.Stop ();
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
		//nextmaze=rand.Next (0,2);
		nextmaze=UnityEngine.Random.Range(0,2);
//		nextmaze=1;
//		if (counter==3) {
//			counter=0;
//			nextmaze=0;
//		}
		if (nextmaze==0) {
			
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
		else {
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
		
		runningtrialtime=0;
		counter=counter+1;
	}
	
	// Update is called once per frame
	void Update () {
		if (outcome == "'correct'") {
			StartCoroutine (DelayRestartMaze(5.0f));
		}
		runningtrialtime=runningtrialtime+Time.deltaTime;
		print (all_trial_num);
		if (outcome == "'restart'") {
			audio.Stop ();
		}
	}
}

