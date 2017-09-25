using UnityEngine;
using System.Collections;

public class Sword : BaseWeapon {

//	private AudioSource audioS;

	// Use this for initialization
	void Start () {
		SetUpCollider();
		WeaponTrail (false);
	}

	//// Update is called once per frame
	//void Update () {
	//	WeaponEffect();
	//}

	protected override void SetUpCollider(){
		ActivateCollider = false;
	}

	protected override void UpdateCollider(){
//		Debug.DrawRay (raycastOrigin.position, this.transform.TransformDirection (Vector3.up) * distance, Color.cyan);
		if(ActivateCollider){
			RaycastController ();
		}
	}

	void OnDrawGizmos(){
		if(raycastOrigin){
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere (raycastOrigin.position, radiusSizeGizmo);
			Gizmos.DrawRay (raycastOrigin.position, this.transform.TransformDirection (Vector3.up) * distance);
		}
	}

	void FixedUpdate(){
		UpdateCollider ();
	}
}
