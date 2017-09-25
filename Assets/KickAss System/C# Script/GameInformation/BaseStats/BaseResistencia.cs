using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseResistencia : BaseStat {

	public BaseResistencia(){
		Nombre = "Resistencia";
		Descripcion = "Un atributo que modifica directamente la defensa contra los ataques.";
		Valor = 1;
		ValorBase = 1;
		ModValor = .1f;
	}
}
