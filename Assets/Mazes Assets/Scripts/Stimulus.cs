using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Stimulus : MonoBehaviour {
	
	// logging information
	public static string stim = "'None'";
	public static string odor = "'None'";
	
	// only if you want a trial timeout
	public static float runningtrialtime = 0;
	public float timeout = 100;
	
	// for randomly assigning next stimuli
	//public float nexttone;
	//public float nextvis;
	//public float nextodor;
	public float nexttrial;
	System.Random rand = new System.Random();
	
	// for randomly assigning location for stim
	public GameObject player;
	public static bool nextpos = true;
	public float xpos;
	
	// visual stimuli
	public Material v1wallleft; // assign these materials in console
	public Material v1wallright;
	public Material v2wallleft;
	public Material v2wallright;
	public Material v1floorceil;
	public Material v2floorceil;
	public Material v1endleft;
	public Material v1endright;
	public Material v2endleft;
	public Material v2endright;
	public GameObject leftwall;
	public GameObject rightwall;
	public GameObject ceiling;
	public GameObject floor;
	public GameObject endleft;
	public GameObject endright;
	
	// auditory stimuli
	public AudioClip s1; // assign these sounds in consol
	public AudioClip s2;
	public AudioClip s3;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
		leftwall = GameObject.Find("StimLeftWall");
		rightwall = GameObject.Find("StimRightWall");
		ceiling = GameObject.Find("StimCeiling");
		floor = GameObject.Find ("StimFloor");
		endleft = GameObject.Find ("RestartLeft");
		endright = GameObject.Find ("RestartRight");
		audio.loop=true;
		audio.clip=s3;
		audio.Play ();
	}
	
 	void OnTriggerEnter (Collider col) {
		audio.Stop ();
		runningtrialtime=0;
		
		nexttrial=rand.Next (0,10);
		
		if (nexttrial<8){
			leftwall.renderer.material=v1wallleft;
			rightwall.renderer.material=v1wallright;
			ceiling.renderer.material=v1floorceil;
			floor.renderer.material=v1floorceil;
			endleft.renderer.material=v1endleft;
			endright.renderer.material=v1endright;
			
			audio.clip = s1;
			audio.Play ();
		}
		else if (nexttrial>=8){
			leftwall.renderer.material=v2wallleft;
			rightwall.renderer.material=v2wallright;
			ceiling.renderer.material=v2floorceil;
			floor.renderer.material=v2floorceil;
			endleft.renderer.material=v2endleft;
			endright.renderer.material=v2endright;
			
			audio.clip = s2;
			audio.Play ();
		}
		
		//nexttone=rand.Next (0,2);
		//nextvis=rand.Next (0,2);
		//nextodor=rand.Next (0,2);
		
		//UnityEngine.Debug.Log(nextvis);
		//UnityEngine.Debug.Log(nexttone);
		//UnityEngine.Debug.Log(nextodor);
			
//		if (nextvis == 0) {
//			leftwall.renderer.material=v1wallleft;
//			rightwall.renderer.material=v1wallright;
//			ceiling.renderer.material=v1floorceil;
//			floor.renderer.material=v1floorceil;
//			endleft.renderer.material=v1endleft;
//			endright.renderer.material=v1endright;
//		}
//		else if (nextvis == 1){
//			leftwall.renderer.material=v2wallleft;
//			rightwall.renderer.material=v2wallright;
//			ceiling.renderer.material=v2floorceil;
//			floor.renderer.material=v2floorceil;
//			endleft.renderer.material=v2endleft;
//			endright.renderer.material=v2endright;
//		}
//
//		if (nexttone == 0) {		
//	    	audio.clip = s1;
//			audio.Play ();
//		}
//		else if (nexttone == 1){
//			audio.clip = s2;
//			audio.Play ();
//		}
		
//		if (nextodor == 0) {
//			odor="'odor1'";
//		}
//		else if (nextodor == 1){
//			odor="'odor2'";
//		}
		
	}
	
//	void onTriggerExit (Collider col) {
//   	isTriggered = false;
//	}
	
	// Update is called once per frame
	void Update () {
		
		if (!audio.isPlaying){
			audio.clip = s3;
			audio.Play ();
		}
		
		runningtrialtime=runningtrialtime+Time.deltaTime;
		
		if (nextpos == true && stim == "'None'") {
			//xpos = rand.Next (10,20);
			xpos = 30;
			nextpos = false;
		}
		
		//if (nextvis==0 && nexttone==0 && nextodor==0) {
		if (nexttrial>=8) {
			if (nextpos == false && stim == "'None'") {
				if (player.transform.position.x>=xpos) {
					stim = "'water'";
					xpos = 100;
					nextpos = false;
				}
			}
		}
//		else if (nextvis==1 && nexttone==1 && nextodor==1) {
//			if (nextpos == false && stim == "'None'") {
//				if (player.transform.position.x>=xpos) {
//					stim = "'shock'";
//					xpos = 100;
//					nextpos = false;
//				}
//			}
//		}
		

	}
}

