using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseEnergia : BaseStat {

	public BaseEnergia(){
		Nombre = "Energia";
		Descripcion = "Esta energia sirve para realizar toda clase de ataques especiales y super movimientos...si te quedas sin ella tendras problemas para atacar.";
		Valor = 15;
		ValorBase = 15;
		ModValor = .15f;
	}
}
