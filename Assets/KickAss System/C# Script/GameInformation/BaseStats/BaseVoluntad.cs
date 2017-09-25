using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseVoluntad : BaseStat {

	public BaseVoluntad(){
		Nombre = "Voluntad";
		Descripcion = "Un atributo que modifica la Energia.";
		Valor = 1;
		ValorBase = 1;
		ModValor = .1f;
	}

}
