using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryMenu : MonoBehaviour {
	[System.Serializable]
	public class StatsField{

		public Text itName, itValue, itType, itProp;
		public void ClearProp(){
			itName.text = "";
			itValue.text = "";
			itType.text = "";
			itProp.text = "";
		}

		public void ShowSpecyficProp(BaseItem it){

			itName.text = "___" + it.name + "___";
			itValue.text = "<b>$</b> " + it.value;
			if(it is Weapon){
				Weapon w = (Weapon)it;
				itType.text = "<b><i>Arma</i></b>";
				itProp.text = "<b>Descripcion:</b>\n" + w.description + "\n<b>Daño</b>: " + w.dmg;
			}else if(it is Armor){
				Armor a = (Armor)it;
				itType.text = "<b><i>Armadura</i></b>";
				itProp.text = "<b>Descripcion:</b>\n" + a.description + "\n<b>Defensa</b>: " + a.defence;
			}else if(it is Potion){
				Potion p = (Potion)it;
				itType.text = "<b><i>Pocion</i></b>";
				itProp.text = "<b>Descripcion:</b>\n" + p.description + "\n<b>Cantidad de cura</b>: " + p.healthValue;
			}else{
				itType.text = "<b><i>Otro</i></b>";
				itProp.text = "<b>Descripcion:</b>\n" + it.description;
			}

		}

	}
	public StatsField statsField;
	public int activeFrame;
	public bool inventoryMenu = false;
	public Inventory inventory;
	public GameObject itemPrevCamera, itemFrame, prevItem;
	public ItemPreviewContainer prevItemTransform;
	public Color iconActive, iconNotActive;
	public enum IconFilter{ all, weapon, armor, potion, other };
	public IconFilter iconFilter;
	public InventoryIcon[] icons;
	public List<ItemFrame> itemFrames;
	public float yOffset = .001f;
	public GameObject frameList, equipWeaponOp, equipArmorOp;
	private GameObject tmp, tmpPrevCamItem;
	private ItemFrame tmpIf;
	private RectTransform itemFrameRT, tmpRT, rectTrans;
	private CanvasGroup cg;
	private int rectSize;
	public int iconIdActive;
	private GamePadInputs gpi;
	private bool weaponEquipMode = false, armorEquipMode = false;
	public Button[] weaponEquipButtons, armorEquipButtons;
    private int weaponEquipButtonsId = 0, armorEquipButtonsId = 0;

//	void Start(){
//		itemFrameRT = itemFrame.GetComponent<RectTransform>();
//		rectTrans = frameList.GetComponent<RectTransform>();
//	}

	void Start(){
		statsField.ClearProp();
		cg = this.GetComponent<CanvasGroup>();

		itemFrameRT = itemFrame.GetComponent<RectTransform>();
		rectTrans = frameList.GetComponent<RectTransform>();

		DisableIcons();
		iconIdActive = 0;
		ActiveIcon(iconIdActive);
		UpdateInventory();

		if(!prevItemTransform){
			tmpPrevCamItem = Instantiate(itemPrevCamera, itemPrevCamera.transform.position, itemPrevCamera.transform.rotation) as GameObject;
			prevItemTransform = (ItemPreviewContainer)FindObjectOfType(typeof(ItemPreviewContainer));
			tmpPrevCamItem.SetActive(false);
		}


	}

	void Update(){

		if(!gpi){
			gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
		}

		if(inventoryMenu){
			if(!weaponEquipMode && !armorEquipMode){
				if(gpi.horizontalDPadJoystick.isPositive){
					
					DisableIcons();
					iconIdActive++;
					if(iconIdActive >= 4){
						iconIdActive = 4;
					}

					ActiveIcon(iconIdActive);

				}else if(gpi.horizontalDPadJoystick.isNegative){

					DisableIcons();
					iconIdActive--;
					if(iconIdActive <= 0){
						iconIdActive = 0;
					}
					ActiveIcon(iconIdActive);
				}

				if(itemFrames.Count > 0){
					if(gpi.verticalDPadJoystick.isPositive){

						DisableItemFrame();
						activeFrame--;
						if(activeFrame <= 0){
							activeFrame = 0;
						}
							
						ActiveItemFrame(activeFrame);

					}else if(gpi.verticalDPadJoystick.isNegative){

						DisableItemFrame();
						activeFrame++;
						if(activeFrame >= itemFrames.Count - 1){
							activeFrame = itemFrames.Count - 1;
						}
				
						ActiveItemFrame(activeFrame);

					}

					if(gpi.a.isUp){
						UseActiveItemFrame();
					}
				}
			}else if(weaponEquipMode){
				if(gpi.verticalDPadJoystick.isPositive){

					weaponEquipButtonsId--;
					if(weaponEquipButtonsId <= 0){
						weaponEquipButtonsId = 0;
					}

					weaponEquipButtons[weaponEquipButtonsId].Select();

				}else if(gpi.verticalDPadJoystick.isNegative){
					
					weaponEquipButtonsId++;
					if(weaponEquipButtonsId >= 3){
						weaponEquipButtonsId = 3;
					}

					weaponEquipButtons[weaponEquipButtonsId].Select();

				}

				if(gpi.a.isUp){
					EquipWeapon();
				}

			}else if(armorEquipMode){
				if(gpi.verticalDPadJoystick.isPositive){

					armorEquipButtonsId--;
					if(armorEquipButtonsId <= 0){
						armorEquipButtonsId = 0;
					}

					armorEquipButtons[armorEquipButtonsId].Select();

				}else if(gpi.verticalDPadJoystick.isNegative){

					armorEquipButtonsId++;
					if(armorEquipButtonsId >= 3){
						armorEquipButtonsId = 3;
					}

					armorEquipButtons[armorEquipButtonsId].Select();

				}

				if(gpi.a.isUp){
					EquipArmor();
				}
			}
		}

		if(!inventory){
			inventory = (Inventory)FindObjectOfType(typeof(Inventory));
		}

		if(!prevItemTransform){
			tmpPrevCamItem = Instantiate(itemPrevCamera, itemPrevCamera.transform.position, itemPrevCamera.transform.rotation) as GameObject;
			prevItemTransform = (ItemPreviewContainer)FindObjectOfType(typeof(ItemPreviewContainer));
			tmpPrevCamItem.SetActive(false);
		}
	}

	public void InventoryMenuOn(){
		
		UpdateInventory();
		tmpPrevCamItem.SetActive(true);
		cg.alpha = 1f;
		cg.interactable = true;
		cg.blocksRaycasts = true;
		inventoryMenu = true;
	
	}

	public void InventoryMenuOff(){

		UpdateInventory();
		tmpPrevCamItem.SetActive(false);
		WeaponEquipModeOff();
		ArmorEquipModeOff();
		cg.alpha = 0f;
		cg.interactable = false;
		cg.blocksRaycasts = false;
		inventoryMenu = false;

	}

	void UpdateInventory(){
		inventory.SortInventory();
		rectTrans.anchoredPosition = Vector2.zero;
		activeFrame = 0;

		while(itemFrames.Count != 0){
			GameObject go = itemFrames[0].gameObject;
			itemFrames.RemoveAt(0);
			GameObject.Destroy(go);
		}

		rectSize = 0;
		itemFrame.SetActive(true);

		if(!inventory){
			inventory = (Inventory)FindObjectOfType(typeof(Inventory));
		}else{

			if(iconFilter == IconFilter.all || iconFilter == IconFilter.weapon)
				for(int i = 0; i < inventory.inv_Weapon.Count; i++)
					DrawItem(inventory.inv_Weapon[i]);

			if(iconFilter == IconFilter.all || iconFilter == IconFilter.armor)
				for(int i = 0; i < inventory.inv_Armor.Count; i++)
					DrawItem(inventory.inv_Armor[i]);

			if(iconFilter == IconFilter.all || iconFilter == IconFilter.potion)
				for(int i = 0; i < inventory.inv_Potion.Count; i++)
					DrawItem(inventory.inv_Potion[i]);

			if(iconFilter == IconFilter.all || iconFilter == IconFilter.other)
				for(int i = 0; i < inventory.inv_Other.Count; i++)
					DrawItem(inventory.inv_Other[i]);


			itemFrame.SetActive(false);
			rectTrans.sizeDelta = new Vector2(0f, Mathf.Max((itemFrameRT.rect.height + 1.5f) * rectSize, 670));
			rectTrans.anchoredPosition += new Vector2(0f, -Mathf.Max((itemFrameRT.rect.height + 1.5f) * rectSize/2, 670/2));
		}
	}

	void DrawItem(BaseItem it){
		tmp = GameObject.Instantiate(itemFrame);
		tmp.transform.SetParent(frameList.transform);
		tmpRT = tmp.GetComponent<RectTransform>();
		tmpRT.localScale = itemFrameRT.localScale;

		tmpRT.localPosition = itemFrameRT.localPosition;
		tmpRT.localPosition += new Vector3(0f, (itemFrameRT.rect.height + yOffset) * -rectSize, 0f);

		tmpRT.localRotation = itemFrameRT.localRotation;

		tmpIf = tmp.GetComponent<ItemFrame>();
		tmpIf.index = itemFrames.Count;
		itemFrames.Add(tmpIf);
		tmpIf.SetValues(it);
		rectSize++;
	}

	public void DisableIcons(){

		foreach(InventoryIcon ic in icons){
			ic.ChangeColor(iconNotActive);
		}

	}

	public void ActiveIcon(int iconId){
		iconIdActive = iconId;
		icons[iconIdActive].ChangeColor(iconActive);

		switch(iconIdActive){
		case 0:
			iconFilter = IconFilter.all;
			break;
		case 1:
			iconFilter = IconFilter.weapon;
			break;
		case 2:
			iconFilter = IconFilter.armor;
			break;
		case 3:
			iconFilter = IconFilter.potion;
			break;
		case 4:
			iconFilter = IconFilter.other;
			break;
		default:
			break;
		}
		UpdateInventory();
	}

//	public void CloseMenuTouchButton(){
//		gpi.touchControlVisible = true;
//		inventoryMenu = false;
//		TimeManagger.NormalizeTime();
//		inventory.GetComponent<KickAssCombatInputs>().CanEnterInputs(true);
//		UpdateInventory();
//		InventoryMenuOn();
//	}

	public void DisableItemFrame(){

		foreach(ItemFrame iframe in itemFrames){
			iframe.DeactiveFrame();
		}

	}

	public void ActiveItemFrame(int frameId){
		activeFrame = frameId;
		itemFrames[activeFrame].ActiveFrame();
	}

	public void UseActiveItemFrame(){
		itemFrames[activeFrame].UseOrEquip();
	}

	public void PreviewNewItem(string itemPath){
		if(prevItem){
			Destroy(prevItem);
		}

		prevItem = GameObject.Instantiate(Resources.Load(itemPath)) as GameObject;
		prevItem.GetComponent<Rigidbody>().useGravity = false;
		prevItem.transform.SetParent(prevItemTransform.transform);
		prevItem.transform.localPosition = Vector3.zero;
	}

	public void WeaponEquipModeOn(){
		weaponEquipMode = true;
		equipWeaponOp.SetActive(true);
	}

	public void WeaponEquipModeOff(){
		weaponEquipMode = false;
		equipWeaponOp.SetActive(false);
	}

	public void EquipWeapon(){

		//Falta mas codigo, hacer la funcion de equipamento en el WeaponManager

		WeaponEquipModeOff();
	}

	public void ArmorEquipModeOn(){
		armorEquipMode = true;
		equipArmorOp.SetActive(true);
	}

	public void ArmorEquipModeOff(){
		armorEquipMode = false;
		equipArmorOp.SetActive(false);
	}

	public void EquipArmor(){

		//Falta mas codigo, hacer la funcion de equipamento en el ArmorManager

		ArmorEquipModeOff();
	}

	public void RemoveItemToInventory(BaseItem item, int index){
		inventory.RemoveItem(item, index);
		GameObject go = itemFrames[activeFrame].gameObject;
		itemFrames.RemoveAt(activeFrame);
		GameObject.Destroy(go);
		statsField.ClearProp();
		if(prevItem){
			Destroy(prevItem);
		}
		UpdateInventory();
	}
}
