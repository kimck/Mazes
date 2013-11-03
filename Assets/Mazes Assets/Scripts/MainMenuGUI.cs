using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {
    void OnGUI() {
        GL.Clear(true, true, Color.black);
        // Make a background box
        GUI.Box(new Rect(10, 10, Screen.width-10, Screen.height-10), "MouseVR");
        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 160, 40), "Elevated Plus Maze")) {
            Application.LoadLevel(1);
        }
        // Make the second button.
        if (GUI.Button(new Rect(200, 40, 160, 40), "Open Field Test")) {
            Application.LoadLevel(2);
        }
        if (GUI.Button(new Rect(Screen.width-180, Screen.height-60, 160, 40), "Exit")) {
            Application.Quit();
        }
    }
    void Start() {
        Screen.showCursor = true;
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }
}