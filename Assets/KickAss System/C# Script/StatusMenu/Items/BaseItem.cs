using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class BaseItem : IComparable<BaseItem> {
	public int id;
	public string name, description;
	public bool stackable;
	public int value;
	public string modelPath, gameObjPath;

	public BaseItem(string c_name, string c_description, bool c_stackable, int c_value, string c_modelPath, string c_gameObjPath){
        name = c_name;
        description = c_description;
        stackable = c_stackable;
        value = c_value;
        modelPath = c_modelPath;
        gameObjPath = c_gameObjPath;
	}

	#region IComparable implementation

	public int CompareTo (BaseItem other)
	{
		return String.Compare(name, other.name);
	}

	#endregion
}
