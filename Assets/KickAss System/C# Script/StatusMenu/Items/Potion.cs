using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class Potion : BaseItem, IComparable<Potion> {

	public int healthValue;

	public Potion(string c_name, string c_description, bool c_stackable, int c_value, string c_modelPath, string c_gameObjPath, int c_healthValue) : base(c_name, c_description, c_stackable, c_value, c_modelPath, c_gameObjPath)
	{
		healthValue = c_healthValue;
	}

	#region IComparable implementation

	public int CompareTo (Potion other)
	{
		if(healthValue == other.healthValue)
			return String.Compare(name, other.name);

		return other.healthValue - healthValue;
	}

	#endregion
}
