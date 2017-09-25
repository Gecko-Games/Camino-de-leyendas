using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DamageTo{
	Player,
	Enemy
}

public enum WeapondType{
	Normal,
	Magic
}

public class BaseWeapon : MonoBehaviour {
	[Header("Componentes base")]
	public string prefabPath = string.Empty;
	[Space(.5f)]
	[Header("Define el tipo de arma (Normal o Magica)")]
	public WeapondType wt = WeapondType.Normal;

	[Header("Nombre")]
	public string nombre;
	[Header("Descripcion")]
	public string descripcion;

	public List<KickAssAbilityMove> moveList;

	[Header("Layer al que se le va a generar daño (Player / Enemy / Environment / Etc)")]
	[SerializeField]private LayerMask collisionMask;

	[Header("Origen del raycast")]
	[SerializeField]protected Transform raycastOrigin;
	[SerializeField]protected float radiusSizeGizmo = .05f;
	private RaycastHit hit;

	[Header("Efecto de golpe")]
	[SerializeField]private GameObject goHit;

	[Header("Distancia de ataque")]
	[SerializeField]protected float distance = 1f;

	[Header("Daño base del arma")]
	[SerializeField]private float baseDamage = 1f;

	[Header("Multiplicador de daño basado en la clase BasePlayer")]
	public float multiplierMod = 10f;

	[Header("Esfecto trail del arma")]
	[SerializeField]protected ParticleSystem weaponTrail;

	public BasePlayer p;

	private InnateElement element = InnateElement.Neutro;

	private int tempDamage = 0;

	private float criD = 0f;

	private bool tempCrit = false;
	private KickAssCombatSystem kacs;
	private AbilityCaster ac;

	public BasePlayer PlayerStats{
		get{return p;}
		set{p = value;}
	}

	public InnateElement Elemento{
		get{return element;}
		set{element = value;}
	}

	public bool ActivateEffect {
		get;
		set;
	}

	public bool ActivateCollider {
		get;
		set;
	}

	protected virtual int ReturnDamage(){

		if(p != null){
			switch(Elemento){

			case InnateElement.Neutro:
				tempDamage = p.Fuerza.Valor + (int)MultiplierDamage(p.Fuerza.Valor);
				break;

			case InnateElement.Fuego:
				tempDamage = p.Fuego.Valor + (int)MultiplierDamage(p.Fuego.Valor);
				break;

			case InnateElement.Viento:
				tempDamage = p.Viento.Valor + (int)MultiplierDamage(p.Viento.Valor);
				break;

			case InnateElement.Rayo:
				tempDamage = p.Rayo.Valor + (int)MultiplierDamage(p.Rayo.Valor);
				break;

			case InnateElement.Tierra:
				tempDamage = p.Tierra.Valor + (int)MultiplierDamage(p.Tierra.Valor);
				break;

			case InnateElement.Agua:
				tempDamage = p.Agua.Valor + (int)MultiplierDamage(p.Agua.Valor);
				break;
			}

			if(!ac){
				ac = (AbilityCaster)FindObjectOfType(typeof(AbilityCaster));
			}else{
				if(ac.auraActive){
					tempDamage += (int)baseDamage;
					tempDamage *= 2;
				}else{
					tempDamage += (int)baseDamage;
				}
			}
		}

		return tempDamage;
	}
	
	protected virtual void WeaponEffect(){

        ParticleSystem.EmissionModule emi = weaponTrail.emission;

        if (ActivateEffect){
           
            emi.enabled = true;

		}else{
            emi.enabled = false;
        }

	}

	public void WeaponTrail(bool active){

        if (weaponTrail)
        {
            ParticleSystem.EmissionModule emi = weaponTrail.emission;

            if (active)
            {
                emi.enabled = true;
            }
            else
            {
                emi.enabled = false;
            }
        }
        
	}

	protected virtual void SetUpCollider(){}

	protected virtual void UpdateCollider(){}

	protected void RaycastController(){
		if(Physics.Raycast(raycastOrigin.position, this.transform.TransformDirection (Vector3.up), out hit, distance, collisionMask, QueryTriggerInteraction.Collide)){

			if (hit.transform.tag != "Environment") {
				VitalsManager enemy = hit.transform.GetComponent<VitalsManager>();
				enemy.SubtractHealth ((int)CriticalChance((float)ReturnDamage()), Elemento);

				//Debug.Log("Multiplier itemValue: " + MultiplierDamage((int)baseDamage));
			}

			Instantiate(goHit, hit.point, Quaternion.Euler(hit.normal));
		}
	}

	protected float CriticalChance(float d){
		float tempChance = Random.Range(0f, 100f);

		float critRate = p.Suerte.Valor / 10f;

		if(tempChance <= critRate){
			criD = d * 2f;
			tempCrit = true;

			kacs = (KickAssCombatSystem)FindObjectOfType(typeof(KickAssCombatSystem));
			kacs.SetSlowMotion(.5f);
			//Debug.Log("Critico!! " + criD);
			//Debug.Log("Critico % " + critRate);
			//Debug.Log("Chance " + tempChance);
			return criD;
		}else{
			criD = d;
			tempCrit = false;
			//Debug.Log("Normal " + criD);
			//Debug.Log("Critico % " + critRate);
			//Debug.Log("Chance " + tempChance);
			return criD;
		}
	}

	protected float MultiplierDamage(int originalDamage){
		float tempPorcentaje = originalDamage * multiplierMod;
		return tempPorcentaje;
	}
}
