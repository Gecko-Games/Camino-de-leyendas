using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("LobinuxSoft/Virtual Reality/Multiple TextUI manipulator")]
public class MultipleTextManipulator : MonoBehaviour {

	public string mText;

	public Text[] mTextObj;

	// Update is called once per frame
	void Update () {

		foreach(Text tempText in mTextObj){

			tempText.text = mText;

		}
	}
}
