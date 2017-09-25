using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class LightningBlade : BaseWeapon {

	[Header("Especifico para el efecto")]

	public GameObject goHit;

	public Renderer rend, blade;

	public Material mat;

	public float speedBlend = .2f;

	private CapsuleCollider cc;

	private Material tempMat, bladeMat;

	private ParticleSystem[] ps;

	private LightningCreator lc;

	private AudioSource audioS;

	private float intencity = 0f;

	// Use this for initialization
	void Start () {
		SetUpCollider();
		lc = this.GetComponent<LightningCreator>();
		ps = this.GetComponentsInChildren<ParticleSystem>();
		audioS = this.GetComponentInChildren<AudioSource>();
		rend = this.GetComponent<Renderer>();
		tempMat = mat;
		tempMat.SetFloat("_Intensity", intencity);
		bladeMat = mat;
		bladeMat.SetFloat("_Intensity", intencity);
        //weaponTrail.Init();
	}
	
	// Update is called once per frame
	void Update () {
		WeaponEffect();
		UpdateCollider();
	}

	protected override void SetUpCollider(){
		ActivateCollider = false;
	}

	protected override void UpdateCollider(){
		if(ActivateCollider){
			RaycastController ();
		}
	}
		
	protected override void WeaponEffect(){

		if(ActivateEffect){
			if (weaponTrail.gameObject.active == false) {
				//weaponTrail.Activate();
			}

			//xTrail.enabled = true;
			audioS.enabled = true;
			lc.enabled = true;
			lc.timeToInstance = 0f;
			lc.lType = LightningCreator.LightningType.Linear;

			blade.enabled = true;
			rend.enabled = true;

			for(int i = 0; i < ps.Length; i++){
				ps[i].enableEmission = true;
			}

			if(intencity < 1f){
				intencity += Time.deltaTime * speedBlend;
				tempMat.SetFloat("_Intensity", intencity);
				bladeMat.SetFloat("_Intensity", intencity);
				rend.material = tempMat;
				blade.material = bladeMat;
			}
		}else{
			if (weaponTrail.gameObject.active == true)
			{
				//weaponTrail.Deactivate();
			}
			//xTrail.enabled = false;
			audioS.enabled = false;
			lc.enabled = false;
			for(int i = 0; i < ps.Length; i++){
				ps[i].enableEmission = false;
			}

			if(intencity > 0f){
				intencity -= Time.deltaTime * speedBlend;
				tempMat.SetFloat("_Intensity", intencity);
				bladeMat.SetFloat("_Intensity", intencity);
				rend.material = tempMat;
				blade.material = bladeMat;
			}
			if(intencity < 0f){
				tempMat.SetFloat("_Intensity", 0f);
				bladeMat.SetFloat("_Intensity", 0f);
				rend.material = tempMat;
				blade.material = bladeMat;
				intencity = 0f;
			}
		}
	}
}
