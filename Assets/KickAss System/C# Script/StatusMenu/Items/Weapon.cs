using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class Weapon : BaseItem, IComparable<Weapon> {

	public int dmg;

	public Weapon(string c_name, string c_description, bool c_stackable, int c_value, string c_modelPath, string c_gameObjPath, int c_dmg) : base(c_name, c_description, c_stackable, c_value, c_modelPath, c_gameObjPath)
	{
		dmg = c_dmg;
	}

	#region IComparable implementation

	public int CompareTo (Weapon other)
	{
		if(dmg == other.dmg)
			return String.Compare(name, other.name);
		
		return other.dmg - dmg;
	}

	#endregion
}
