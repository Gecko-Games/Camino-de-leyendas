using UnityEngine;

namespace LobinuxSoft.KickAssSystem.ThirdPersonCharacter
{

	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class KickAssCharacterMotorPrototype : MonoBehaviour
	{
		public float turnSmoothing = 15f;
		public float speedMove = 1.5f;
		public float jumpPower = 5f;
		public float speedDampTime = 0.1f;
		public float groundCheckDistance = .3f;
		public bool isGround = true;
		public bool jump,sprint;

		private Animator anim;
		private Rigidbody rig;
		private Transform cam;
		private Vector3 m_GroundNormal;
		private float h,v;

		private Quaternion targetRotation = Quaternion.identity;

		void Awake(){
			anim = GetComponent<Animator>();
			rig = GetComponent<Rigidbody>();
			cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
		}

		void Update(){
			ControlInputs(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),Input.GetKey(KeyCode.Space),Input.GetKey(KeyCode.LeftShift));
		}

		void FixedUpdate(){
			MovementManagement(h,v,jump,sprint);
			CheckGroundStatus();
		}

		public void ControlInputs(float x, float y, bool j, bool s){
			h = x;
			v = y;
			jump = j;
			sprint = s;
		}

		void MovementManagement(float horizontal, float vertical, bool jumping, bool sprinting){

			anim.SetBool("Sprint", sprinting);

			if(isGround){
				if(jumping){
					rig.velocity = new Vector3(rig.velocity.x, jumpPower, rig.velocity.z);
					anim.SetBool("Ground", false);
				}
			}

			anim.SetBool("Ground", isGround);

			float moveLerp = new Vector2(horizontal, vertical).magnitude;

			if(horizontal != 0 || vertical != 0){
				Rotating(horizontal, vertical);
				anim.speed = speedMove;
				anim.SetFloat("Speed", moveLerp, speedDampTime, Time.deltaTime);
			}else{
				anim.SetFloat("Speed", 0f);
				anim.speed = 1f;
			}

		}

		void Rotating(float horizontal, float vertical){

			Vector3 targetDirection = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
			Vector3 moveDir = vertical*targetDirection + horizontal*cam.right;
			if(moveDir != Vector3.zero){
				targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
			}
			Quaternion newRotation = Quaternion.Lerp(rig.rotation, targetRotation, turnSmoothing * Time.deltaTime);

			rig.MoveRotation(newRotation);

		}

		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
			#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance), Color.red);
			#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
			{
	//			m_GroundNormal = hitInfo.normal;
				isGround = true;
				anim.applyRootMotion = true;
			}
			else
			{
				isGround = false;
	//			m_GroundNormal = Vector3.up;
				anim.applyRootMotion = false;
			}
		}
	}
}
