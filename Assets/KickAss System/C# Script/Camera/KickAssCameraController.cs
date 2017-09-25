using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KickAssProtectCameraFromWallClip))]
public class KickAssCameraController : KickAssBasePivotCam{
	// This script is designed to be placed on the root object of a camera rig,
	// comprising 3 gameobjects, each parented to the next:

	// 	Camera Rig
	// 		Pivot
	// 			Camera

	[SerializeField] private float m_MoveSpeed = 5f;                      // How fast the rig will move to keep up with the target's position.
	[Range(0f, 10f)] [SerializeField] private float m_TurnSpeed = 5f;   // How fast the rig will rotate from user input.
	[SerializeField] private float m_TurnSmoothing = 1f;                // How much smoothing to apply to the turn input, to reduce mouse-turn jerkiness
	[SerializeField] private float m_TiltMax = 75f;                       // The maximum itemValue of the x axis rotation of the pivot.
	[SerializeField] private float m_TiltMin = 45f;                       // The minimum itemValue of the x axis rotation of the pivot.
	[SerializeField] private bool m_LockCursor = false;                   // Whether the cursor should be hidden and locked.
	[SerializeField] private bool m_AutoReturn = false;           // set wether or not the vertical axis should auto return

	public Vector3 camOffset = new Vector3(0f, 1.5f, 0f), camTargetEnemyOffset = new Vector3(.5f, 0f, 1f);
	public bool usingGyro = false;
	public Transform enemyTarget;
	private GamePadInputs gpi;
	private float m_LookAngle;                    // The rig's y axis rotation.
	private float m_TiltAngle;                    // The pivot's x axis rotation.
	private const float k_LookDistance = 100f;    // How far in front of the pivot the character's look target is.
	private Vector3 m_PivotEulers;
	private Quaternion m_PivotTargetRot;
	private Quaternion m_TransformTargetRot;

	private float x = 0f, y = 0f;

	protected override void Awake()
	{
		base.Awake();
		// Lock or unlock the cursor.
		Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !m_LockCursor;
		m_PivotEulers = m_Pivot.rotation.eulerAngles;

		m_PivotTargetRot = m_Pivot.transform.localRotation;
		m_TransformTargetRot = transform.localRotation;
	}

	protected override void Start()
	{

		gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
		if(usingGyro == true){
			m_Pivot = m_Cam.parent.parent;
		}else{
			m_Pivot = m_Cam.parent;
		}


	}

	protected void Update()
	{

		m_AutoReturn = gpi.r3.isActive;

		if(!enemyTarget){


			#if MOBILE_INPUT
			if(gpi.r1.isActive){	
				if((gpi.horizontalLJoystick.aValue > .5f || gpi.horizontalLJoystick.aValue < -.5f) && gpi.verticalLJoystick.aValue >= .25f){
					m_LookAngle = Target.rotation.eulerAngles.y;
				}
			}
			#endif


			HandleRotationMovement();

		}else{

			x = 0f;
			y = 0f;

			m_TiltAngle = 0f;
			m_LookAngle = m_Pivot.rotation.eulerAngles.y;

			var targetRotation = Quaternion.LookRotation(new Vector3(enemyTarget.position.x - transform.position.x, 0f, enemyTarget.position.z - transform.position.z));

			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_TurnSpeed * Time.deltaTime);
		}

		if (m_LockCursor && Input.GetMouseButtonUp(0))
		{
			Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = !m_LockCursor;
		}
	}


	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}


	protected override void FollowTarget(float deltaTime)
	{
		if (m_Target == null) return;
		// Move the rig towards target position.
		if(!enemyTarget){

			transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime*m_MoveSpeed);
			m_Pivot.localPosition = Vector3.Slerp(m_Pivot.localPosition, camOffset, Time.deltaTime);

		}else{
			transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime*m_MoveSpeed);
			Vector3 newOffset = camOffset + camTargetEnemyOffset;
			m_Pivot.localPosition = Vector3.Slerp(m_Pivot.localPosition, newOffset, Time.deltaTime);
		}
	}


	private void HandleRotationMovement()
	{
		if(Time.timeScale < float.Epsilon)
			return;

		// Read the user input

		x = gpi.horizontalRJoystick.aValue;
		y = gpi.verticalRJoystick.aValue;


		if (m_AutoReturn)
		{
			// For tilt input, we need to behave differently depending on whether we're using mouse or touch input:
			// on mobile, vertical input is directly mapped to tilt itemValue, so it springs back autotempMatically when the look input is released
			// we have to test whether above or below zero because we want to auto-return to zero even if min and max are not symmetrical.
			m_TiltAngle = y > 0 ? Mathf.Lerp(0, -m_TiltMin, y) : Mathf.Lerp(0, m_TiltMax, -y);

			m_LookAngle = Target.rotation.eulerAngles.y;


		}
		else
		{
			// on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
			m_TiltAngle -= y*m_TurnSpeed;

			// and make sure the new itemValue is within the tilt range
			m_TiltAngle = Mathf.Clamp(m_TiltAngle, -m_TiltMin, m_TiltMax);

			// Adjust the look angle by an amount proportional to the turn speed and horizontal input.
			m_LookAngle += x*m_TurnSpeed;
		}

		// Rotate the rig (the root object) around Y axis only:
		m_TransformTargetRot = Quaternion.Euler(0f, m_LookAngle, 0f);

		// Tilt input around X is applied to the pivot (the child of this object)
		m_PivotTargetRot = Quaternion.Euler(m_TiltAngle, m_PivotEulers.y , m_PivotEulers.z);

		if (m_TurnSmoothing > 0)
		{
			m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRot, m_TurnSmoothing * Time.deltaTime);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, m_TransformTargetRot, m_TurnSmoothing * Time.deltaTime);
		}
		else
		{
			m_Pivot.localRotation = m_PivotTargetRot;
			transform.localRotation = m_TransformTargetRot;
		}
	}
}
