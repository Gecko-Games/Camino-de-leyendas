using UnityEngine;
using System.Collections;

public class ItemPreviewContainer : MonoBehaviour {

	public Vector3 rotateDegreesPerSecond;
	private float m_LastRealTime;
	
	private void Update()
	{
		float deltaTime = Time.deltaTime;

		transform.Rotate(rotateDegreesPerSecond * deltaTime, Space.World);
	}
}
