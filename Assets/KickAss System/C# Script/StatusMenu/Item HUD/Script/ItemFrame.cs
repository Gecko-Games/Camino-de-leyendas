using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemFrame : MonoBehaviour {

	public int index;
	public BaseItem item;
	public string name;
	public int value;
	public Text nameTxt, valueTxt;
	public Image img;
	InventoryMenu im;

	void Awake(){
		im = (InventoryMenu)FindObjectOfType(typeof(InventoryMenu));
		img = this.GetComponent<Image>();
	}

	public void SetValues(BaseItem it){
		item = it;
		nameTxt.text = item.name;
		valueTxt.text = item.value.ToString();
	}

	public void ActiveFrame(){
		im.activeFrame = index;
		im.PreviewNewItem(item.modelPath);
		im.statsField.ShowSpecyficProp(item);
		img.color = Color.black;
	}

	public void DeactiveFrame(){
		img.color = Color.clear;
	}

	public void UseOrEquip(){
		if(item is Weapon){
			im.WeaponEquipModeOn();
		}else if(item is Armor){
			im.ArmorEquipModeOn();
		}else if(item is Potion){
			im.RemoveItemToInventory(item, item.id);
		}

	}
}
