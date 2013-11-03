using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class IncorrectChoiceTrigger : MonoBehaviour {
	
	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
 	void OnTriggerEnter (Collider col) {	
    	CorrectChoiceTrigger.choice = "'incorrect'";
	}
	
	
	System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//    	isTriggered = false;
//	}
	
	
	IEnumerator DelayRestartMaze (float delaytime) {
		
		player = GameObject.Find ("LiveMouseFPSController"); //sets position of player+camera to beginning of maze
		
		Vector3 correctchoicePosition = new Vector3(5,1.1f,15);
		player.transform.position = correctchoicePosition; //places player in black
		
		yield return new WaitForSeconds (delaytime);
		
		if (rand.Next(0, 2) == 0) {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition;
	        player.transform.eulerAngles = originalRotation;
		}
		else {
			Vector3 originalPosition = new Vector3(0.5f,1.1f,200); 
			Vector3 originalRotation = new Vector3(0,90,0);
			player.transform.position = originalPosition; //sets position of player+camera to beginning of maze
	       	player.transform.eulerAngles = originalRotation; //sets rotation of player+camera to 90 degrees
		}
		
		CorrectChoiceTrigger.runningtrialtime=0;
			
	}
	
	// Update is called once per frame
	void Update () {	
		
		if (CorrectChoiceTrigger.choice == "'incorrect'") {
			//audio.Play();
			CorrectChoiceTrigger.runningtrialtime=0;
			StartCoroutine (DelayRestartMaze(6.0f));
		}
		

	}
}

