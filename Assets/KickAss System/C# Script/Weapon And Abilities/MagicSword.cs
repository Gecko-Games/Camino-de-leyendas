using UnityEngine;
using System.Collections;

public class MagicSword : Sword {

	public ParticleSystem emi;
	private Animator ani;
	ParticleSystem.EmissionModule tempEmi;

	void Start(){
		SetUpCollider();
		WeaponTrail (false);
		ani = this.GetComponent<Animator>();
		tempEmi = emi.emission;
	}

	public void EquipWeapond(){
		ani.SetTrigger("Active");
	}

	protected override void WeaponEffect(){

		if(ActivateEffect){
			tempEmi.enabled = true;
		}else{
			tempEmi.enabled = false;
		}
	}
}
