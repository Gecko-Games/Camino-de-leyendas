using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GamePadInputs))]
public class SimpleControl : MonoBehaviour {

	public float rotSpeed = 50f, moveSpeed = 50f;

	private GamePadInputs gpi;

	// Use this for initialization
	void Start () {
		gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(-gpi.verticalRJoystick.aValue * Time.deltaTime * rotSpeed, gpi.horizontalRJoystick.aValue * Time.deltaTime * rotSpeed, 0f));
		this.transform.Translate(new Vector3(gpi.horizontalLJoystick.aValue * moveSpeed * Time.deltaTime, 0f, gpi.verticalLJoystick.aValue * moveSpeed * Time.deltaTime)); 
	}
}
