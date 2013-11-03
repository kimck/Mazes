using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]

public class LiveMouseController : MonoBehaviour {

	public float linearSensitivity = 1F;
	public float angularSensitivity = 0.25F;
	public static float portNumber = 8501;
	//public float xmouse = 0;
	//public float ymouse = 0;
	//public float currentxmouse = 0;
	//public float currentymouse = 0;
	
	private CharacterMotor motor;
	
	void Awake() {
		motor = gameObject.GetComponent<CharacterMotor>();
	}

	void Update() {
		
		Vector3 directionVector = new Vector3(0, 0, -Input.GetAxis ("Mouse X") * linearSensitivity);
		motor.inputMoveDirection = transform.rotation * directionVector;
		transform.Rotate(0, Input.GetAxis("Mouse Y") * angularSensitivity, 0);
		
		
		//if (Time.frameCount < 2) {
		//	currentxmouse = 0;
		//	currentymouse = 0;
		//}
		//else {
		//	currentxmouse = -Input.GetAxis ("Mouse X");
		//	currentymouse = -Input.GetAxis ("Mouse Y");
		//}
		
		//xmouse = xmouse + currentxmouse;
		//ymouse = ymouse + currentymouse;
		
		//print (xmouse);
		//print (ymouse);

	}
	
	void Start() {
		Screen.showCursor = false;
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}