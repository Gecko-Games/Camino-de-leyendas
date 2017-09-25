using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class InputGamePadKey{
	public KeyCode keyCode;
	public bool isActive , isDown, isUp;

	public InputGamePadKey(KeyCode kc){
		keyCode = kc;
	}
}

[System.Serializable]
public class InputGamePadAxis{
	public string axisName;
	public float aValue;

	public bool isActive, isPositive, isNegative;

	public InputGamePadAxis(string aName){
		axisName = aName;
	}
}

public enum InputMode{
	PC,
	GAMEPAD,
	TOUCH,
	CUSTOM
}

[AddComponentMenu("LobinuxSoft/Virtual Reality/Gamepad Inputs")]
public class GamePadInputs : MonoBehaviour {

	public bool test = false, touchControlVisible = true;

	public InputMode im = InputMode.PC;
	public CanvasGroup touchControls;
	public VRScript vrcam;

	private CanvasGroup controls;

	[Header("(Inputs Gamepad Buttons)")]
	 
	public InputGamePadKey a = new InputGamePadKey(KeyCode.JoystickButton0);
	public InputGamePadKey b = new InputGamePadKey(KeyCode.JoystickButton1);
	public InputGamePadKey x = new InputGamePadKey(KeyCode.JoystickButton2);
	public InputGamePadKey y = new InputGamePadKey(KeyCode.JoystickButton3);
	public InputGamePadKey l1 = new InputGamePadKey(KeyCode.JoystickButton4);
	public InputGamePadKey r1 = new InputGamePadKey(KeyCode.JoystickButton5);
	public InputGamePadKey l2 = new InputGamePadKey(KeyCode.JoystickButton6);
	public InputGamePadKey r2 = new InputGamePadKey(KeyCode.JoystickButton7);
	public InputGamePadKey start = new InputGamePadKey(KeyCode.JoystickButton10);
	public InputGamePadKey select = new InputGamePadKey(KeyCode.JoystickButton11);
	public InputGamePadKey l3 = new InputGamePadKey(KeyCode.JoystickButton13);
	public InputGamePadKey r3 = new InputGamePadKey(KeyCode.JoystickButton14);

	[Header("(Axis Inputs for digitalpad and analog joystick)")]
	[Header("(Set this inputs first in the input project)")]

	public InputGamePadAxis horizontalLJoystick = new InputGamePadAxis("Horizontal");
	public InputGamePadAxis verticalLJoystick = new InputGamePadAxis("Vertical");
	public InputGamePadAxis horizontalRJoystick = new InputGamePadAxis("Mouse X");
	public InputGamePadAxis verticalRJoystick = new InputGamePadAxis("Mouse Y");
	public InputGamePadAxis horizontalDPadJoystick = new InputGamePadAxis("Horizontal D");
	public InputGamePadAxis verticalDPadJoystick = new InputGamePadAxis("Vertical D");


