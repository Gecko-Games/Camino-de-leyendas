using UnityEngine;
using System.Collections;

[AddComponentMenu("LobinuxSoft/Virtual Reality/Multiple GameObject manipulator")]
public class MultipleGameObjectManipulator : MonoBehaviour {


	public GameObject[] mGameObject;


	public void Activation(bool mActive){

		foreach(GameObject tempGO in mGameObject){
			tempGO.gameObject.SetActive(mActive);
		}
	}
}
