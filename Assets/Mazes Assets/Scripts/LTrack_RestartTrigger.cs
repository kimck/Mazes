using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class LTrack_RestartTrigger : MonoBehaviour {
	
	public GameObject player;

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
		Vector3 blackboxPosition = new Vector3(300,1.1f,100);
		player.transform.position = blackboxPosition;
		
		yield return new WaitForSeconds (delaytime);
		
		if (rand.Next(0, 2) == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger.context = "'restart'";
		}
		else {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	       	player.transform.eulerAngles = originalRotation;
			LTrack_RewardTrigger.context = "'reward'";
		}
		
		LTrack_RewardTrigger.runningtrialtime=0;
	}
	
	// Update is called once per frame
	void Update () {		
		if (LTrack_RewardTrigger.outcome == "'restart'" || LTrack_RewardTrigger.outcome == "'incorrect'") {
			LTrack_RewardTrigger.runningtrialtime=0;
			StartCoroutine (DelayRestartMaze(5.0f));
		}
	}
}

