using UnityEngine;
using System.Collections;

[System.Serializable]
public class PerteneciaElemental{

	public InnateElement Element{ get; set;}
	
	//gets and sets
	public string PerteneciaElementalName{ get; set;}
	
	public string PerteneciaElementalDescrition{ get; set;}

	//Atributos fisicos
	public BaseStat Salud{ get; set;}

	public BaseStat Voluntad{ get; set;}

	public BaseStat Fuerza{ get; set;}
	
	public BaseStat Resistencia{ get; set;}

	public BaseStat Energia{ get; set;}
	
	public BaseStat Suerte{ get; set;}

	//Atributos elementales
	public BaseStat Fuego{ get; set;}
	
	public BaseStat Viento{ get; set;}
	
	public BaseStat Rayo{ get; set;}
	
	public BaseStat Tierra{ get; set;}
	
	public BaseStat Agua{ get; set;}

	public PerteneciaElemental(){
		Element = InnateElement.Neutro;
		Salud = new BaseSalud();
		Voluntad = new BaseVoluntad();
		Fuerza = new BaseFuerza ();
		Resistencia = new BaseResistencia ();
		Energia = new BaseEnergia();
		Suerte = new BaseSuerte ();
		Fuego = new BaseStat ("Fuego", "", 1, 1, .1f);
		Viento = new BaseStat ("Viento", "", 1, 1, .1f);
		Rayo = new BaseStat ("Rayo", "", 1, 1, .1f);
		Tierra = new BaseStat ("Tierra", "", 1, 1, .1f);
		Agua = new BaseStat ("Agua", "", 1, 1, .1f);
	}
}
