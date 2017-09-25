using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("LobinuxSoft/Virtual Reality/Multiple canvas manipulator")]
public class MultipleCanvasManipulator : MonoBehaviour {

	public bool mActive = false;
	public Canvas[] mCanvas;

	// Update is called once per frame
	void Update () {
	
		foreach(Canvas tempCan in mCanvas){
			tempCan.gameObject.SetActive(mActive);
		}

	}
}
