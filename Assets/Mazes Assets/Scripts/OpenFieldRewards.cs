using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class OpenFieldRewards : MonoBehaviour {
	
	public static string choice = "'None'";
	public bool nextpos = true;

	public GameObject player;
	
	public float xpos;
	public float zpos;
	System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("LiveMouseFPSController");
	}

	
	// Update is called once per frame
	void Update () {
		
		if (nextpos == true && choice == "'None'") {
			xpos = rand.Next (6,44);
			zpos = rand.Next (71,109);
			nextpos = false;
		}
		
		if ( nextpos == false ) {
			if (player.transform.position.x>=xpos-5 && player.transform.position.x<=xpos+5) {
				if (player.transform.position.z>=zpos-5 && player.transform.position.z<=zpos+5) {
					choice = "'correct'";
					nextpos = true;
				}
			}
		}

		
	}
}

