using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class CorrectChoiceTrigger : MonoBehaviour {
	
	public static string choice = "'None'";
	public static float runningtrialtime = 0;
	public GameObject player;
	public float timeout = 100000000;
	public float nextmaze;

	// Use this for initialization
	void Start () {
		nextmaze=rand.Next (0,2);
		if (nextmaze == 1) {
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
	}
	
 	void OnTriggerEnter (Collider col) {	
    	choice = "'correct'";
	}
	
	System.Random rand = new System.Random();
	
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
//	}
	
	IEnumerator DelayRestartMaze (float delaytime) {
		
		player = GameObject.Find ("LiveMouseFPSController"); //sets position of player+camera to beginning of maze
		
		Vector3 correctchoicePosition = new Vector3(5,1.1f,15);
		player.transform.position = correctchoicePosition; //places player in black
		
		yield return new WaitForSeconds (delaytime);
		
		nextmaze=rand.Next (0,2);
		if (nextmaze == 1) {
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
		
		runningtrialtime=0;
			
	}
	
	
	// Update is called once per frame
	void Update () {
		runningtrialtime=runningtrialtime+Time.deltaTime;	
		if (choice == "'correct'") {
			runningtrialtime=0;
			StartCoroutine (DelayRestartMaze(2.0f));
		}
		
		if (runningtrialtime > timeout) {
			choice = "'timeout'";
			runningtrialtime=0;
			StartCoroutine (DelayRestartMaze (6.0f));
		}
		
	}
}

