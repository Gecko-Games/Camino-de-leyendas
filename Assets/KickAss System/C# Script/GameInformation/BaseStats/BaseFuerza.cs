using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseFuerza : BaseStat {

	public BaseFuerza(){
		Nombre = "Fuerza";
		Descripcion = "Un atributo que modifica directamente la fuerza de ataque.";
		Valor = 1;
		ValorBase = 1;
		ModValor = .1f;
	}

}
