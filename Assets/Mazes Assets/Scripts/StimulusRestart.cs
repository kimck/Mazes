using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class StimulusRestart : MonoBehaviour {
	
	public GameObject player;
	public GameObject bartrig;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		bartrig = GameObject.Find ("BarrierTrigger");
	}
	
 	void OnTriggerEnter (Collider col) {
		bartrig.audio.Stop ();
		Vector3 originalPosition = new Vector3(0.5f,1.1f,0);
		Vector3 originalRotation = new Vector3(0,90,0);
		player.transform.position = originalPosition;
	    player.transform.eulerAngles = originalRotation;
		Stimulus.nextpos = true;
	}
	
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
//	}
	
	// Update is called once per frame
	void Update () {
	}
}

