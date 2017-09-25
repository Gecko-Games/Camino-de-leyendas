using System;
using UnityEngine;

[RequireComponent(typeof (KickAssCharacterMotor))]
[RequireComponent(typeof (KickAssCombatSystem))]
public class KickAssThirdPersonUserController : MonoBehaviour
{
	private KickAssCharacterMotor m_Character; // A reference to the ThirdPersonCharacter on the object
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private bool m_Crouch, m_Sprint;
	private KickAssCombatSystem kacs;
	private VitalsManager vm;
	private bool canEnterInputs = true;
    GamePadInputs gpi;


	private void Start()
	{
		kacs = (KickAssCombatSystem)FindObjectOfType(typeof(KickAssCombatSystem));
        gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));

        vm = this.GetComponent<VitalsManager>();
		// get the transform of the main camera


		if (m_Cam == null)
		{
			m_Cam = Camera.main.transform;
		}
		
		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<KickAssCharacterMotor>();
	}
	
	
	private void Update()
	{
		if(!vm.dead){

			if(canEnterInputs){
				if (!m_Jump)
				{
					m_Jump = gpi.a.isDown;
				}
			}
		}
	}
	
	
	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		if(!vm.dead){

			if(canEnterInputs){

				// read inputs
				float h = gpi.horizontalLJoystick.aValue;
				float v = gpi.verticalLJoystick.aValue;

				if(gpi.l3.isDown){
					if(m_Crouch == false){
						m_Crouch = true;
					}else{
						m_Crouch = false;
					}
				}

                m_Sprint = gpi.r1.isActive;

				// calculate move direction to pass to character
				if (m_Cam != null)
				{
					// calculate camera relative direction to move:
					m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1,0,1)).normalized;
					m_Move = v*m_CamForward + h*m_Cam.right;
				}
				else
				{
					// we use world-relative directions in the case of no main camera
					m_Move = v*Vector3.forward + h*Vector3.right;
				}

				if(kacs.canMove){
					m_Character.Move(m_Move, m_Crouch, m_Jump, m_Sprint);
					m_Jump = false;
				}
			}
		}
	}

	public void CanEnterInputs(bool can){
		canEnterInputs = can;
	}
}
