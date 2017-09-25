using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseStat{
	private string _nombre;			//Nombre del estado, atributo , etc.
	private string _descripcion;		//Descricion del estado,
	private int _valor;			//Valor inicial.
	private int _valorBase;			//Valor base de la propiedad
	private float _modValor;		//Modificador del valor inicial en porcentajes.


	//Constructores
	public BaseStat(){
		Nombre = string.Empty;
		Descripcion = string.Empty;
		Valor = 1;
		ValorBase = 1;
		ModValor = 0.1f;
	}

	public BaseStat(string newNombre, string newDescricion, int newValor, int newValorBase, float newModValor){
		Nombre = newNombre;
		Descripcion = newDescricion;
		Valor = newValor;
		ValorBase = newValorBase;
		ModValor = newModValor;
	}

	//Funcion que calcula la modificacion del valor y la retorna;
	public float CalcularModValor(){
		float newValor = (ValorBase * ModValor);
		return newValor;
	}


	//Area de Gets and Sets
	public string Nombre{
		get{ return _nombre; }
		set{ _nombre = value; }
	}

	public string Descripcion{
		get{ return _descripcion; }
		set{ _descripcion = value; }
	}

	public int Valor{
		get{ return _valor; }
		set{ _valor = value; }
	}

	public int ValorBase{
		get{ return _valorBase; }
		set{ _valorBase = value; }
	}

	public float ModValor{
		get{ return _modValor; }
		set{ _modValor = value; }
	}
}
