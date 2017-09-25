using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class LightningCreator : MonoBehaviour {

	public Lightning lightningPref;

	public LightningType lType = LightningType.Linear;

	public int manyLightnings = 3;

	public float timeToInstance = 1f;

	public bool randomDistance, procedural;

	public float distance = 2f, speedRaise = 1.5f;

	private float tempTimer;

	private Lightning tempLightning;

	// Use this for initialization
	void Update () {

		lightningPref.procedural = procedural;

		if(lType == LightningType.Linear){

			for(int i = 0; i < manyLightnings; i++){
				if(tempTimer > 0){
					tempTimer -= Time.deltaTime;
				}else{
					LinearLightning();
					tempTimer = timeToInstance;
				}

			}

		}else if(lType == LightningType.Spherical){

			for(int i = 0; i < manyLightnings; i++){
				if(tempTimer > 0){
					tempTimer -= Time.deltaTime;
				}else{
					SphericalLightning();
					tempTimer = timeToInstance;
				}
			}
		}


	}

	void LinearLightning(){

		if (!randomDistance) {
			lightningPref.maxZ = distance;
			lightningPref.speedLightningRaise = speedRaise;
			tempLightning = Instantiate (lightningPref, this.transform.position, this.transform.rotation) as Lightning;
			tempLightning.transform.SetParent(this.transform);
		} else {
			lightningPref.maxZ = Random.Range(distance/2f, distance);
			lightningPref.speedLightningRaise = speedRaise;
			tempLightning = Instantiate (lightningPref, this.transform.position, this.transform.rotation) as Lightning;
			tempLightning.transform.SetParent(this.transform);
		}
	}

	void SphericalLightning(){

		if (!randomDistance) {
			lightningPref.maxZ = distance;
			lightningPref.speedLightningRaise = speedRaise;
			tempLightning = Instantiate (lightningPref, this.transform.position, new Quaternion (Random.Range (-360f, 360f),
		                                                                   Random.Range (-360f, 360f),
		                                                                   Random.Range (-360f, 360f),
				Random.Range (-360f, 360f))) as Lightning;
		} else {
			lightningPref.maxZ = Random.Range(distance/2f, distance);
			lightningPref.speedLightningRaise = speedRaise;
			tempLightning = Instantiate (lightningPref, this.transform.position, new Quaternion (Random.Range (-360f, 360f),
				Random.Range (-360f, 360f),
				Random.Range (-360f, 360f),
				Random.Range (-360f, 360f))) as Lightning;
		}

	}

	public enum LightningType{
		Linear,
		Spherical
	}
}
