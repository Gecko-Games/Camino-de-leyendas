using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseSalud : BaseStat {

	public BaseSalud(){
		Nombre = "Salud";
		Descripcion = "La salud del personaje, una vez llegue a 0 el personaje muere.";
		Valor = 25;
		ValorBase = 25;
		ModValor = .25f;
	}
}
