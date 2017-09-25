using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BasePlayer))]
public class VitalsManager : MonoBehaviour {

	public bool test, dead = false, enemy = false;

	public int curHealth = 100, maxHealth = 100, curEnergy = 3, maxEnergy = 100;

	public ProgresBar hpBar, euBar, expBar;
	public Text levelText;

	private BasePlayer bp;
	public BasePlayer tempPlayer;

	private float hpVTimer = 3f, euVTimer = 3f, expVTimer = 3f;
	private Animator ani;
	private KickAssCombatSystem kacs;

	// Use this for initialization
	void Start () {

		if(!enemy){
			if(!ani){
				ani = GetComponent<Animator>();
			}

			if(!kacs){
				kacs = GetComponent<KickAssCombatSystem>();
			}
		}

		if(!test){
			bp = this.GetComponent<BasePlayer>();

			if(bp){
				maxHealth = bp.Salud.Valor + bp.Resistencia.Valor;
				maxEnergy = bp.Energia.Valor + bp.Voluntad.Valor;
			}
		}

		curHealth = maxHealth;
		curEnergy = maxEnergy;



	}
	
	// Update is called once per frame
	void Update () {

		if(enemy){
			if(!tempPlayer){
				tempPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<BasePlayer>();
			}
		}

		if(bp){
			maxHealth = bp.Salud.Valor + bp.Resistencia.Valor;
			maxEnergy = bp.Energia.Valor + bp.Voluntad.Valor;
		}

		if(hpBar){
			if(hpBar.visible){
				if(curHealth >= maxHealth){
					if(hpVTimer > 0){
						hpVTimer -= Time.deltaTime;
					}else{
						hpBar.visible = false;
					}
				}
			}else{
				hpVTimer = 3f;
			}
		}

		if(euBar){
			if(euBar.visible){
				if(curEnergy == maxEnergy){
					if(euVTimer > 0){
						euVTimer -= Time.deltaTime;
					}else{
						euBar.visible = false;
					}
				}
			}else{
				euVTimer = 3f;
			}
		}

		if(curHealth < 0){
			curHealth = 0;
		}else if(curHealth == 0){
			Death();
		}

		if(hpBar){
			hpBar.SetMax(maxHealth);
			hpBar.SetCurrentValue(curHealth);
		}

		if(euBar){
			euBar.SetMax(maxEnergy);
			euBar.SetCurrentValue(curEnergy);
		}

		if(expBar){
			expBar.SetMax(bp.RequiredXP);
			expBar.SetCurrentValue(bp.CurrentXP);
		}

		if(expBar){
			if(expBar.visible){
				if(expVTimer > 0){
					expVTimer -= Time.deltaTime;
				}else{
					expBar.visible = false;
				}
			}else{
				expVTimer = 3f;
			}
		}

		if(levelText){
			levelText.text = "Lv " + bp.PlayerLevel;
		}

	}


	public void AddHealth(int addValue){
		hpBar.visible = true;
		curHealth += addValue;
	}

	public void SubtractHealth(int subtractValue, InnateElement elementType){

		if(hpBar){
			hpBar.visible = true;
		}

		if(bp){
			switch(elementType){
			case InnateElement.Fuego:
				curHealth -= (subtractValue - (bp.Resistencia.Valor + bp.Fuego.Valor));
				break;
			case InnateElement.Viento:
				curHealth -= (subtractValue - (bp.Resistencia.Valor + bp.Viento.Valor));
				break;
			case InnateElement.Rayo:
				curHealth -= (subtractValue - (bp.Resistencia.Valor + bp.Rayo.Valor));
				break;
			case InnateElement.Tierra:
				curHealth -= (subtractValue - (bp.Resistencia.Valor + bp.Tierra.Valor));
				break;
			case InnateElement.Agua:
				curHealth -= (subtractValue - (bp.Resistencia.Valor + bp.Agua.Valor));
				break;
			case InnateElement.Neutro:
				curHealth -= (subtractValue - bp.Resistencia.Valor);
				break;

			case InnateElement.Sangre:
				curHealth -= (subtractValue - bp.Resistencia.Valor);
				break;
			}
		}else{
			curHealth -= subtractValue;
		}

		if(ani){
			ani.SetLayerWeight(2, 1f);
			ani.SetTrigger("TakeDamage");
		}

		if(kacs){
			kacs.ClearAttackList();
		}

	}


	public void AddEnergy(int addValue){
		if(euBar){
			euBar.visible = true;
		}
		curEnergy += addValue;
	}

	public void SubtractEnergy(int subtractValue){
		if(euBar){
			euBar.visible = true;
		}
		curEnergy -= subtractValue;
	}

	public void Death(){
		Debug.Log("I'am dead");

		if(enemy){
			tempPlayer.AddExp(250);
			tempPlayer.GetComponent<VitalsManager>().expBar.visible = true;
			Destroy(this.gameObject);
		}else{
			if(!dead){
				dead = true;
				ani.SetTrigger("Dead");
				ani.SetLayerWeight(1,0f);
				ani.SetLayerWeight(2,0f);
			}
		}


	}

	public void ReBorn(){
		if(dead){
			curHealth = maxHealth;
			curEnergy = maxEnergy;
			dead = false;
			ani.SetTrigger("Reborn");
			ani.SetLayerWeight(1,1f);
			ani.SetLayerWeight(2,1f);
		}
	}

	public void DamageLayerOff(){
		ani.SetLayerWeight(2, 0f);
	}

}
