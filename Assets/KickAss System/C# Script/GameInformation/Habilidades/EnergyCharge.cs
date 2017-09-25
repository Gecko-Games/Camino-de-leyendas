using UnityEngine;
using System.Collections;

public class EnergyCharge : MonoBehaviour {

	public int energyToCharge = 10;

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			other.GetComponent<VitalsManager>().AddEnergy(energyToCharge);
			Destroy(this.gameObject);
		}
	}
}
