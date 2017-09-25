using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Proyectil : Habilidad {

	public float speed = 20f, smoothLookAt = 6.0f;

	public GameObject explosion, originExplosion;

//	public GameObject goTexto;

//	private HitText hitT;

	private Rigidbody rb;

	private int tempDamage = 0;

	private bool tempCrit = false;

	private float criD = 0f;

    	private Vector3 originBorn;

	// Use this for initialization
	void Start () {
        	originBorn = transform.position;
		
		rb = this.GetComponent<Rigidbody>();
		SphereCollider sc = this.GetComponent<SphereCollider>();
		sc.isTrigger = true;
		Instantiate(originExplosion, transform.position, Quaternion.identity);

	}
		
	void FixedUpdate()
	{
		if (target != null)
		{
			Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
		        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothLookAt);

		        if (Vector3.Distance(transform.position, originBorn) > distance)
		        {
				Explode();
		        }
		}
		else
		{
			if (Vector3.Distance(transform.position, originBorn) > distance)
		        {
				Explode();
		        }
		}
		rb.velocity = transform.forward * speed;
	}
	

	public override int ReturnDamage(){

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

		return tempDamage;
	}

	void Explode(){
		this.GetComponentInChildren<UnityStandardAssets.Utility.ParticleSystemDestroyer> ().enabled = true;
		this.GetComponentInChildren<Transform>().transform.DetachChildren();
		explosion.GetComponent<Habilidad> ().p = p;
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void DurationAbility(){

		if (duration > 0) {
			duration -= Time.deltaTime;
		} else {
			Explode();
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

	void OnTriggerEnter(Collider other)  {

		switch(targetType){
		case TargetType.Enemy:

			if(other.CompareTag("Enemy")){
				
				Explode ();
						VitalsManager enemy = other.gameObject.GetComponent<VitalsManager> ();
						enemy.SubtractHealth ((int)CriticalChance((float)ReturnDamage()), Elemento);
				//		hitT =  goT.GetComponent<HitText>();
				//		hitT.critHit = tempCrit;
				//		hitT.text = collision.gameObject.GetComponent<SaludVisual>().ReturnResult().ToString();

//				Instantiate(goTexto, transform.position, transform.rotation);

			}else if(other.CompareTag("Environment")){
				
				Explode ();

			}

			break;

			case TargetType.Player:

			if(other.CompareTag("Player")){

				Explode ();
					VitalsManager enemy = other.gameObject.GetComponent<VitalsManager> ();
					enemy.SubtractHealth ((int)CriticalChance((float)ReturnDamage()), Elemento);
				//		hitT =  goT.GetComponent<HitText>();
				//		hitT.critHit = tempCrit;
				//		hitT.text = collision.gameObject.GetComponent<SaludVisual>().ReturnResult().ToString();

//				Instantiate(goTexto, transform.position, transform.rotation);

			}else if(other.CompareTag("Environment")){

				Explode ();

			}

			break;
		}
	}

}
