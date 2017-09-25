using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[AddComponentMenu("LobinuxSoft/Virtual Reality/VR Script")]
public class VRScript : MonoBehaviour {

	public bool usingGyro = false;

	[Header("Input Mode (Touch or Normal (normal is for GamePad or Keyboard) input)")]
	public InputType inputMode = InputType.TouchInput;
	
	[Header("Camera mode")]
	public VRModes mode = VRModes.None;
	
	[Header("Prefab camera to VR efecct")]
	public Camera camVR;
	
	[Header("Prefab (UI VR Controls)")]
	public Canvas uiControls;
	
	[Header("VR distances of eyes")]
	[Range(0f, .2f)]public float offsetVR = .05f;

	[Header("Distance of UI to camera")]
	[Range(.5f, 3f)]public float canvasOffsetView = .5f;

	[Header("MasterCanvas")]
	public AutoParent masterCanvas;

	[HideInInspector]
	public Camera Left, Right, MainC;

	private Transform mainTransform;
	
	public string saveFileName = "vrsave";
	
	private GyroRotCam gyroCam;

	RectTransform tempRect;
	
	public enum VRModes{
		None,
		SideToSide
	}
	
	public void VRModeChange(int changeMode){
		mode = (VRModes)changeMode;
	}

	public int WhatModeIs(){
		int modeIs = 0;
		switch (mode){
		case VRModes.None:
			modeIs = 0;
			break;
		case VRModes.SideToSide:
			modeIs = 1;
			break;
		}

		return modeIs;
	}

	public void OffsetVR(float change){
		offsetVR = change;
	}

	public void Save(){
		
		VRConfigToSave dataToSave = new VRConfigToSave();
		
		dataToSave.inputModeToSave = inputMode;
		dataToSave.vrmodeToSave = mode;
		dataToSave.offsetvrToSave = offsetVR;
		dataToSave.canvasOffsetView = canvasOffsetView;
		
		SaveAndLoadSystem.Save<VRConfigToSave>(dataToSave, saveFileName);
	}
	
	public void Load(){
		
		if(SaveAndLoadSystem.FileExist(saveFileName)){
			VRConfigToSave dataToSave = SaveAndLoadSystem.Load<VRConfigToSave>(saveFileName);
			
			inputMode =  dataToSave.inputModeToSave;
			mode = dataToSave.vrmodeToSave;
			offsetVR = dataToSave.offsetvrToSave;
			canvasOffsetView = dataToSave.canvasOffsetView;
		}
	}
	
	// Use this for initialization
	void Start () {

		mainTransform = Camera.main.transform;
		MainC = Camera.main;
		uiControls = Instantiate(uiControls,mainTransform.position, mainTransform.rotation) as Canvas;
		if(masterCanvas){
			tempRect = masterCanvas.GetComponent<RectTransform>();
			tempRect.sizeDelta = new Vector2(Screen.width, Screen.height);
		}

		if(Left == null){
			
			Left = Instantiate(camVR, mainTransform.position, mainTransform.rotation) as Camera;
			Left.name = "Left eye";
			Left.transform.parent = mainTransform;
			
			Left.depth = 0f;
			
		}
		
		if(Right == null){
			Right = Instantiate(camVR, mainTransform.position, mainTransform.rotation) as Camera;
			Right.name = "Right eye";
			Right.transform.parent = mainTransform;
			
			Right.depth = 0f;
			
		}

		if(usingGyro == true){
			gyroCam = (GyroRotCam)FindObjectOfType(typeof(GyroRotCam));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		switch(mode){
		case VRModes.None:
			
			uiControls.GetComponent<UIControlsVR>().DeactivateControls();
			MainC.enabled = true;
			
			Left.gameObject.SetActive(false);
			Right.gameObject.SetActive(false);

			if(usingGyro == true){
				if(gyroCam.enabled == true){
					gyroCam.Recalibate();
					gyroCam.enabled = false;
				}
			}
			
			if(masterCanvas){

				canvasOffsetView = 1f;

				if(masterCanvas.GetComponent<Canvas>().worldCamera != MainC.GetComponent<Camera>()){
					masterCanvas.GetComponent<Canvas>().worldCamera = MainC.GetComponent<Camera>();
				}

				tempRect.sizeDelta = new Vector2(Screen.width, Screen.height);

				Vector3 tempScale = new Vector3(canvasOffsetView / Screen.height, canvasOffsetView / Screen.height, canvasOffsetView / Screen.height);

				masterCanvas.GetComponent<Canvas>().transform.localScale = tempScale;

				masterCanvas.offset = new Vector3(masterCanvas.offset.x, masterCanvas.offset.y, canvasOffsetView);



			}
			
			break;
			
		case VRModes.SideToSide:
			
			Left.rect = new Rect(0f, 0f, .5f, 1f);
			Right.rect = new Rect(.5f, 0f, .5f, 1f);
			
			uiControls.GetComponent<UIControlsVR>().ActivateControls();
			MainC.enabled = false;
			
			Left.gameObject.SetActive(true);
			Right.gameObject.SetActive(true);

			if(usingGyro == true){
				gyroCam.enabled = true;
			}
			
			Left.transform.position = new Vector3(mainTransform.position.x, mainTransform.position.y,mainTransform.position.z);
			Right.transform.position = new Vector3(mainTransform.position.x, mainTransform.position.y,mainTransform.position.z);
			
			Left.transform.localPosition = new Vector3(- offsetVR, 0f, 0f);
			Right.transform.localPosition = new Vector3(offsetVR, 0f, 0f);
			
			Left.transform.rotation = mainTransform.rotation;
			Right.transform.rotation = mainTransform.rotation;
			
			if(masterCanvas){

				canvasOffsetView = 1f;

				if(masterCanvas.GetComponent<Canvas>().worldCamera != Left){
					masterCanvas.GetComponent<Canvas>().worldCamera = Left;
				}

				tempRect.sizeDelta = new Vector2(Screen.width * .75f, Screen.width * .75f);

				Vector3 tempScale = new Vector3(canvasOffsetView / Screen.height, canvasOffsetView / Screen.height, canvasOffsetView / Screen.height);

				masterCanvas.GetComponent<Canvas>().transform.localScale = tempScale;

				masterCanvas.offset = new Vector3(masterCanvas.offset.x, masterCanvas.offset.y, canvasOffsetView);
					
			}
			
			break;
		}
	}
	
	[Serializable]
	class VRConfigToSave{
		public InputType inputModeToSave;
		public VRModes vrmodeToSave;
		public float offsetvrToSave;
		public float canvasOffsetView;
	}
}