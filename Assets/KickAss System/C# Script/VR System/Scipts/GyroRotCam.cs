using UnityEngine;
using System.Collections;

[AddComponentMenu("LobinuxSoft/Virtual Reality/Gyroscope Rotate Camera")]
public class GyroRotCam : MonoBehaviour {
	
	[Header("The sensivity of Gyreoscope")]
	public float gyroSensitivity = 1.5f;
	
	private float x, y, z, rotX, rotY, rotZ;
	
	private Vector3 newRot = Vector3.zero;
	
	private Transform pivotRoot;

	private GamePadInputs gpi;
	
	void Awake () {

		gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));

		// Activate the gyroscope
		Input.gyro.enabled = true;
		pivotRoot = new GameObject("PivotCam").GetComponent<Transform>();
		pivotRoot.position = Camera.main.transform.position;
		pivotRoot.rotation = Camera.main.transform.rotation;
		
		if(this.transform.childCount != 0){
			pivotRoot.position = Camera.main.transform.position;
			pivotRoot.rotation = Camera.main.transform.rotation;
			pivotRoot.parent = Camera.main.transform.parent;
			Camera.main.transform.parent = pivotRoot;
			Camera.main.transform.position = pivotRoot.position;
			Camera.main.transform.rotation = pivotRoot.rotation;
		}else{
			Camera.main.transform.parent = pivotRoot;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(gpi.r3.isActive){
			Recalibate();
		}
		
		if (SystemInfo.supportsGyroscope) {
			if (Input.gyro.enabled){
				
				//get values from the gyroscope
				
				if(Input.gyro.rotationRateUnbiased.x < -.03f || Input.gyro.rotationRateUnbiased.x > .03f){
					x = Input.gyro.rotationRateUnbiased.x;
				}else{
					x = 0f;
				}
				
				if(Input.gyro.rotationRateUnbiased.y < -.03f || Input.gyro.rotationRateUnbiased.y > .03f){
					y = Input.gyro.rotationRateUnbiased.y;
				}else{
					y = 0f;
				}
				
				if(Input.gyro.rotationRateUnbiased.z < -.03f || Input.gyro.rotationRateUnbiased.z > .03f){
					z = Input.gyro.rotationRateUnbiased.z;
				}else{
					z = 0f;
				}
				
				RotCamRotate(new Vector3(x,y,z));
				
			}
		}
	}

	public void OnDisable(){
		Camera.main.transform.rotation = pivotRoot.rotation;
	}

	public void FirstReset(){
		pivotRoot.transform.rotation = pivotRoot.transform.parent.rotation;
		Camera.main.transform.rotation = pivotRoot.transform.rotation;
	}

	public void Recalibate(){
		Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, pivotRoot.rotation, Time.deltaTime * 2f);
	}
	
	public void RotCamRotate(Vector3 rot){
		
		rotY = rot.y;
		
		rotX = rot.x;
		
		rotZ = rot.z;
		
		newRot = new Vector3(-rotX, -rotY, rotZ);
		
		newRot = newRot * gyroSensitivity;
		
		this.transform.Rotate(newRot, Space.Self);
		
	}
}
