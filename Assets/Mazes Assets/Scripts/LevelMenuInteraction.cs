using UnityEngine;
using System.Collections;

public class LevelMenuInteraction : MonoBehaviour
{
    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.LoadLevel("MainMenu");
        }
    }
}