using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasePlayerSaveInfo{
	
	public int id;
	
	//Get and Set Stat
	public string nombre;
	
	//Atributos referentes a la experiencia
	public int nivel;
	
	public int curXP;
	
	public int reqXP;
	
	//Elemento Innato
	public InnateElement elementoInnato;
	
	//Clase base del personaje y con la que se lo modifica
	public PerteneciaElemental elementClass;
	
	//Atributos basicos
	public BaseStat salud;

	public BaseStat voluntad;

	public BaseStat fuerza;

	public BaseStat resistencia;

	public BaseStat energia;
	
	public BaseStat suerte;
	
	//Atributos de elementos
	public BaseStat fuego;
	
	public BaseStat viento;
	
	public BaseStat rayo;
	
	public BaseStat tierra;
	
	public BaseStat agua;
	
	public BasePlayerSaveInfo(){
		id = 0;
		nombre = string.Empty;
		nivel = 0;
		curXP = 0;
		reqXP = 0;
		elementoInnato = InnateElement.Neutro;
		elementClass = new PerteneciaElemental();
		salud = new BaseStat();
		voluntad = new BaseStat();
		fuerza = new BaseStat();
		resistencia = new BaseStat();
		energia = new BaseStat();
		suerte = new BaseStat();
		fuego = new BaseStat();
		viento = new BaseStat();
		rayo = new BaseStat();
		tierra = new BaseStat();
		agua = new BaseStat();
	}

	public BasePlayer TransformInfoToPlayer(){
		GameObject tempObj = new GameObject();
		tempObj.AddComponent(typeof(BasePlayer));
		BasePlayer tempP = tempObj.GetComponent<BasePlayer>();
		tempP.PlayerID = id;
		tempP.PlayerName = nombre;
		tempP.PlayerLevel = nivel;
		tempP.CurrentXP = curXP;
		tempP.RequiredXP = reqXP;
		tempP.Element = elementoInnato;
		tempP.ElementClass = elementClass;
		tempP.Salud = salud;
		tempP.Voluntad = voluntad;
		tempP.Fuerza = fuerza;
		tempP.Resistencia = resistencia;
		tempP.Energia = energia;
		tempP.Suerte = suerte;
		tempP.Fuego = fuego;
		tempP.Viento = viento;
		tempP.Rayo = rayo;
		tempP.Tierra = tierra;
		tempP.Agua = agua;
		GameObject.Destroy(tempObj);
		return tempP;

	}
}