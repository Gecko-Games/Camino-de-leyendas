using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Funciones para las animaciones SetWeaponColActivation(int 0) , SetWeaponColActivation(int 1),
//WaitForNext(), SetWeaponColActivation(int 0), FinishAttack(),
//WeapondToRightHand(), WeapondToLeftHand()

[RequireComponent(typeof(KickAssCombatInputs))]
public class KickAssCombatSystem : MonoBehaviour {

	public Transform target;
	public float speedToRotationTarget = 90f;

	public List<KickAssAbilityMove> moveList;
	public bool canAdd = true, lastAttack = false, canMove = true, attackMode = false;

	public BaseWeapon weapon;
	private WeapomManager wm;

	private bool makeCritical = false;
	private float slowMotionTime = 0f;

	public int WeaponColActivation{
		get; set;
	}

	public int WeaponEffectActivation{
		get; set;
	}

	private Animator ani;
	private AbilityCaster ac;
	private float smootLerpLayer;
	public int indexMove = 0;

	// Use this for initialization
	void Start () {
		ani = this.GetComponent<Animator>();
		ac = this.GetComponent<AbilityCaster>();
		weapon = this.transform.GetComponentInChildren<BaseWeapon>();
		wm = (WeapomManager)FindObjectOfType(typeof(WeapomManager));
	}

	// Update is called once per frame
	void Update () {

		if(ac.auraActive){
			ani.speed = 1.5f;
		}else{
			ani.speed = 1;
		}

		if(makeCritical){
			SlowMotionManagger();
		}

		if(weapon){

			if(WeaponEffectActivation > 0){
				weapon.ActivateEffect = true;
			}else{
				weapon.ActivateEffect = false;
			}

			//if(WeaponColActivation > 0){
			//	weapon.ActivateCollider = true;
			//}else{
			//	weapon.ActivateCollider = false;
			//}
		}

		if(attackMode){
			weapon.ActivateEffect = true;
			if(!canMove){

				if(target){
					Vector3 targetDir = target.position - this.transform.position;
					float step = speedToRotationTarget * Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards(this.transform.forward, new Vector3(targetDir.x, 0f, targetDir.z), step, 0f);
					this.transform.rotation = Quaternion.LookRotation(newDir);
				}

				ani.SetLayerWeight(1, 0f);

				if(moveList.Count > 0){
					Attack();
				}


			}else{

				ani.SetLayerWeight(1, 1f);
				ani.SetBool("CanMove", true);
				ClearAttackList();
			}
		}else{
			if(weapon){
				weapon.ActivateEffect = false;
			}
		}

	}

	public void addAttacksToList(KickAssAbilityMove attackToAdd){
		if(canAdd){
			ani.SetBool("CanMove", false);
			moveList.Add(attackToAdd);
			canAdd = false;
		}
	}

	public void ClearAttackList(){
		moveList.Clear();
		indexMove = 0;
		lastAttack = false;

		wm.ResetWeapondHand();
		canAdd = true;
	}

	public void Attack(){
		ani.SetInteger("IndexMove", indexMove);
		ani.SetTrigger(moveList[0].name);
		ac.AbilityToCastName = moveList[0].abilityName;
		weapon.multiplierMod = moveList[0].multiplierDamage;
		moveList.Clear();
	}

	public void WaitForNext(){
		indexMove++;
		canAdd = true;
		lastAttack = false;
	}

	public void FinishAttack(){
		indexMove = 0;
		lastAttack = true;
		ani.SetBool("CanMove", false);
		canMove = true;
        weapon.WeaponTrail(false);
        wm.ResetWeapondHand();
		canAdd = true;
	}



	public void Evasion(){
		ani.SetTrigger("Evasion");
	}

	public void SetSlowMotion(float duration){
		slowMotionTime = duration;
		makeCritical = true;
	}

	void SlowMotionManagger(){
		if(slowMotionTime > 0){
			TimeManagger.AlterateTime(.25f);
			slowMotionTime -= Time.deltaTime;
		}else{
			TimeManagger.NormalizeTime();
			makeCritical = false;
		}
	}

	public void SetWeaponColActivation(int active){

        if (weapon)
        {
            switch (active)
            {
                case 0:
                    weapon.ActivateCollider = false;
                    weapon.WeaponTrail(false);
                    break;
                case 1:
                    weapon.ActivateCollider = true;
                    weapon.WeaponTrail(true);
                    break;
            }
        }
    }

}
