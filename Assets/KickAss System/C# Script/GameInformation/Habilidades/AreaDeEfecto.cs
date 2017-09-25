using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class AreaDeEfecto : Habilidad {

	public float radius = 1f, forcePower = 10f;

	public bool thisDestroy = true;

//	public GameObject goTexto;
	
//	private HitText hitT;

	private float damageTimer = .2f;

	private SphereCollider sc;

	private int tempDamage = 0;

	private bool tempCrit = false;
	
	private float criD = 0f;

	void Start () {

		sc = this.GetComponent<SphereCollider>();
//		sc.radius = radius * .5f;
		sc.isTrigger = true;
		
	}

	// Update is called once per frame
	void Update () {
		if(thisDestroy){
			DurationAbility ();
		}
	}
	

	public override int ReturnDamage(){
		if(p != null){
			switch(Elemento){
				
			case InnateElement.Neutro:
				tempDamage = p.Fuerza.Valor + (int)MultiplierDamage(p.Fuerza.Valor);
				break;

			case InnateElement.Sangre:
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
		}
		
		return tempDamage;
	}

	public void DurationAbility(){
		
		if (duration > 0) {
			duration -= Time.deltaTime;
		} else {
			Destroy(this.gameObject);
		}
		
	}

	float CriticalChance(float d){
		float tempChance = Random.Range(0f, 100f);
		
		float critRate = p.Suerte.Valor / 10f;
		
		if(tempChance <= critRate){
			criD = d * 2;
			tempCrit = true;
			
			Debug.Log("Critico!! " + criD);
			Debug.Log("Critico % " + critRate);
			Debug.Log("Chance " + tempChance);
			return criD;
		}else{
			criD = d;
			tempCrit = false;
			Debug.Log("Normal " + criD);
			Debug.Log("Critico % " + critRate);
			Debug.Log("Chance " + tempChance);
			return criD;
		}
	}

	void OnTriggerStay(Collider other) {


		switch(targetType){
		case TargetType.Enemy:

			if(other.CompareTag("Enemy")){

				if (other.attachedRigidbody && other.isTrigger == false) {

                        other.attachedRigidbody.AddExplosionForce(forcePower, this.transform.position, radius, 0f, ForceMode.VelocityChange);
                    }

				if (damageTimer > 0f) {
					damageTimer -= Time.deltaTime;
				} else {
					damageTimer = .2f;
						VitalsManager enemy = other.gameObject.GetComponent<VitalsManager> ();
						enemy.SubtractHealth ((int)CriticalChance((float)ReturnDamage()), Elemento);
					//			hitT =  goT.GetComponent<HitText>();
					//			hitT.critHit = tempCrit;
					//			hitT.text = other.gameObject.GetComponent<SaludVisual>().ReturnResult().ToString();

//					Instantiate(goTexto, this.transform.position, this.transform.rotation);
				}

			}else if(other.CompareTag("Environment")){

				if (other.attachedRigidbody && other.isTrigger == false) {

					other.attachedRigidbody.AddExplosionForce (forcePower, this.transform.position, radius, 0f, ForceMode.VelocityChange);
				
				}
			}

			break;

		case TargetType.Player:

			if(other.CompareTag("Player")){

				if (other.attachedRigidbody && other.isTrigger == false) {

                        other.attachedRigidbody.AddExplosionForce(forcePower, this.transform.position, radius, 0f, ForceMode.VelocityChange);
                    }

				if (damageTimer > 0f) {
					damageTimer -= Time.deltaTime;
				} else {
					damageTimer = .2f;
						VitalsManager enemy = other.gameObject.GetComponent<VitalsManager> ();
						enemy.SubtractHealth ((int)CriticalChance((float)ReturnDamage()), Elemento);
					//			hitT =  goT.GetComponent<HitText>();
					//			hitT.critHit = tempCrit;
					//			hitT.text = other.gameObject.GetComponent<SaludVisual>().ReturnResult().ToString();

//					Instantiate(goTexto, this.transform.position, this.transform.rotation);
				}

			}else if(other.CompareTag("Environment")){

				if (other.attachedRigidbody && other.isTrigger == false) {

					other.attachedRigidbody.AddExplosionForce (forcePower, this.transform.position, radius);
				}

			}

			break;
		}
			
	}
}
