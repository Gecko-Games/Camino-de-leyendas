using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemInfo))]
[CanEditMultipleObjects]
public class Custom_inspector_ItemInfo : Editor {
	ItemInfo itemInfo;
	SerializedProperty item_type, item_name, item_desc, item_stackable, item_value, item_modelPath, item_gameObjPath, weapon_damage, armor_defence, potion_health_value;

	void OnEnable(){
		itemInfo = (ItemInfo)target;
		item_type = serializedObject.FindProperty("itemType");
		item_name = serializedObject.FindProperty("itemInfo.name");
		item_desc = serializedObject.FindProperty("itemInfo.description");
		item_stackable = serializedObject.FindProperty("itemInfo.stackable");
		item_value = serializedObject.FindProperty("itemInfo.value");
		item_modelPath = serializedObject.FindProperty("itemInfo.modelPath");
		item_gameObjPath = serializedObject.FindProperty("itemInfo.gameObjPath");
		weapon_damage = serializedObject.FindProperty("damage");
		armor_defence = serializedObject.FindProperty("defence");
		potion_health_value = serializedObject.FindProperty("healthValue");
	}

	public override void OnInspectorGUI(){
		//base.OnInspectorGUI();
		serializedObject.Update();
		EditorGUILayout.PropertyField(item_type);
		EditorGUILayout.PropertyField(item_name);
		EditorGUILayout.PropertyField(item_desc);
		EditorGUILayout.PropertyField(item_stackable);
		EditorGUILayout.PropertyField(item_value);
		EditorGUILayout.PropertyField(item_modelPath);
		EditorGUILayout.PropertyField(item_gameObjPath);

		switch(itemInfo.itemType){
		case ItemInfo.ItemType.weapon:
			EditorGUILayout.PropertyField(weapon_damage);
			break;
		case ItemInfo.ItemType.armor:
			EditorGUILayout.PropertyField(armor_defence);
			break;
		case ItemInfo.ItemType.potion:
			EditorGUILayout.PropertyField(potion_health_value);
			break;
		default:
			break;
		}

		serializedObject.ApplyModifiedProperties();
	}
}
