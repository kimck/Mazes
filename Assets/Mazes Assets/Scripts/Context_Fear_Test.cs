using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Context_Fear_Test : MonoBehaviour {
	
	public GameObject player;
	public static string shock = "'None'";
	public static string led = "'None'";
	
	// flags for triggering led, shock, and location movements
	//bool fear_flag = false;
	bool led_flag = false;
	
	// Use this for initialization
	void Start () {
		led = "'led_on'";
	}
	
	
//	IEnumerator Test_Context (float delaytime) {
//		
//		player = GameObject.Find ("LiveMouseFPSController"); 
//		
//		Vector3 black_position = new Vector3(0.5f,1.1f,0);
//		Vector3 black_rotation = new Vector3(0,90,0);
//		player.transform.position = black_position; //places player in black box
//		player.transform.eulerAngles = black_rotation;
//		
//		yield return new WaitForSeconds (delaytime);
//				
//	}
	
	
	// Update is called once per frame
	void Update () {
		print (Time.time);
		
		// resets led and shock to None at the start of each frame
		led = "'None'";
		shock = "'None'";
		
		// moves player from open field context to the fear context
//		if (fear_flag == false) {
//			if (Time.time >= 150 && led_flag == false) { // 150
//				// places player in the fear context
//				player = GameObject.Find ("LiveMouseFPSController");
//				Vector3 fear_position = new Vector3(0.5f,1.1f,90);
//				Vector3 fear_rotation = new Vector3(0,90,0);
//				player.transform.position = fear_position;
//				player.transform.eulerAngles = fear_rotation;
//				
//				// turns LED off for one frame
//				led = "'led_off'";
//				led_flag = true;
//			}
//			
//			else if (Time.time >= 150 && led_flag == true) { // 150
//				// turns LED back on
//				led = "'led_on'";	
//				led_flag = false;
//				
//				fear_flag = true;
//			}
//		}
		
		
		// moves player to black box after 2.5 minutes in fear context
		if (Time.time >= 150 && led_flag == false) { // 300
			player = GameObject.Find ("LiveMouseFPSController"); 
		
			Vector3 black_position = new Vector3(0.5f,1.1f,0);
			Vector3 black_rotation = new Vector3(0,90,0);
			player.transform.position = black_position; //places player in black box
			player.transform.eulerAngles = black_rotation;
			
			// turns led off
			led = "'led_off'";
			led_flag = true;
		}
		

	}
}

