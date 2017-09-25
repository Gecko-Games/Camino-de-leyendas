using UnityEngine;
using System.Collections;

public class AutoParent : MonoBehaviour {

	public Transform target;
	public Vector3 offset = new Vector3(0f, 7.5f, 0f);
	public Vector3 rotationOffset = new Vector3(0f, 30f, 0f);


	private void LateUpdate()
	{
		if(target){
			transform.SetParent(target);
			transform.localPosition = offset;
			transform.rotation = target.rotation;
			transform.localRotation = Quaternion.Euler(rotationOffset);
		}
	}
}
