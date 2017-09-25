using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseSuerte : BaseStat {

	public BaseSuerte(){
		Nombre = "Suerte";
		Descripcion = "Un atributo que modifica las probabilidades de un ataque critico.";
		Valor = 1;
		ValorBase = 1;
		ModValor = .1f;
	}

}
