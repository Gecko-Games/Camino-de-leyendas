using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class Armor : BaseItem, IComparable<Armor> {

	public int defence;

	public Armor(string c_name, string c_description, bool c_stackable, int c_value, string c_modelPath, string c_gameObjPath, int c_Defence) : base(c_name, c_description, c_stackable, c_value, c_modelPath, c_gameObjPath)
	{
		defence = c_Defence;
	}

	#region IComparable implementation
	public int CompareTo (Armor other)
	{
		if(defence == other.defence)
			return String.Compare(name, other.name);

		return other.defence - defence;
	}
	#endregion
}
