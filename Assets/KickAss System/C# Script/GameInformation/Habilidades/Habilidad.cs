using UnityEngine;
using System.Collections;

public enum TargetType{
	Enemy,
	Player
}

public class Habilidad : MonoBehaviour {

	public string description;

	public InnateElement element = InnateElement.Neutro;

	public TargetType targetType = TargetType.Enemy;

	public float duration = 1f, distance = 10f; 

	public int cost = 1, multiplierMod = 10; 

	public BasePlayer p;

	public Transform target;

	public string Descripcion{
		get{return description;}
		set{description = value;}
	}

	public InnateElement Elemento{
		get{return element;}
		set{element = value;}
	}

	public virtual void PerformAction(){}

	public virtual int ReturnDamage(){
		return multiplierMod;
	}

	public float MultiplierDamage(int originalD){
		float tempPorcentaje = originalD * multiplierMod;
		return tempPorcentaje;
	}
}
