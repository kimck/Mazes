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
	public float nextmaze;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		nextmaze=rand.Next (0,2);
		if (nextmaze == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart'";
		}
		else {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward'";
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
		
		nextmaze=rand.Next (0,2);
		if (nextmaze == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			context = "'restart'";
		}
		else {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			context = "'reward'";
		}
		
		runningtrialtime=0;	
	}
	
	// Update is called once per frame
	void Update () {
		runningtrialtime=runningtrialtime+Time.deltaTime;
		
		if (outcome == "'correct'") {
			runningtrialtime=0;
			StartCoroutine (DelayRestartMaze(5.0f));
		}
	}
}

