using UnityEngine;
using System.Collections;

public class MenuCharacter : MonoBehaviour {
	public MenuCharacterIcon[] menuCharacterIcons;
	public enum MenuCharacterIconFilter{ main, status, abilities, inventory, settings };
	public MenuCharacterIconFilter iconFilter = MenuCharacterIconFilter.main;
	public CanvasGroup[] menues;
	public int iconIdActive = 0;

	private GamePadInputs gpi;
	private InventoryMenu im;
	private StatusCharacterMenu scm;
	public bool menuCharacterTrigger = false;
	private VRScript vrs;
	private KickAssCombatInputs kaci;
	private KickAssThirdPersonUserController katpc;
	private CanvasGroup tmpCanvas;
	private AutoParent ap;

	// Use this for initialization
	void Start () {

		tmpCanvas = this.GetComponent<CanvasGroup>();

		if(!gpi){
			gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
		}

		if(!vrs){
			vrs = (VRScript)FindObjectOfType(typeof(VRScript));
		}

		if(!im){
			im = (InventoryMenu)FindObjectOfType(typeof(InventoryMenu));
		}

		if(!scm){
			scm = (StatusCharacterMenu)FindObjectOfType(typeof(StatusCharacterMenu));
		}

		if(!kaci){
			kaci = (KickAssCombatInputs)FindObjectOfType(typeof(KickAssCombatInputs));
		}

		if(!katpc){
			katpc = (KickAssThirdPersonUserController)FindObjectOfType(typeof(KickAssThirdPersonUserController));
		}

		if(!ap){
			ap = this.GetComponent<AutoParent>();
		}
	}
	
	// Update is called once per frame
	void Update () {


		if(!vrs){
			vrs = (VRScript)FindObjectOfType(typeof(VRScript));
		}else{
			if(vrs.mode == VRScript.VRModes.None){
				this.GetComponent<Canvas>().worldCamera = vrs.MainC;
			}else{
				this.GetComponent<Canvas>().worldCamera = null;
			}
		}

		if(ap){
			if(!ap.target){
				ap.target = GameObject.FindGameObjectWithTag("MainCamera").transform;
			}
		}

		if(!gpi){
			gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
		}

		if(!kaci){
			kaci = (KickAssCombatInputs)FindObjectOfType(typeof(KickAssCombatInputs));
		}

		if(!katpc){
			katpc = (KickAssThirdPersonUserController)FindObjectOfType(typeof(KickAssThirdPersonUserController));
		}

		if(!im){
			im = (InventoryMenu)FindObjectOfType(typeof(InventoryMenu));
		}

		if(!scm){
			scm = (StatusCharacterMenu)FindObjectOfType(typeof(StatusCharacterMenu));
		}

		if(gpi.select.isUp){
			MenuCharacterOnOff();
		}

		if(menuCharacterTrigger){

			if(gpi.b.isUp){
				MenuOff();
			}

			if(iconFilter == MenuCharacterIconFilter.main){
				
				if(gpi.verticalDPadJoystick.isPositive){

					DisableIcons();
					iconIdActive--;
					if(iconIdActive <= 0){
						iconIdActive = 0;
					}
					ActiveIcon(iconIdActive);

				}else if(gpi.verticalDPadJoystick.isNegative){

					DisableIcons();
					iconIdActive++;
					if(iconIdActive >= menues.Length - 1){
						iconIdActive = menues.Length - 1;
					}

					ActiveIcon(iconIdActive);

				}

				if(gpi.a.isUp){
					switch(iconIdActive){
					case 0:
						iconFilter = MenuCharacterIconFilter.status;
						MenuOn(iconIdActive);
						break;
					case 1:
						iconFilter = MenuCharacterIconFilter.abilities;
						MenuOn(iconIdActive);
						break;
					case 2:
						iconFilter = MenuCharacterIconFilter.inventory;
						MenuOn(iconIdActive);
						break;
					case 3:
						iconFilter = MenuCharacterIconFilter.settings;
						MenuOn(iconIdActive);
						break;
					default:
						break;
					}
				}

			}
		}

	}

	public void MenuCharacterOnOff(){
		iconIdActive = 0;
		DisableIcons();

		if(menuCharacterTrigger){
			tmpCanvas.alpha = 0f;
			tmpCanvas.interactable = false;
			tmpCanvas.blocksRaycasts = false;
			TimeManagger.NormalizeTime();
			if(kaci)
				kaci.CanEnterInputs(true);

			if(katpc)
				katpc.CanEnterInputs(true);

			menuCharacterTrigger = false;
		}else{
			gpi.touchControlVisible = false;
			tmpCanvas.alpha = 1f;
			tmpCanvas.interactable = true;
			tmpCanvas.blocksRaycasts = true;
			TimeManagger.AlterateTime(.01f);
			if(kaci)
				kaci.CanEnterInputs(false);

			if(katpc)
				katpc.CanEnterInputs(false);

			menuCharacterTrigger = true;
			ActiveIcon(iconIdActive);
		}
	}

	public void CloseMenuTouchButton(){
		tmpCanvas.alpha = 0f;
		tmpCanvas.interactable = false;
		tmpCanvas.blocksRaycasts = false;
		TimeManagger.NormalizeTime();
		if(kaci)
			kaci.CanEnterInputs(true);

		if(katpc)
			katpc.CanEnterInputs(true);

		menuCharacterTrigger = false;
		gpi.touchControlVisible = true;
		DisableIcons();
	}

	public void DisableIcons(){

		foreach(MenuCharacterIcon ic in menuCharacterIcons){
			ic.Off();
		}

		MenuOff();

	}

	public void ActiveIcon(int iconId){
		iconIdActive = iconId;
		menuCharacterIcons[iconIdActive].On();
	}

	public void MenuOn(int menuId){

		switch(menuId){
		case 0:
			scm.StatusCharacterMenuOn();
			break;
		case 1:
			menues[menuId].alpha = 1f;
			menues[menuId].interactable = true;
			menues[menuId].blocksRaycasts = true;
			break;
		case 2:
			im.InventoryMenuOn();
			break;
		case 3:
			menues[menuId].alpha = 1f;
			menues[menuId].interactable = true;
			menues[menuId].blocksRaycasts = true;
			break;
		default:
			break;
		}
	}

	void MenuOff(){
		iconFilter = MenuCharacterIconFilter.main;
		im.InventoryMenuOff();
		scm.StatusCharacterMenuOff();
		foreach(CanvasGroup menu in menues){
			menu.alpha = 0f;
			menu.interactable = false;
			menu.blocksRaycasts = false;
		}
	}
}
