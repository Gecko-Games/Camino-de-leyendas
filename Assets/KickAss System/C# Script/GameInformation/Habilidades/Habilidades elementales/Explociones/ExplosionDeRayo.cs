using UnityEngine;
using System.Collections;

public class ExplosionDeRayo : AreaDeEfecto {

	public ExplosionDeRayo(){
		Descripcion = "Una explosion de energia electrica que genera poco daño pero que atrae como un iman sin dejar salir a la victima";
		Elemento = InnateElement.Rayo;
	}
}