	void Start(){
		controls = (CanvasGroup)Instantiate(touchControls, Vector3.zero, Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {

		#if MOBILE_INPUT
		if(vrcam){	
			switch(vrcam.mode){
			case VRScript.VRModes.None:

				if(Input.GetJoystickNames().Length > 0){
					im = InputMode.GAMEPAD;
				}else{
					im = InputMode.TOUCH;
				}

				break;
			case VRScript.VRModes.SideToSide:
				
				im = InputMode.GAMEPAD;

				break;
			}
		}

		#endif

		switch(im){
		case InputMode.GAMEPAD:

			controls.interactable = false;
			controls.blocksRaycasts = false;
			controls.alpha = 0f;

			a = new InputGamePadKey(KeyCode.JoystickButton0);
			b = new InputGamePadKey(KeyCode.JoystickButton1);
			x = new InputGamePadKey(KeyCode.JoystickButton2);
			y = new InputGamePadKey(KeyCode.JoystickButton3);
			l1 = new InputGamePadKey(KeyCode.JoystickButton4);
			r1 = new InputGamePadKey(KeyCode.JoystickButton5);
			l2 = new InputGamePadKey(KeyCode.JoystickButton6);
			r2 = new InputGamePadKey(KeyCode.JoystickButton7);
			start = new InputGamePadKey(KeyCode.JoystickButton10);
			select = new InputGamePadKey(KeyCode.JoystickButton11);
			l3 = new InputGamePadKey(KeyCode.JoystickButton13);
			r3 = new InputGamePadKey(KeyCode.JoystickButton14);

			break;

		case InputMode.PC:

			controls.interactable = false;
			controls.blocksRaycasts = false;
			controls.alpha = 0f;

			a = new InputGamePadKey(KeyCode.Space);
			b = new InputGamePadKey(KeyCode.B);
			x = new InputGamePadKey(KeyCode.Mouse0);
			y = new InputGamePadKey(KeyCode.Mouse1);
			l1 = new InputGamePadKey(KeyCode.LeftControl);
			r1 = new InputGamePadKey(KeyCode.LeftShift);
			l2 = new InputGamePadKey(KeyCode.JoystickButton6);
			r2 = new InputGamePadKey(KeyCode.T);
			start = new InputGamePadKey(KeyCode.Escape);
			select = new InputGamePadKey(KeyCode.Tab);
			l3 = new InputGamePadKey(KeyCode.E);
			r3 = new InputGamePadKey(KeyCode.Q);

			break;

		case InputMode.TOUCH:

			if(touchControlVisible){
				controls.interactable = true;
				controls.blocksRaycasts = true;
				controls.alpha = 1f;
			}else{
				controls.interactable = false;
				controls.blocksRaycasts = false;
				controls.alpha = 0f;
			}

			break;

		case InputMode.CUSTOM:

			controls.interactable = false;
			controls.blocksRaycasts = false;
			controls.alpha = 0f;

			break;

		}

		ControlUpdate();

	}

	void OnGUI(){

		if(test){
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode))){
				if(Input.GetKey(kcode))
					GUILayout.Box("Key Code: " + kcode);
			}

			GUILayout.Box("A Button: " + a.isActive);
			GUILayout.Box("A ButtonDown: " + a.isDown);
			GUILayout.Box("A ButtonUp: " + a.isUp);

			GUILayout.Box("B Button: " + b.isActive);
			GUILayout.Box("B ButtonDown: " + b.isDown);
			GUILayout.Box("B ButtonUp: " + b.isUp);

			GUILayout.Box("X Button: " + x.isActive);
			GUILayout.Box("X ButtonDown: " + x.isDown);
			GUILayout.Box("X ButtonUp: " + x.isUp);

			GUILayout.Box("Y Button: " + y.isActive);
			GUILayout.Box("Y ButtonDown: " + y.isDown);
			GUILayout.Box("Y ButtonUp: " + y.isUp);
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
			GUILayout.Box("L1 Button: " + l1.isActive);
			GUILayout.Box("L1 ButtonDown: " + l1.isDown);
			GUILayout.Box("L1 ButtonUp: " + l1.isUp);

			GUILayout.Box("R1 Button: " + r1.isActive);
			GUILayout.Box("R1 ButtonDown: " + r1.isDown);
			GUILayout.Box("R1 ButtonUp: " + r1.isUp);

			GUILayout.Box("L2 Button: " + l2.isActive);
			GUILayout.Box("L2 ButtonDown: " + l2.isDown);
			GUILayout.Box("L2 ButtonUp: " + l2.isUp);

			GUILayout.Box("R2 Button: " + r2.isActive);
			GUILayout.Box("R2 ButtonDown: " + r2.isDown);
			GUILayout.Box("R2 ButtonUp: " + r2.isUp);
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
			GUILayout.Box("Start Button: " + start.isActive);
			GUILayout.Box("Start ButtonDown: " + start.isDown);
			GUILayout.Box("Start ButtonUp: " + start.isUp);

			GUILayout.Box("Select Button: " + select.isActive);
			GUILayout.Box("Select ButtonDown: " + select.isDown);
			GUILayout.Box("Select ButtonUp: " + select.isUp);

			GUILayout.Box("L3 Button: " + l3.isActive);
			GUILayout.Box("L3 ButtonDown: " + l3.isDown);
			GUILayout.Box("L3 ButtonUp: " + l3.isUp);

			GUILayout.Box("R3 Button: " + r3.isActive);
			GUILayout.Box("R3 ButtonDown: " + r3.isDown);
			GUILayout.Box("R3 ButtonUp: " + r3.isUp);
			GUILayout.EndVertical();

			GUILayout.BeginVertical();

			GUILayout.Box("Horizontal Axis: " + horizontalLJoystick.aValue);

			GUILayout.Box("Vertical Axis: " + verticalLJoystick.aValue);

			GUILayout.Box("Mouse X Axis: " + horizontalRJoystick.aValue);

			GUILayout.Box("Mouse Y Axis: " + verticalRJoystick.aValue);

			GUILayout.Box("Horizontal D Axis: " + horizontalDPadJoystick.aValue);

			GUILayout.Box("Vertical D Axis: " + verticalDPadJoystick.aValue);

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();
		}

	}

	public void ControlUpdate(){
		switch (im){

		case InputMode.GAMEPAD:
			a.isActive = Input.GetKey(a.keyCode);
			a.isDown = Input.GetKeyDown(a.keyCode);
			a.isUp = Input.GetKeyUp(a.keyCode);

			b.isActive = Input.GetKey(b.keyCode);
			b.isDown = Input.GetKeyDown(b.keyCode);
			b.isUp = Input.GetKeyUp(b.keyCode);

			x.isActive = Input.GetKey(x.keyCode);
			x.isDown = Input.GetKeyDown(x.keyCode);
			x.isUp = Input.GetKeyUp(x.keyCode);

			y.isActive = Input.GetKey(y.keyCode);
			y.isDown = Input.GetKeyDown(y.keyCode);
			y.isUp = Input.GetKeyUp(y.keyCode);

			l1.isActive = Input.GetKey(l1.keyCode);
			l1.isDown = Input.GetKeyDown(l1.keyCode);
			l1.isUp = Input.GetKeyUp(l1.keyCode);

			r1.isActive = Input.GetKey(r1.keyCode);
			r1.isDown = Input.GetKeyDown(r1.keyCode);
			r1.isUp = Input.GetKeyUp(r1.keyCode);

			l2.isActive = Input.GetKey(l2.keyCode);
			l2.isDown = Input.GetKeyDown(l2.keyCode);
			l2.isUp = Input.GetKeyUp(l2.keyCode);

			r2.isActive = Input.GetKey(r2.keyCode);
			r2.isDown = Input.GetKeyDown(r2.keyCode);
			r2.isUp = Input.GetKeyUp(r2.keyCode);

			start.isActive = Input.GetKey(start.keyCode);
			start.isDown = Input.GetKeyDown(start.keyCode);
			start.isUp = Input.GetKeyUp(start.keyCode);

			select.isActive = Input.GetKey(select.keyCode);
			select.isDown = Input.GetKeyDown(select.keyCode);
			select.isUp = Input.GetKeyUp(select.keyCode);

			l3.isActive = Input.GetKey(l3.keyCode);
			l3.isDown = Input.GetKeyDown(l3.keyCode);
			l3.isUp = Input.GetKeyUp(l3.keyCode);

			r3.isActive = Input.GetKey(r3.keyCode);
			r3.isDown = Input.GetKeyDown(r3.keyCode);
			r3.isUp = Input.GetKeyUp(r3.keyCode);

			horizontalLJoystick.aValue = Input.GetAxis(horizontalLJoystick.axisName);

			if(horizontalLJoystick.isActive == false){
				if(horizontalLJoystick.aValue > 0f){
					horizontalLJoystick.isPositive = true;
					horizontalLJoystick.isActive = true;
				}

				if(horizontalLJoystick.aValue < 0f){
					horizontalLJoystick.isNegative = true;
					horizontalLJoystick.isActive = true;
				}

			}else{
				horizontalLJoystick.isPositive = false;
				horizontalLJoystick.isNegative = false;
				if(horizontalLJoystick.aValue == 0f){
					horizontalLJoystick.isActive = false;
				}
			}

			verticalLJoystick.aValue = Input.GetAxis(verticalLJoystick.axisName);

			if(verticalLJoystick.isActive == false){
				if(verticalLJoystick.aValue > 0f){
					verticalLJoystick.isPositive = true;
					verticalLJoystick.isActive = true;
				}

				if(verticalLJoystick.aValue < 0f){
					verticalLJoystick.isNegative = true;
					verticalLJoystick.isActive = true;
				}

			}else{
				verticalLJoystick.isPositive = false;
				verticalLJoystick.isNegative = false;
				if(verticalLJoystick.aValue == 0f){
					verticalLJoystick.isActive = false;
				}
			}

			horizontalRJoystick.aValue = Input.GetAxis(horizontalRJoystick.axisName);

			if(horizontalRJoystick.isActive == false){
				if(horizontalRJoystick.aValue > 0f){
					horizontalRJoystick.isPositive = true;
					horizontalRJoystick.isActive = true;
				}

				if(horizontalRJoystick.aValue < 0f){
					horizontalRJoystick.isNegative = true;
					horizontalRJoystick.isActive = true;
				}

			}else{
				horizontalRJoystick.isPositive = false;
				horizontalRJoystick.isNegative = false;
				if(horizontalRJoystick.aValue == 0f){
					horizontalRJoystick.isActive = false;
				}
			}

			verticalRJoystick.aValue = Input.GetAxis(verticalRJoystick.axisName);

			if(verticalRJoystick.isActive == false){
				if(verticalRJoystick.aValue > 0f){
					verticalRJoystick.isPositive = true;
					verticalRJoystick.isActive = true;
				}

				if(verticalRJoystick.aValue < 0f){
					verticalRJoystick.isNegative = true;
					verticalRJoystick.isActive = true;
				}

			}else{
				verticalRJoystick.isPositive = false;
				verticalRJoystick.isNegative = false;
				if(verticalRJoystick.aValue == 0f){
					verticalRJoystick.isActive = false;
				}
			}

			horizontalDPadJoystick.aValue = Input.GetAxis(horizontalDPadJoystick.axisName);

			if(horizontalDPadJoystick.isActive == false){
				if(horizontalDPadJoystick.aValue > 0f){
					horizontalDPadJoystick.isPositive = true;
					horizontalDPadJoystick.isActive = true;
				}

				if(horizontalDPadJoystick.aValue < 0f){
					horizontalDPadJoystick.isNegative = true;
					horizontalDPadJoystick.isActive = true;
				}

			}else{
				horizontalDPadJoystick.isPositive = false;
				horizontalDPadJoystick.isNegative = false;
				if(horizontalDPadJoystick.aValue == 0f){
					horizontalDPadJoystick.isActive = false;
				}
			}

			verticalDPadJoystick.aValue = Input.GetAxis(verticalDPadJoystick.axisName);

			if(verticalDPadJoystick.isActive == false){
				if(verticalDPadJoystick.aValue > 0f){
					verticalDPadJoystick.isPositive = true;
					verticalDPadJoystick.isActive = true;
				}

				if(verticalDPadJoystick.aValue < 0f){
					verticalDPadJoystick.isNegative = true;
					verticalDPadJoystick.isActive = true;
				}

			}else{
				verticalDPadJoystick.isPositive = false;
				verticalDPadJoystick.isNegative = false;
				if(verticalDPadJoystick.aValue == 0f){
					verticalDPadJoystick.isActive = false;
				}
			}
			break;
		case InputMode.PC:
			a.isActive = Input.GetKey(a.keyCode);
			a.isDown = Input.GetKeyDown(a.keyCode);
			a.isUp = Input.GetKeyUp(a.keyCode);

			b.isActive = Input.GetKey(b.keyCode);
			b.isDown = Input.GetKeyDown(b.keyCode);
			b.isUp = Input.GetKeyUp(b.keyCode);

			x.isActive = Input.GetKey(x.keyCode);
			x.isDown = Input.GetKeyDown(x.keyCode);
			x.isUp = Input.GetKeyUp(x.keyCode);

			y.isActive = Input.GetKey(y.keyCode);
			y.isDown = Input.GetKeyDown(y.keyCode);
			y.isUp = Input.GetKeyUp(y.keyCode);

			l1.isActive = Input.GetKey(l1.keyCode);
			l1.isDown = Input.GetKeyDown(l1.keyCode);
			l1.isUp = Input.GetKeyUp(l1.keyCode);

			r1.isActive = Input.GetKey(r1.keyCode);
			r1.isDown = Input.GetKeyDown(r1.keyCode);
			r1.isUp = Input.GetKeyUp(r1.keyCode);

			l2.isActive = Input.GetKey(l2.keyCode);
			l2.isDown = Input.GetKeyDown(l2.keyCode);
			l2.isUp = Input.GetKeyUp(l2.keyCode);

			r2.isActive = Input.GetKey(r2.keyCode);
			r2.isDown = Input.GetKeyDown(r2.keyCode);
			r2.isUp = Input.GetKeyUp(r2.keyCode);

			start.isActive = Input.GetKey(start.keyCode);
			start.isDown = Input.GetKeyDown(start.keyCode);
			start.isUp = Input.GetKeyUp(start.keyCode);

			select.isActive = Input.GetKey(select.keyCode);
			select.isDown = Input.GetKeyDown(select.keyCode);
			select.isUp = Input.GetKeyUp(select.keyCode);

			l3.isActive = Input.GetKey(l3.keyCode);
			l3.isDown = Input.GetKeyDown(l3.keyCode);
			l3.isUp = Input.GetKeyUp(l3.keyCode);

			r3.isActive = Input.GetKey(r3.keyCode);
			r3.isDown = Input.GetKeyDown(r3.keyCode);
			r3.isUp = Input.GetKeyUp(r3.keyCode);

			horizontalLJoystick.aValue = Input.GetAxis(horizontalLJoystick.axisName);

			if(horizontalLJoystick.isActive == false){
				if(horizontalLJoystick.aValue > 0f){
					horizontalLJoystick.isPositive = true;
					horizontalLJoystick.isActive = true;
				}

				if(horizontalLJoystick.aValue < 0f){
					horizontalLJoystick.isNegative = true;
					horizontalLJoystick.isActive = true;
				}

			}else{
				horizontalLJoystick.isPositive = false;
				horizontalLJoystick.isNegative = false;
				if(horizontalLJoystick.aValue == 0f){
					horizontalLJoystick.isActive = false;
				}
			}

			verticalLJoystick.aValue = Input.GetAxis(verticalLJoystick.axisName);

			if(verticalLJoystick.isActive == false){
				if(verticalLJoystick.aValue > 0f){
					verticalLJoystick.isPositive = true;
					verticalLJoystick.isActive = true;
				}

				if(verticalLJoystick.aValue < 0f){
					verticalLJoystick.isNegative = true;
					verticalLJoystick.isActive = true;
				}

			}else{
				verticalLJoystick.isPositive = false;
				verticalLJoystick.isNegative = false;
				if(verticalLJoystick.aValue == 0f){
					verticalLJoystick.isActive = false;
				}
			}

			horizontalRJoystick.aValue = Input.GetAxis(horizontalRJoystick.axisName);

			if(horizontalRJoystick.isActive == false){
				if(horizontalRJoystick.aValue > 0f){
					horizontalRJoystick.isPositive = true;
					horizontalRJoystick.isActive = true;
				}

				if(horizontalRJoystick.aValue < 0f){
					horizontalRJoystick.isNegative = true;
					horizontalRJoystick.isActive = true;
				}

			}else{
				horizontalRJoystick.isPositive = false;
				horizontalRJoystick.isNegative = false;
				if(horizontalRJoystick.aValue == 0f){
					horizontalRJoystick.isActive = false;
				}
			}

			verticalRJoystick.aValue = Input.GetAxis(verticalRJoystick.axisName);

			if(verticalRJoystick.isActive == false){
				if(verticalRJoystick.aValue > 0f){
					verticalRJoystick.isPositive = true;
					verticalRJoystick.isActive = true;
				}

				if(verticalRJoystick.aValue < 0f){
					verticalRJoystick.isNegative = true;
					verticalRJoystick.isActive = true;
				}

			}else{
				verticalRJoystick.isPositive = false;
				verticalRJoystick.isNegative = false;
				if(verticalRJoystick.aValue == 0f){
					verticalRJoystick.isActive = false;
				}
			}

			horizontalDPadJoystick.aValue = Input.GetAxis(horizontalDPadJoystick.axisName);

			if(horizontalDPadJoystick.isActive == false){
				if(horizontalDPadJoystick.aValue > 0f){
					horizontalDPadJoystick.isPositive = true;
					horizontalDPadJoystick.isActive = true;
				}

				if(horizontalDPadJoystick.aValue < 0f){
					horizontalDPadJoystick.isNegative = true;
					horizontalDPadJoystick.isActive = true;
				}

			}else{
				horizontalDPadJoystick.isPositive = false;
				horizontalDPadJoystick.isNegative = false;
				if(horizontalDPadJoystick.aValue == 0f){
					horizontalDPadJoystick.isActive = false;
				}
			}

			verticalDPadJoystick.aValue = Input.GetAxis(verticalDPadJoystick.axisName);

			if(verticalDPadJoystick.isActive == false){
				if(verticalDPadJoystick.aValue > 0f){
					verticalDPadJoystick.isPositive = true;
					verticalDPadJoystick.isActive = true;
				}

				if(verticalDPadJoystick.aValue < 0f){
					verticalDPadJoystick.isNegative = true;
					verticalDPadJoystick.isActive = true;
				}

			}else{
				verticalDPadJoystick.isPositive = false;
				verticalDPadJoystick.isNegative = false;
				if(verticalDPadJoystick.aValue == 0f){
					verticalDPadJoystick.isActive = false;
				}
			}
			break;
		case InputMode.CUSTOM:
			a.isActive = Input.GetKey(a.keyCode);
			a.isDown = Input.GetKeyDown(a.keyCode);
			a.isUp = Input.GetKeyUp(a.keyCode);

			b.isActive = Input.GetKey(b.keyCode);
			b.isDown = Input.GetKeyDown(b.keyCode);
			b.isUp = Input.GetKeyUp(b.keyCode);

			x.isActive = Input.GetKey(x.keyCode);
			x.isDown = Input.GetKeyDown(x.keyCode);
			x.isUp = Input.GetKeyUp(x.keyCode);

			y.isActive = Input.GetKey(y.keyCode);
			y.isDown = Input.GetKeyDown(y.keyCode);
			y.isUp = Input.GetKeyUp(y.keyCode);

			l1.isActive = Input.GetKey(l1.keyCode);
			l1.isDown = Input.GetKeyDown(l1.keyCode);
			l1.isUp = Input.GetKeyUp(l1.keyCode);

			r1.isActive = Input.GetKey(r1.keyCode);
			r1.isDown = Input.GetKeyDown(r1.keyCode);
			r1.isUp = Input.GetKeyUp(r1.keyCode);

			l2.isActive = Input.GetKey(l2.keyCode);
			l2.isDown = Input.GetKeyDown(l2.keyCode);
			l2.isUp = Input.GetKeyUp(l2.keyCode);

			r2.isActive = Input.GetKey(r2.keyCode);
			r2.isDown = Input.GetKeyDown(r2.keyCode);
			r2.isUp = Input.GetKeyUp(r2.keyCode);

			start.isActive = Input.GetKey(start.keyCode);
			start.isDown = Input.GetKeyDown(start.keyCode);
			start.isUp = Input.GetKeyUp(start.keyCode);

			select.isActive = Input.GetKey(select.keyCode);
			select.isDown = Input.GetKeyDown(select.keyCode);
			select.isUp = Input.GetKeyUp(select.keyCode);

			l3.isActive = Input.GetKey(l3.keyCode);
			l3.isDown = Input.GetKeyDown(l3.keyCode);
			l3.isUp = Input.GetKeyUp(l3.keyCode);

			r3.isActive = Input.GetKey(r3.keyCode);
			r3.isDown = Input.GetKeyDown(r3.keyCode);
			r3.isUp = Input.GetKeyUp(r3.keyCode);

			horizontalLJoystick.aValue = Input.GetAxis(horizontalLJoystick.axisName);

			if(horizontalLJoystick.isActive == false){
				if(horizontalLJoystick.aValue > 0f){
					horizontalLJoystick.isPositive = true;
					horizontalLJoystick.isActive = true;
				}

				if(horizontalLJoystick.aValue < 0f){
					horizontalLJoystick.isNegative = true;
					horizontalLJoystick.isActive = true;
				}

			}else{
				horizontalLJoystick.isPositive = false;
				horizontalLJoystick.isNegative = false;
				if(horizontalLJoystick.aValue == 0f){
					horizontalLJoystick.isActive = false;
				}
			}

			verticalLJoystick.aValue = Input.GetAxis(verticalLJoystick.axisName);

			if(verticalLJoystick.isActive == false){
				if(verticalLJoystick.aValue > 0f){
					verticalLJoystick.isPositive = true;
					verticalLJoystick.isActive = true;
				}

				if(verticalLJoystick.aValue < 0f){
					verticalLJoystick.isNegative = true;
					verticalLJoystick.isActive = true;
				}

			}else{
				verticalLJoystick.isPositive = false;
				verticalLJoystick.isNegative = false;
				if(verticalLJoystick.aValue == 0f){
					verticalLJoystick.isActive = false;
				}
			}

			horizontalRJoystick.aValue = Input.GetAxis(horizontalRJoystick.axisName);

			if(horizontalRJoystick.isActive == false){
				if(horizontalRJoystick.aValue > 0f){
					horizontalRJoystick.isPositive = true;
					horizontalRJoystick.isActive = true;
				}

				if(horizontalRJoystick.aValue < 0f){
					horizontalRJoystick.isNegative = true;
					horizontalRJoystick.isActive = true;
				}

			}else{
				horizontalRJoystick.isPositive = false;
				horizontalRJoystick.isNegative = false;
				if(horizontalRJoystick.aValue == 0f){
					horizontalRJoystick.isActive = false;
				}
			}

			verticalRJoystick.aValue = Input.GetAxis(verticalRJoystick.axisName);

			if(verticalRJoystick.isActive == false){
				if(verticalRJoystick.aValue > 0f){
					verticalRJoystick.isPositive = true;
					verticalRJoystick.isActive = true;
				}

				if(verticalRJoystick.aValue < 0f){
					verticalRJoystick.isNegative = true;
					verticalRJoystick.isActive = true;
				}

			}else{
				verticalRJoystick.isPositive = false;
				verticalRJoystick.isNegative = false;
				if(verticalRJoystick.aValue == 0f){
					verticalRJoystick.isActive = false;
				}
			}

			horizontalDPadJoystick.aValue = Input.GetAxis(horizontalDPadJoystick.axisName);

			if(horizontalDPadJoystick.isActive == false){
				if(horizontalDPadJoystick.aValue > 0f){
					horizontalDPadJoystick.isPositive = true;
					horizontalDPadJoystick.isActive = true;
				}

				if(horizontalDPadJoystick.aValue < 0f){
					horizontalDPadJoystick.isNegative = true;
					horizontalDPadJoystick.isActive = true;
				}

			}else{
				horizontalDPadJoystick.isPositive = false;
				horizontalDPadJoystick.isNegative = false;
				if(horizontalDPadJoystick.aValue == 0f){
					horizontalDPadJoystick.isActive = false;
				}
			}

			verticalDPadJoystick.aValue = Input.GetAxis(verticalDPadJoystick.axisName);

			if(verticalDPadJoystick.isActive == false){
				if(verticalDPadJoystick.aValue > 0f){
					verticalDPadJoystick.isPositive = true;
					verticalDPadJoystick.isActive = true;
				}

				if(verticalDPadJoystick.aValue < 0f){
					verticalDPadJoystick.isNegative = true;
					verticalDPadJoystick.isActive = true;
				}

			}else{
				verticalDPadJoystick.isPositive = false;
				verticalDPadJoystick.isNegative = false;
				if(verticalDPadJoystick.aValue == 0f){
					verticalDPadJoystick.isActive = false;
				}
			}
			break;
		case InputMode.TOUCH:

			a.isActive = CrossPlatformInputManager.GetButton("A");
			a.isDown = CrossPlatformInputManager.GetButtonDown("A");
			a.isUp = CrossPlatformInputManager.GetButtonUp("A");

			b.isActive = CrossPlatformInputManager.GetButton("B");
			b.isDown = CrossPlatformInputManager.GetButtonDown("B");
			b.isUp = CrossPlatformInputManager.GetButtonUp("B");

			x.isActive = CrossPlatformInputManager.GetButton("X");
			x.isDown = CrossPlatformInputManager.GetButtonDown("X");
			x.isUp = CrossPlatformInputManager.GetButtonUp("X");

			y.isActive = CrossPlatformInputManager.GetButton("Y");
			y.isDown = CrossPlatformInputManager.GetButtonDown("Y");
			y.isUp = CrossPlatformInputManager.GetButtonUp("Y");

			l1.isActive = CrossPlatformInputManager.GetButton("L1");
			l1.isDown = CrossPlatformInputManager.GetButtonDown("L1");
			l1.isUp = CrossPlatformInputManager.GetButtonUp("L1");

			r1.isActive = CrossPlatformInputManager.GetButton("R1");
			r1.isDown = CrossPlatformInputManager.GetButtonDown("R1");
			r1.isUp = CrossPlatformInputManager.GetButtonUp("R1");

			l2.isActive = CrossPlatformInputManager.GetButton("L2");
			l2.isDown = CrossPlatformInputManager.GetButtonDown("L2");
			l2.isUp = CrossPlatformInputManager.GetButtonUp("L2");

			r2.isActive = CrossPlatformInputManager.GetButton("R2");
			r2.isDown = CrossPlatformInputManager.GetButtonDown("R2");
			r2.isUp = CrossPlatformInputManager.GetButtonUp("R2");

			start.isActive = CrossPlatformInputManager.GetButton("START");
			start.isDown = CrossPlatformInputManager.GetButtonDown("START");
			start.isUp = CrossPlatformInputManager.GetButtonUp("START");

			select.isActive = CrossPlatformInputManager.GetButton("SELECT");
			select.isDown = CrossPlatformInputManager.GetButtonDown("SELECT");
			select.isUp = CrossPlatformInputManager.GetButtonUp("SELECT");

			l3.isActive = CrossPlatformInputManager.GetButton("L3");
			l3.isDown = CrossPlatformInputManager.GetButtonDown("L3");
			l3.isUp = CrossPlatformInputManager.GetButtonUp("L3");

			r3.isActive = CrossPlatformInputManager.GetButton("R3");
			r3.isDown = CrossPlatformInputManager.GetButtonDown("R3");
			r3.isUp = CrossPlatformInputManager.GetButtonUp("R3");

			horizontalLJoystick.aValue = CrossPlatformInputManager.GetAxisRaw(horizontalLJoystick.axisName);

			if(horizontalLJoystick.isActive == false){
				if(horizontalLJoystick.aValue > 0f){
					horizontalLJoystick.isPositive = true;
					horizontalLJoystick.isActive = true;
				}

				if(horizontalLJoystick.aValue < 0f){
					horizontalLJoystick.isNegative = true;
					horizontalLJoystick.isActive = true;
				}

			}else{
				horizontalLJoystick.isPositive = false;
				horizontalLJoystick.isNegative = false;
				if(horizontalLJoystick.aValue == 0f){
					horizontalLJoystick.isActive = false;
				}
			}

			verticalLJoystick.aValue = CrossPlatformInputManager.GetAxisRaw(verticalLJoystick.axisName);

			if(verticalLJoystick.isActive == false){
				if(verticalLJoystick.aValue > 0f){
					verticalLJoystick.isPositive = true;
					verticalLJoystick.isActive = true;
				}

				if(verticalLJoystick.aValue < 0f){
					verticalLJoystick.isNegative = true;
					verticalLJoystick.isActive = true;
				}

			}else{
				verticalLJoystick.isPositive = false;
				verticalLJoystick.isNegative = false;
				if(verticalLJoystick.aValue == 0f){
					verticalLJoystick.isActive = false;
				}
			}

			horizontalRJoystick.aValue = CrossPlatformInputManager.GetAxis(horizontalRJoystick.axisName);

			if(horizontalRJoystick.isActive == false){
				if(horizontalRJoystick.aValue > 0f){
					horizontalRJoystick.isPositive = true;
					horizontalRJoystick.isActive = true;
				}

				if(horizontalRJoystick.aValue < 0f){
					horizontalRJoystick.isNegative = true;
					horizontalRJoystick.isActive = true;
				}

			}else{
				horizontalRJoystick.isPositive = false;
				horizontalRJoystick.isNegative = false;
				if(horizontalRJoystick.aValue == 0f){
					horizontalRJoystick.isActive = false;
				}
			}

			verticalRJoystick.aValue = CrossPlatformInputManager.GetAxis(verticalRJoystick.axisName);

			if(verticalRJoystick.isActive == false){
				if(verticalRJoystick.aValue > 0f){
					verticalRJoystick.isPositive = true;
					verticalRJoystick.isActive = true;
				}

				if(verticalRJoystick.aValue < 0f){
					verticalRJoystick.isNegative = true;
					verticalRJoystick.isActive = true;
				}

			}else{
				verticalRJoystick.isPositive = false;
				verticalRJoystick.isNegative = false;
				if(verticalRJoystick.aValue == 0f){
					verticalRJoystick.isActive = false;
				}
			}

			horizontalDPadJoystick.aValue = 0f;
			horizontalDPadJoystick.isActive = false;
			horizontalDPadJoystick.isPositive = CrossPlatformInputManager.GetButtonUp("DPAD H POSITIVE");
			horizontalDPadJoystick.isNegative = CrossPlatformInputManager.GetButtonUp("DPAD H NEGATIVE");

			verticalDPadJoystick.aValue = 0f;
			verticalDPadJoystick.isActive = false;
			verticalDPadJoystick.isPositive = CrossPlatformInputManager.GetButtonUp("DPAD V POSITIVE");
			verticalDPadJoystick.isNegative = CrossPlatformInputManager.GetButtonUp("DPAD V NEGATIVE");


			break;
		}
	}

	public void Test(bool valor){
		test = valor;
	}
}



