using UnityEngine;
using System.Collections;

public class ShockWave : MonoBehaviour {

	public float lDistance = 10f, sVelocity = 10f, dismis = 1.5f;

	private float fres = 1f, vScale = 0f, vTime = 0f;

	private Renderer shockWaveRend;
	// Use this for initialization
	void Start () {
		shockWaveRend = this.gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (vScale < lDistance) {
			vScale += sVelocity * Time.deltaTime;
			this.transform.localScale = new Vector3 (Mathf.Clamp (vScale, 0f, lDistance), 
			                                         Mathf.Clamp (vScale, 0f, lDistance), 
			                                         Mathf.Clamp (vScale, 0f, lDistance));
		} else if(vScale >= lDistance){

			vTime += Time.deltaTime * dismis;
			fres = Mathf.Lerp(1f, 0f, vTime);
			shockWaveRend.material.SetFloat("_Intensity", fres);
		}

	}
}
