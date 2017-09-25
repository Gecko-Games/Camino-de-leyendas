using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(VitalsManager))]
public class AbilityCaster : MonoBehaviour {

	[SerializeField] float timeToCharge = .5f, timeToDischarge = .5f;
	public string AbilityToCastName{get;set;}
	[SerializeField]List<GameObject> abilityToCast = new List<GameObject> ();

	[SerializeField]Transform originOfCast;
	[SerializeField]GameObject transformParticle;
	[SerializeField]GameObject auraParticle;
	public bool auraActive = false, autoCharge = false;
	[SerializeField]Color auraAndRimColor = Color.white;
	[SerializeField]int matAuraIndex;
	[SerializeField]Renderer playerAuraRenderer;
//	[SerializeField]int matRimIndex;
//	[SerializeField]Renderer playerBodyRenderer;
	public Renderer PlayerBodyRenderer{
		get{ return playerAuraRenderer; }
	}
	float tempTimerToCharge = 0f, tempTimerToDischarge = 0f, tempF = 0f;

	Color tempC = Color.clear;

	Material matAura; // matRim;
	BasePlayer player;
	Animator ani;
	VitalsManager vm;
	Transform target;
	GameObject tmpParticle;
	ParticleSystem ap;

	void Start(){

		ani = this.GetComponent<Animator>();

		vm = this.GetComponent<VitalsManager>();

		player = this.GetComponent<BasePlayer>();
		matAura = playerAuraRenderer.materials[matAuraIndex];
//		matRim = playerBodyRenderer.materials[matRimIndex];
	}

	void Update(){

		if(!vm.dead){
			
			ani.SetBool("Aura", auraActive);

			if(autoCharge){

				if(vm.curEnergy < vm.maxEnergy){
					if(tempTimerToCharge <= 0){
						vm.AddEnergy(1);
						tempTimerToCharge = timeToCharge;
					}else{
						tempTimerToCharge -= Time.deltaTime * 1f;
					}
				}

				if(vm.curEnergy >= vm.maxEnergy){
					vm.curEnergy = vm.maxEnergy;
				}


			}
		
			if(auraActive){


				autoCharge = false;
				if(tempF < 1){
					tempF += Time.deltaTime;
				}
				matAura.SetColor ("_Color", Color.Lerp(tempC, auraAndRimColor, Mathf.Clamp(tempF, 0f, 1f)));

//				matRim.SetColor ("_Color", Color.Lerp(tempC, auraAndRimColor, Mathf.Clamp(tempF, 0f, 1f)));


				if(vm.curHealth < vm.maxHealth){
					if(tempTimerToCharge <= 0){
						vm.AddHealth(1);
						tempTimerToCharge = timeToCharge;
					}else{
						tempTimerToCharge -= Time.deltaTime * 1f;
					}
				}

				if(vm.curHealth >= vm.maxHealth){
					vm.curHealth = vm.maxHealth;
				}

				if(vm.curEnergy > 0){
					if(tempTimerToDischarge <= 0){
						vm.SubtractEnergy(1);
						tempTimerToDischarge = timeToDischarge;
					}else{
						tempTimerToDischarge -= Time.deltaTime;
					}
				}else{
					auraActive = false;
				}
					
			}else{
				if(tmpParticle){
					GameObject.Destroy(tmpParticle);
				}
					
				autoCharge = true;
				if(tempF > 0){
					tempF -= Time.deltaTime;
				}
				matAura.SetColor ("_Color", Color.Lerp(tempC, auraAndRimColor, Mathf.Clamp(tempF, 0f, 1f)));

//				matRim.SetColor ("_Color", Color.Lerp(tempC, auraAndRimColor, Mathf.Clamp(tempF, 0f, 1f)));

			}

			playerAuraRenderer.materials[matAuraIndex] = matAura;
			playerAuraRenderer.materials[matAuraIndex].SetTextureOffset("_MainTexture", new Vector2(.4f, -.5f * Time.time));
			playerAuraRenderer.materials[matAuraIndex].SetTextureOffset("_Noise", new Vector2(0f, .25f * Time.time));
//			playerBodyRenderer.materials[matRimIndex] = matRim;
		}

	}

	public void Cast(){

		foreach(GameObject toCast in abilityToCast){
				if(toCast.name == AbilityToCastName){

				if(toCast.GetComponent<Habilidad>().element == InnateElement.Sangre){
					if (vm.curHealth >= toCast.GetComponent<Habilidad>().cost) {

						toCast.GetComponent<Habilidad> ().p = player;
						if(target){
							toCast.GetComponent<Habilidad> ().target = target;
						}else{
							toCast.GetComponent<Habilidad> ().target = null;
						}
						GameObject clone = Instantiate (toCast, originOfCast.position, this.transform.rotation) as GameObject;
						clone.transform.position = originOfCast.position;
						clone.SetActive(true);
						//Debug.Log(clone.transform.position.ToString());

						vm.AddHealth(-toCast.GetComponent<Habilidad>().cost);

					}
				}else{
					if (vm.curEnergy >= toCast.GetComponent<Habilidad>().cost) {

						toCast.GetComponent<Habilidad> ().p = player;
						if(target){
							toCast.GetComponent<Habilidad> ().target = target;
						}else{
							toCast.GetComponent<Habilidad> ().target = null;
						}
						GameObject clone = Instantiate (toCast, originOfCast.position, this.transform.rotation) as GameObject;
						clone.transform.position = originOfCast.position;
						clone.SetActive(true);
						//Debug.Log(clone.transform.position.ToString());

						if(!auraActive){
							vm.SubtractEnergy(toCast.GetComponent<Habilidad>().cost);
						}

					}
				}

			}
		}
	}
		

	public void FreedomState(){
		if(!auraActive){
			if(vm.curEnergy == vm.maxEnergy){
				ani.CrossFade("Transform", .1f);
				Instantiate(transformParticle, this.transform.position, Quaternion.identity);
				tmpParticle = Instantiate(auraParticle, Vector3.zero, Quaternion.identity) as GameObject;
				ap = tmpParticle.GetComponent<ParticleSystem>();
				var sh = ap.shape;
				sh.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
				sh.skinnedMeshRenderer = (SkinnedMeshRenderer)playerAuraRenderer;
				auraActive = true;
			}
		}else{
			auraActive = false;
			if(tmpParticle){
				GameObject.Destroy(tmpParticle);
			}
		}
	}

	public void SetTarget(Transform newTarget){
		target = newTarget;
	}

	public void AddAbilityToCast(GameObject ability){
		abilityToCast.Add (ability);
	}
}
