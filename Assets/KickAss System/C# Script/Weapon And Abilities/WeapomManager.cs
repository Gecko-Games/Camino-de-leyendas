using UnityEngine;
using System.Collections;
using LobinuxSoft.KickAssSystem;

[System.Serializable]
class WeaponManagerInfo{
	public string rwPath, lwPath, uwPath, dwPath;
}

public class WeapomManager : MonoBehaviour {

	public Transform leftHand, rightHand, upWeaponHolder, downWeaponHolder, leftWeaponHolder, rightWeaponHolder;
	public string rightWeaponPath, leftWeaponPath, upWeaponPath, downWeaponPath;
	private Transform resetHand;
	[Space(1f)]
	public BaseWeapon rightWeapon, leftWeapon, upWeapon, downWeapon;
	private Animator ani;
	public WeaponsToEquip wte = WeaponsToEquip.Right;
	private KickAssCombatSystem kacs;
	private KickAssCombatInputs kaci;
	private VitalsManager vm;
	public WeapondType wt;
    GamePadInputs gpi;

	// Use this for initialization
	void Start () {

		ani = this.GetComponent<Animator>();
		kacs = this.GetComponent<KickAssCombatSystem>();
		kaci = this.GetComponent<KickAssCombatInputs>();
		vm = this.GetComponent<VitalsManager>();
        gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));

        //Load();

        WeaponInit();

		if(kacs.weapon){
			wt = kacs.weapon.wt;
		}



		WeaponBasePlayerUpdate();
	}
	
	// Update is called once per frame
	void Update () {

		if(gpi.b.isUp){
			ResetWeapondHand();
		}

		if(!vm.dead){

			WeaponBasePlayerUpdate();
			WeapondManipulation();

		}

		WeaponPathsUpdate();
	}


	//Solo sirve en las animaciones
	public void WeapondToRightHand(){
		switch(wte){
		case WeaponsToEquip.Up:
			
			upWeapon.transform.position = rightHand.position;
			upWeapon.transform.rotation = rightHand.rotation;
			upWeapon.transform.SetParent(rightHand);

			break;
		case WeaponsToEquip.Down:

			downWeapon.transform.position = rightHand.position;
			downWeapon.transform.rotation = rightHand.rotation;
			downWeapon.transform.SetParent(rightHand);

			break;
		case WeaponsToEquip.Right:

			rightWeapon.transform.position = rightHand.position;
			rightWeapon.transform.rotation = rightHand.rotation;
			rightWeapon.transform.SetParent(rightHand);

			break;
		case WeaponsToEquip.Left:

			leftWeapon.transform.position = rightHand.position;
			leftWeapon.transform.rotation = rightHand.rotation;
			leftWeapon.transform.SetParent(rightHand);

			break;
		}
	}

	//Solo sirve en las animaciones
	public void WeapondToLeftHand(){
		switch(wte){
		case WeaponsToEquip.Up:

			upWeapon.transform.position = leftHand.position;
			upWeapon.transform.rotation = leftHand.rotation;
			upWeapon.transform.SetParent(leftHand);

			break;
		case WeaponsToEquip.Down:

			downWeapon.transform.position = leftHand.position;
			downWeapon.transform.rotation = leftHand.rotation;
			downWeapon.transform.SetParent(leftHand);

			break;
		case WeaponsToEquip.Right:

			rightWeapon.transform.position = leftHand.position;
			rightWeapon.transform.rotation = leftHand.rotation;
			rightWeapon.transform.SetParent(leftHand);

			break;
		case WeaponsToEquip.Left:

			leftWeapon.transform.position = leftHand.position;
			leftWeapon.transform.rotation = leftHand.rotation;
			leftWeapon.transform.SetParent(leftHand);

			break;
		}
	}


	public void WeapondEquip(){
		switch(wte){
		case WeaponsToEquip.Up:

			SwordEquip(rightHand);
			resetHand = rightHand;

			break;
		case WeaponsToEquip.Down:

			SwordEquip(rightHand);
			resetHand = rightHand;

			break;
		case WeaponsToEquip.Right:

			SwordEquip(rightHand);
			resetHand = rightHand;

			break;
		case WeaponsToEquip.Left:

			SwordEquip(rightHand);
			resetHand = rightHand;

			break;
		}
	}

	public void WeapondUnequip(){
		switch(wte){
		case WeaponsToEquip.Up:

			SwordUnequip();

			break;
		case WeaponsToEquip.Down:

			SwordUnequip();

			break;
		case WeaponsToEquip.Right:

			SwordUnequip();

			break;
		case WeaponsToEquip.Left:

			SwordUnequip();

			break;
		}
	}

	void WeapondManipulation(){
		if(kaci.canEnterInputs){
			if(kacs.canMove){

				if(gpi.verticalDPadJoystick.isPositive){	//Boton ARRIBA
					if(upWeapon){
						switch(wte){
						case WeaponsToEquip.Up:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Down:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Right:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Left:
							
							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						}

						if(!kacs.attackMode){
							kacs.weapon = upWeapon;
							wt = kacs.weapon.wt;
							kaci.weapondAttack = upWeapon.moveList;
							wte = WeaponsToEquip.Up;
							SwordEquipOrder();
						}
					}

				}else if(gpi.verticalDPadJoystick.isNegative)
                {		//Boton ABAJO

					if(downWeapon){
						switch(wte){
						case WeaponsToEquip.Up:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Down:
							
							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Right:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Left:
							
							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						}

						if(!kacs.attackMode){
							kacs.weapon = downWeapon;
							wt = kacs.weapon.wt;
							kaci.weapondAttack = downWeapon.moveList;
							wte = WeaponsToEquip.Down;
							SwordEquipOrder();
						}
					}

				}else if(gpi.horizontalDPadJoystick.isPositive)
                {	//Boton DERECHO

					if(rightWeapon){
						switch(wte){
						case WeaponsToEquip.Up:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Down:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Right:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Left:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						}

						if(!kacs.attackMode){
							kacs.weapon = rightWeapon;
							wt = kacs.weapon.wt;
							kaci.weapondAttack = rightWeapon.moveList;
							wte = WeaponsToEquip.Right;
							SwordEquipOrder();
						}
					}


				}else if(gpi.horizontalDPadJoystick.isNegative)
                {	//Boton IZQUIERDO

					if(leftWeapon){
						switch(wte){
						case WeaponsToEquip.Up:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Down:
							
							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Right:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						case WeaponsToEquip.Left:

							if(kacs.attackMode){
								SwordEquipOrder();
							}

							break;
						}

						if(!kacs.attackMode){
							kacs.weapon = leftWeapon;
							wt = kacs.weapon.wt;
							kaci.weapondAttack = leftWeapon.moveList;
							wte = WeaponsToEquip.Left;
							SwordEquipOrder();
						}
					}
				}
			}
		}
	}

	void SwordEquipOrder(){

		kaci.weapondAttack = kacs.weapon.moveList;

		if(kacs.weapon == upWeapon){
			ani.SetTrigger("BackHolder");
		}else if(kacs.weapon == downWeapon){
			ani.SetTrigger("LowerBackHolder");
		}else if(kacs.weapon == leftWeapon){
			ani.SetTrigger("BackHolder");
		}else if(kacs.weapon == rightWeapon){
			ani.SetTrigger("LowerBackHolder");
		}
		//ani.SetTrigger(kacs.weapon.equipOrder);

	}

	void SwordEquip(Transform hand){

		switch(wt){
		case WeapondType.Normal:
			kacs.weapon.transform.position = hand.position;
			kacs.weapon.transform.rotation = hand.rotation;
			kacs.weapon.transform.SetParent(hand);
			break;
		case WeapondType.Magic:
			kacs.weapon.transform.position = hand.position;
			kacs.weapon.transform.rotation = hand.rotation;
			kacs.weapon.transform.SetParent(hand);
			kacs.weapon.GetComponent<MagicSword>().EquipWeapond();
			break;
		}

		kacs.attackMode = true;

	}

	void SwordUnequip(){

		switch(wt){
		case WeapondType.Normal:
			
			if(kacs.weapon == upWeapon){
				kacs.weapon.transform.position = upWeaponHolder.position;
				kacs.weapon.transform.rotation = upWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(upWeaponHolder);
				resetHand = upWeaponHolder;
			}else if(kacs.weapon == downWeapon){
				kacs.weapon.transform.position = downWeaponHolder.position;
				kacs.weapon.transform.rotation = downWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(downWeaponHolder);
				resetHand = downWeaponHolder;
			}else if(kacs.weapon == leftWeapon){
				kacs.weapon.transform.position = leftWeaponHolder.position;
				kacs.weapon.transform.rotation = leftWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(leftWeaponHolder);
				resetHand = leftWeaponHolder;
			}else if(kacs.weapon == rightWeapon){
				kacs.weapon.transform.position = rightWeaponHolder.position;
				kacs.weapon.transform.rotation = rightWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(rightWeaponHolder);
				resetHand = rightWeaponHolder;
			}

			break;
		case WeapondType.Magic:

			if(kacs.weapon == upWeapon){
				kacs.weapon.GetComponent<MagicSword>().EquipWeapond();
				kacs.weapon.transform.position = upWeaponHolder.position;
				kacs.weapon.transform.rotation = upWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(upWeaponHolder);
				resetHand = upWeaponHolder;
			}else if(kacs.weapon == downWeapon){
				kacs.weapon.GetComponent<MagicSword>().EquipWeapond();
				kacs.weapon.transform.position = downWeaponHolder.position;
				kacs.weapon.transform.rotation = downWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(downWeaponHolder);
				resetHand = downWeaponHolder;
			}else if(kacs.weapon == leftWeapon){
				kacs.weapon.GetComponent<MagicSword>().EquipWeapond();
				kacs.weapon.transform.position = leftWeaponHolder.position;
				kacs.weapon.transform.rotation = leftWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(leftWeaponHolder);
				resetHand = leftWeaponHolder;
			}else if(kacs.weapon == rightWeapon){
				kacs.weapon.GetComponent<MagicSword>().EquipWeapond();
				kacs.weapon.transform.position = rightWeaponHolder.position;
				kacs.weapon.transform.rotation = rightWeaponHolder.rotation;
				kacs.weapon.transform.SetParent(rightWeaponHolder);
				resetHand = rightWeaponHolder;
			}

			break;
		}

		kacs.attackMode = false;

	}

	public void ResetWeapondHand(){

		kacs.weapon.transform.position = resetHand.position;
		kacs.weapon.transform.rotation = resetHand.rotation;
		kacs.weapon.transform.SetParent(resetHand);
	}

	public enum WeaponsToEquip{
		Up,
		Down,
		Left,
		Right
	}

	public void WeaponPathsUpdate(){

		if(upWeapon){
			if(upWeaponPath != upWeapon.prefabPath || upWeaponPath != string.Empty){
				upWeaponPath = upWeapon.prefabPath;
			}
		}else{
			upWeaponPath = string.Empty;
		}

		if(downWeapon){
			if(downWeaponPath != downWeapon.prefabPath || downWeaponPath != string.Empty){
				downWeaponPath = downWeapon.prefabPath;
			}
		}else{
			downWeaponPath = string.Empty;
		}

		if(rightWeapon){
			if(rightWeaponPath != rightWeapon.prefabPath || rightWeaponPath != string.Empty){
				rightWeaponPath = rightWeapon.prefabPath;
			}
		}else{
			rightWeaponPath = string.Empty;
		}

		if(leftWeapon){
			if(leftWeaponPath != leftWeapon.prefabPath || leftWeaponPath != string.Empty){
				leftWeaponPath = leftWeapon.prefabPath;
			}
		}else{
			leftWeaponPath = string.Empty;
		}

		Save();
	}

	public void Save(){
		WeaponManagerInfo wmi = new WeaponManagerInfo();
		wmi.uwPath = upWeaponPath;
		wmi.dwPath = downWeaponPath;
		wmi.lwPath = leftWeaponPath;
		wmi.rwPath = rightWeaponPath;

		SaveAndLoadSystem.Save<WeaponManagerInfo>(wmi, "WMI");
	}

	public void Load(){
		if(SaveAndLoadSystem.FileExist("WMI")){
			WeaponManagerInfo wmi = SaveAndLoadSystem.Load<WeaponManagerInfo>("WMI");

			upWeaponPath = wmi.uwPath;
			downWeaponPath = wmi.dwPath;
			leftWeaponPath = wmi.lwPath;
			rightWeaponPath = wmi.rwPath;
		}
	}

    void WeaponInit()
    {
        if (upWeaponPath != string.Empty)
        {
            upWeapon = Instantiate(Resources.Load(upWeaponPath, typeof(BaseWeapon)), upWeaponHolder.position, upWeaponHolder.rotation) as BaseWeapon;
            upWeapon.transform.parent = upWeaponHolder;
        }

        if (downWeaponPath != string.Empty)
        {
            downWeapon = Instantiate(Resources.Load(downWeaponPath, typeof(BaseWeapon)), downWeaponHolder.position, downWeaponHolder.rotation) as BaseWeapon;
            downWeapon.transform.parent = downWeaponHolder;
        }

        if (leftWeaponPath != string.Empty)
        {
            leftWeapon = Instantiate(Resources.Load(leftWeaponPath, typeof(BaseWeapon)), leftWeaponHolder.position, leftWeaponHolder.rotation) as BaseWeapon;
            leftWeapon.transform.parent = leftWeaponHolder;
        }

        if (rightWeaponPath != string.Empty)
        {
            rightWeapon = Instantiate(Resources.Load(rightWeaponPath, typeof(BaseWeapon)), rightWeaponHolder.position, rightWeaponHolder.rotation) as BaseWeapon;
            rightWeapon.transform.parent = rightWeaponHolder;
        }
    }

	void WeaponBasePlayerUpdate(){
		
		if(rightWeapon){
			rightWeapon.p = this.GetComponent<BasePlayer>();
		}

		if(leftWeapon){
			leftWeapon.p = this.GetComponent<BasePlayer>();
		}

		if(upWeapon){
			upWeapon.p = this.GetComponent<BasePlayer>();
		}

		if(downWeapon){
			downWeapon.p = this.GetComponent<BasePlayer>();
		}

	}
}
