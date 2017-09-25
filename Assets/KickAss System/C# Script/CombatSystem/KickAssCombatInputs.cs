using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KickAssCombatInputs : MonoBehaviour {

	public Mesh gizmoMesh;
	public Color gizmosColor = Color.yellow;
	public GameObject warpParticle;
	public List<KickAssAbilityMove> weapondAttack, abilityAttack;
	public int indexWeapondAttack, indexAbilityAttack;

	public bool canAttack = true, defence = false, canEnterInputs = true;

	public Transform target;
	public Transform targetSigner;

	public List<Transform> targets;
	public float distanceToSeeTarget = 30f;

	private KickAssCombatSystem kacs;
	private KickAssCameraController kacc;
	private GamePadInputs gpi;
	private AbilityCaster hc;
	private Transform targetS;
	private float pushTimer = .5f;
	private bool quickMenu = false;
	private VitalsManager vm;
	private GameObject tmpParticle;
	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		kacs = this.GetComponent<KickAssCombatSystem>();
		kacc = (KickAssCameraController)FindObjectOfType(typeof(KickAssCameraController));
		gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
		hc = this.GetComponent<AbilityCaster>();
		vm = this.GetComponent<VitalsManager>();

		if(targetSigner){
			targetS = (Transform)Instantiate(targetSigner, Vector3.zero, Quaternion.identity);
			targetS.gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		if(!vm.dead){

			if(canEnterInputs){
				if(gpi.l1.isActive){
					pushTimer -= Time.deltaTime;
				}

				if(pushTimer <= 0f){
					if(gpi.l1.isUp){
						pushTimer = .5f;
						hc.FreedomState();
					}
				}else{
					if(gpi.l1.isUp)
                    {
						if(quickMenu){
							quickMenu = false;
							TimeManagger.NormalizeTime();
						}else{
							quickMenu = true;
							TimeManagger.AlterateTime(.25f);
						}

						pushTimer = .5f;
					}
				}

				if(targetS){
					if(target){
						targetS.position = target.position;
						targetS.rotation = kacc.transform.rotation;
						targetS.gameObject.SetActive(true);
						target.GetComponent<VitalsManager>().expBar.visible = true;
						target.GetComponent<VitalsManager>().hpBar.visible = true;
					}else{
						targetS.gameObject.SetActive(false);
					}
				}

				AttackModeActions();
				FindTarget();
				Evasion();
			}
		}

	}

	void AttackModeActions(){
		if(kacs.attackMode){
			if(kacs.lastAttack == false){
				canAttack = true;
			}else{
				ResetAttack();
			}

			if(kacs.canMove == true){
				ResetAttack();
				kacs.WeaponColActivation = 0;
				kacs.WeaponEffectActivation = 0;
			}



			if(canAttack){
				if(kacs.canAdd){
					if(gpi.x.isUp){
						SetTarget();
						kacs.canMove = false; 
						kacs.addAttacksToList(weapondAttack[indexWeapondAttack]);
						//indexWeapondAttack++;
						if(indexWeapondAttack >= weapondAttack.Count){
							canAttack = false;
						}
					}

					if(gpi.y.isUp)
                    {
						SetTarget();
						kacs.canMove = false;
						kacs.addAttacksToList(abilityAttack[indexAbilityAttack]);
						indexAbilityAttack++;
						if(indexAbilityAttack >= abilityAttack.Count){
							canAttack = false;
						}
					}
				}
			}else{
				ResetAttack();
			}
		}else{
			ResetAttack();
		}
	}

	void ResetAttack(){
		indexWeapondAttack = 0;
		indexAbilityAttack = 0;
	}

	void Evasion(){
		if(gpi.b.isUp)
        {
			kacs.canMove = true;
			canAttack = true;

			if(warpParticle){
				if(hc.auraActive){
					tmpParticle = Instantiate(warpParticle, Vector3.zero, Quaternion.identity) as GameObject;
					ps = tmpParticle.GetComponent<ParticleSystem>();
					var sh = ps.shape;
					sh.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
					sh.skinnedMeshRenderer = (SkinnedMeshRenderer)hc.PlayerBodyRenderer;
				}
			}
			kacs.Evasion();
		}
	}

	private void SetTarget(){
		if(!target){
			AddTargets();
			if(targets.Count > 0){
				kacs.target = target;
				kacc.enemyTarget = target;
				hc.SetTarget(target);
			}else{
				kacs.target = null;
				//kacc.enemyTarget = null;
				hc.SetTarget(null);
			}
		}
	}

	private void FindTarget(){

		if(gpi.r3.isUp)
        {
			if(!target){
				SetTarget();
			}else{
				target = null;
				kacs.target = null;
				kacc.enemyTarget = null;
			}

		}

		if(target){
			if(Vector3.Distance(this.transform.position, target.position) > distanceToSeeTarget){
				target = null;
				kacs.target = null;
				kacc.enemyTarget = null;
			}
		}

	}

	private void AddTargets(){

		targets.Clear();

		if(!target){

			Collider[] colTargets = Physics.OverlapSphere(transform.position, distanceToSeeTarget); 

			foreach(Collider curTarget in colTargets){
				if(curTarget.CompareTag("Enemy")){
					targets.Add(curTarget.transform);
				}
			}

			if(targets.Count <= 0){
				return;
			}else{
				SortTargetByDistance();
				target = targets[0];
			}
		}
	}

	private void SortTargetByDistance(){
		targets.Sort(
			delegate(Transform t1, Transform t2) {
				return(Vector3.Distance(t1.position, transform.position).CompareTo(Vector3.Distance(t2.position, transform.position)));
			}
		);
	}

	public void CanEnterInputs(bool can){
		canEnterInputs = can;
	}

	void OnDrawGizmos() {
		if(gizmoMesh){

			Gizmos.color = gizmosColor;
			Gizmos.DrawMesh(gizmoMesh, this.transform.position,Quaternion.identity, Vector3.one * distanceToSeeTarget * 2);

		}else{
			return;
		}

	}
}