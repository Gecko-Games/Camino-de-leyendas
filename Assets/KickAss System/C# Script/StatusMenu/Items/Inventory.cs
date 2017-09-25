using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<Weapon> inv_Weapon;
	public List<Armor> inv_Armor;
	public List<Potion> inv_Potion;
	public List<BaseItem> inv_Other;

	public void Save(){
		InventoryData inventoryData = new InventoryData();
		inventoryData.weapon_data = inv_Weapon;
		inventoryData.armor_data = inv_Armor;
		inventoryData.potion_data = inv_Potion;
		inventoryData.other_data = inv_Other;
		SaveAndLoadSystem.Save<InventoryData>(inventoryData, "InventoryData");
	}

	public void Load(){

		if(SaveAndLoadSystem.FileExist("InventoryData")){
			InventoryData id = SaveAndLoadSystem.Load<InventoryData>("InventoryData");
			inv_Weapon = id.weapon_data;
			inv_Armor = id.armor_data;
			inv_Potion = id.potion_data;
			inv_Other = id.other_data;
		}
	}

	public void AddToInventory(BaseItem item){
		if(item is Weapon){
			item.id = inv_Weapon.Count;
			inv_Weapon.Add((Weapon)item);
		}else if(item is Armor){
			item.id = inv_Armor.Count;
			inv_Armor.Add((Armor)item);
		}else if(item is Potion){
			item.id = inv_Potion.Count;
			inv_Potion.Add((Potion)item);
		}else{
			item.id = inv_Other.Count;
			inv_Other.Add(item);
		}
	}

	public void SortInventory(){
		if(inv_Weapon.Count > 0){
			inv_Weapon.Sort();
			for(int i = 0; i < inv_Weapon.Count; i++){
				inv_Weapon[i].id = i;
			}
		}
		if(inv_Armor.Count > 0){
			inv_Armor.Sort();
			for(int i = 0; i < inv_Armor.Count; i++){
				inv_Armor[i].id = i;
			}
		}
		if(inv_Potion.Count > 0){
			inv_Potion.Sort();
			for(int i = 0; i < inv_Potion.Count; i++){
				inv_Potion[i].id = i;
			}
		}
		if(inv_Other.Count > 0){
			inv_Other.Sort();
			for(int i = 0; i < inv_Other.Count; i++){
				inv_Other[i].id = i;
			}
		}
	}

	public void RemoveItem(BaseItem item, int indexPos){
		if(item is Weapon){
			inv_Weapon.RemoveAt(indexPos);
		}else if(item is Armor){
			inv_Armor.RemoveAt(indexPos);
		}else if(item is Potion){
			inv_Potion.RemoveAt(indexPos);
		}else{
			inv_Other.RemoveAt(indexPos);
		}

		SortInventory();
	}

}

[System.Serializable]
public class InventoryData {

	public List<Weapon> weapon_data;
	public List<Armor> armor_data;
	public List<Potion> potion_data;
	public List<BaseItem> other_data;
}
