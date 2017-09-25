using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class KickAssCharacterMotor : MonoBehaviour
{

	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float m_GroundCheckDistance = 0.3f;
	
	Rigidbody m_Rigidbody;
	Animator m_AnitempMator;
	bool m_IsGrounded;
	float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	public float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;
	bool m_Crouching;
	bool m_Sprint;
	AbilityCaster hc;

	
	void Start()
	{
		m_AnitempMator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;
		
		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;

		hc = GetComponent<AbilityCaster>();
	}
	
	
	public void Move(Vector3 move, bool crouch, bool jump, bool sprint)
	{
		m_Sprint = sprint;
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f){ 
			move.Normalize();
		}

		if(m_Sprint){
			move *= 2f;
		}

		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;
		ApplyExtraTurnRotation();
		
		// control and velocity handling is different when grounded and airborne:
		if (m_IsGrounded)
		{
			HandleGroundedMovement(crouch, jump);
		}
		else
		{
			HandleAirborneMovement();
		}
		
		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom();
		
		// send input and other state parameters to the anitempMator
		UpdateAnitempMator(move);
	}
	
	
	void ScaleCapsuleForCrouching(bool crouch)
	{
		if (m_IsGrounded && crouch)
		{
			if (m_Crouching) return;
			m_Capsule.height = m_Capsule.height / 2f;
			m_Capsule.center = m_Capsule.center / 2f;
			m_Crouching = true;
		}
		else
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength))
			{
				m_Crouching = true;
				return;
			}
			m_Capsule.height = m_CapsuleHeight;
			m_Capsule.center = m_CapsuleCenter;
			m_Crouching = false;
		}
	}
	
	void PreventStandingInLowHeadroom()
	{
		// prevent standing up in crouch-only zones
		if (!m_Crouching)
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength))
			{
				m_Crouching = true;
			}
		}
	}
	
	
	void UpdateAnitempMator(Vector3 move)
	{

		if(hc.auraActive){
			m_AnitempMator.speed = 2f;
		}else{
			m_AnitempMator.speed = 1f;
		}

		// update the anitempMator parameters
		m_AnitempMator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_AnitempMator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		m_AnitempMator.SetBool("Parkour", m_Sprint);
		m_AnitempMator.SetBool("Crouch", m_Crouching);
		m_AnitempMator.SetBool("OnGround", m_IsGrounded);
		if (!m_IsGrounded)
		{
			m_AnitempMator.SetFloat("Jump", m_Rigidbody.velocity.y);
		}
		
		// calculate which leg is behind, so as to leave that leg trailing in the jump anitempMation
		// (This code is reliant on the specific run cycle offset in our anitempMations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				m_AnitempMator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
		if (m_IsGrounded)
		{
			m_AnitempMator.SetFloat("JumpLeg", jumpLeg);
		}
		
		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (m_IsGrounded && move.magnitude > 0)
		{
			if(hc.auraActive){
				m_AnitempMator.speed = m_AnimSpeedMultiplier * 1.5f;
			}else{
				m_AnitempMator.speed = m_AnimSpeedMultiplier;
			}

//				m_AnitempMator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_AnitempMator.speed = 1;
		}
	}
	
	
	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);
		
		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
	}
	
	
	void HandleGroundedMovement(bool crouch, bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && !crouch && m_AnitempMator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		{
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
			m_IsGrounded = false;
			m_AnitempMator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}else if (jump && !crouch && m_AnitempMator.GetCurrentAnimatorStateInfo(0).IsName("Grounded Parkour"))
		{
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower * 1.5f, m_Rigidbody.velocity.z);
			m_IsGrounded = false;
			m_AnitempMator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}
	
	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the anitempMation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}
	
	
	public void OnAnitempMatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (m_IsGrounded && Time.deltaTime > 0)
		{
			Vector3 v;
			if(m_Sprint){
				v = ((m_AnitempMator.deltaPosition * m_MoveSpeedMultiplier)* 1.5f) / Time.deltaTime;
			}else{
				v = (m_AnitempMator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
			}
			
			// we preserve the existing y part of the current velocity.
			if(m_Sprint){
				v.y = (m_Rigidbody.velocity.y * 2f);
			}else{
				v.y = m_Rigidbody.velocity.y;
			}
			m_Rigidbody.velocity = v;
		}
	}
	
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_GroundNormal = hitInfo.normal;
			m_IsGrounded = true;
			m_AnitempMator.applyRootMotion = true;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_AnitempMator.applyRootMotion = false;
		}
	}
}
