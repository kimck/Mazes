using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

public class Context_Fear_Train : MonoBehaviour {
	
	public GameObject player;
	public static string shock = "'None'";
	public static string led = "'None'";
	
	// flags for triggering led, shock, and location movements
	//bool fear_flag = false;
	bool led_flag = false;
	bool shock_flag = false;
	
	public int num_shocks = 5;
	public float next_shock;
	System.Random rand = new System.Random();
	public float start = 150;
	public int shock_counter = 1;
	public float end = 60;
		
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
		
		// gives the first shock 
		if (shock_counter ==1){
			//  turns 1st shock on for 2 seconds after 2.5 minutes of baseline
			if (Time.time >= start && Time.time < start+2) {
				if (shock_flag == false && led_flag == false) {
					// turns shock on
					shock = "'zap_on'";
					shock_flag = true;
					
					// turns led off for one frame
					led = "'led_off'";
					led_flag = true;
				}
				else if (shock_flag == true && led_flag == true) {
					// turns led back on
					led = "'led_on'";
					led_flag = false;
				}
			}
			if (Time.time >= start+2){
				shock_counter = shock_counter+1;
				start=start+2;
				
				//  turns shock off and sets random intertrial interval between 60-120 s
				next_shock=rand.Next (60,121);
			}
		}
		
		
		// gives subsequent shocks if specified
		if (shock_counter > 1 && shock_counter < num_shocks+1){
			
			if (Time.time >= start && Time.time < start+next_shock) {
				if (shock_flag == true && led_flag == false) {
					// turns shock off
					shock = "'zap_off'";
					shock_flag = false;
					
					// turns led off for one frame
					led = "'led_off'";
					led_flag = true;
				}
				else if (shock_flag == false && led_flag == true) {
					// turns led back on
					led = "'led_on'";
					led_flag = false;
				}
			}
				
			if (Time.time >= start+next_shock && Time.time < start+next_shock+2) {
				if (shock_flag == false && led_flag == false) {
					// turns shock on
					shock = "'zap_on'";
					shock_flag = true;
					
					// turns led off for one frame
					led = "'led_off'";
					led_flag = true;
				}
				else if (shock_flag == true && led_flag == true) {
					// turns led back on
					led = "'led_on'";
					led_flag = false;
				}
			}
			
			if (Time.time >= start+next_shock+2){
				start=start+next_shock+2;
				shock_counter=shock_counter+1;
				next_shock=rand.Next (60,121);
			}
		}
		
		// ends the simulation after shocks
		else if (shock_counter >= num_shocks+1){
			
			// turns last shock off		
			if (Time.time >= start && Time.time < start+end) {
				if (shock_flag == true && led_flag == false) {
					// turns shock off
					shock = "'zap_off'";
					shock_flag = false;
					
					// turns led off for one frame
					led = "'led_off'";
					led_flag = true;
				}
				else if (shock_flag == false && led_flag == true) {
					// turns led back on
					led = "'led_on'";
					led_flag = false;
				}
			}

			// moves player to black box 60 seconds after shock
			if (Time.time >= start+end && led_flag == false) {
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

}

