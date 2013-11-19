using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;
using System.Xml;

// for TCP socket
using System.Net;
using System.Net.Sockets;
using System.Text;

// This class acts as a tracker, writing a python dict object as a 
// string over a TCP socket. This will be logged by the client 
// that receives the string. 

// This class tracks a player avatar that is tagged as "Player".
public class SaveLocation_LTrack_Morphing : MonoBehaviour {

    /// The GameObject with the level data, used for some basic meta data.    
    private GameObject trackedPlayer;

    // Get a client stream for reading and writing on port 8001.
    static Int32 port = 8001;
    static string server = "127.0.0.1";
    static TcpClient client = new TcpClient(server, port);
    static NetworkStream stream = client.GetStream();

    // Writes basic meta data at the beginning of the game.
    void Start () {

		// get tracked player
		trackedPlayer = GameObject.FindGameObjectWithTag ("Player");		
	
		// get the current level
		string level= Application.loadedLevelName;
	
	    // Send a message with the starting state of the game at the 
		// begginning of the level to the connected TcpServer. 
			
			
		DateTime time = DateTime.Now;              // Use current time
		string format = "MMM ddd d HH:mm yyyy";    // Use this format
		string datetime =  "'" + time.ToString(format) + "'";
		
		string message = "{'level':" + "'" + level + "'" + 
		    ", 'level_start_time':" + datetime +
		    ", 'running_time':" + Time.timeSinceLevelLoad +
		    ", 'x':" + trackedPlayer.transform.position.x + 
		    ", 'z':" + trackedPlayer.transform.position.z + 
		    ", 'view_angle':" + trackedPlayer.transform.eulerAngles.y + 
		    ", 'x_mouse':" + Input.GetAxis("Mouse X") +
		    ", 'y_mouse':" + Input.GetAxis("Mouse Y") +
			", 'context':" + LTrack_RewardTrigger_Morphing.context +
			", 'outcome':" + LTrack_RewardTrigger_Morphing.outcome +
			"}";
		
		Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
	    stream.Write(data, 0, data.Length);
    }
    	
    void Update () {
		CollectData (" 'default' ");
    }

//    // Logs the current position of the player.
//    public void CollectData () {
//	CollectData ("auto-position");
//    }

    // Logs the current position of the player at a specific event 
    public void CollectData (string eventName) {
		
		string message = "{ 'running_time':" + Time.timeSinceLevelLoad +
		    ", 'x':" + trackedPlayer.transform.position.x + 
		    ", 'z':" + trackedPlayer.transform.position.z + 
		    ", 'view_angle':" + trackedPlayer.transform.eulerAngles.y + 
		    ", 'x_mouse':" + Input.GetAxis("Mouse X") +
		    ", 'y_mouse':" + Input.GetAxis("Mouse Y") +
			", 'context':" + LTrack_RewardTrigger_Morphing.context +
			", 'outcome':" + LTrack_RewardTrigger_Morphing.outcome +
		    "}";

		// TODO: add a an object that persists through the game that 
		// we can add additional meta data to (also as a dict string). 

		Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
	    stream.Write(data, 0, data.Length);
		
		if (LTrack_RewardTrigger_Morphing.context!="'None'"){
				LTrack_RewardTrigger_Morphing.context="'None'";
		}
		
		if (LTrack_RewardTrigger_Morphing.outcome!="'None'"){
				LTrack_RewardTrigger_Morphing.outcome="'None'";
		}
    }

    public void OnApplicationQuit () {
        stream.Close();
        client.Close();
		CollectData ("application_quit");
    }
}
