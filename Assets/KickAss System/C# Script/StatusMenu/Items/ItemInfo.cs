using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class ItemInfo : MonoBehaviour {
	public enum ItemType {weapon, armor, potion, other};
	public ItemType itemType;
	//atributos de arma
	public int damage;
	//atributos de armadura
	public int defence;
	//atributos de posion
	public int healthValue;

	public BaseItem itemInfo;
	private GamePadInputs gpi;
	private Inventory inv;

	void Start(){
		switch(itemType){
		case ItemType.weapon:
			itemInfo = new Weapon(itemInfo.name, itemInfo.description, itemInfo.stackable, itemInfo.value, itemInfo.modelPath, itemInfo.gameObjPath, damage);
			break;
		case ItemType.armor:
			itemInfo = new Armor(itemInfo.name, itemInfo.description, itemInfo.stackable, itemInfo.value, itemInfo.modelPath, itemInfo.gameObjPath, defence);
			break;
		case ItemType.potion:
			itemInfo = new Potion(itemInfo.name, itemInfo.description, itemInfo.stackable, itemInfo.value, itemInfo.modelPath, itemInfo.gameObjPath, healthValue);
			break;
		default:
			break;
		}
	}

	void OnTriggerStay(Collider other){
		if(other.CompareTag("Player")){

			if(!inv){
				inv = (Inventory)FindObjectOfType(typeof(Inventory));
			}

			if(!gpi){
				gpi = (GamePadInputs)FindObjectOfType(typeof(GamePadInputs));
			}else{
				if(gpi.x.isUp){
					inv.AddToInventory(itemInfo);
					Destroy(this.gameObject);
				}
			}
		}
	}
}
