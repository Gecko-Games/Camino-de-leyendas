using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("LobinuxSoft/Virtual Reality/UI Controls for VR")]
public class UIControlsVR : MonoBehaviour {

	public VRScript vrCam;
	public GameObject vrModeOnButton;
	public CanvasGroup controls;
	private int vrMode = 0;

	// Use this for initialization
	void Start () {
		vrCam = (VRScript)FindObjectOfType(typeof(VRScript));
		LoadConfig();
	}

	public void OffsetVR(float change){
		vrCam.offsetVR = change;
	}

	public void CanvasOffsetViewVR(float change){
		vrCam.canvasOffsetView = change;
	}

	public void SaveConfig(){
		vrCam.Save();
	}

	public void LoadConfig(){
		if(SaveAndLoadSystem.FileExist(vrCam.saveFileName)){
			vrCam.Load();
			vrMode = vrCam.WhatModeIs();
		}
	}

	public void ActiveVR(){
		if(vrMode == 0){
			vrCam.VRModeChange(1);
			vrMode = 1;
		}else{
			vrCam.VRModeChange(0);
			vrMode = 0;
		}
	}

	public void StopGame(){
		TimeManagger.PauseTime();
	}

	public void ResumeGame(){
		TimeManagger.NormalizeTime();
	}

	public void ActivateControls(){
		controls.alpha = 1f;
		controls.blocksRaycasts = true;
		controls.interactable = true;
		vrModeOnButton.SetActive(false);
	}

	public void DeactivateControls(){
		controls.alpha = 0f;
		controls.blocksRaycasts = false;
		controls.interactable = false;
		vrModeOnButton.SetActive(true);
	}
}
