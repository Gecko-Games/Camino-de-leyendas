using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NewBasePlayer : MonoBehaviour {


	private string playerNombre;
	public int playerLevel = 1;
	private int currentXP;
	private int requiredXP;

	public List<BaseStat> playerAtributos = new List<BaseStat> ();		//atributos basicos como fuerza, resistencia, etc.
	public List<BaseStat> playerVitalidades = new List<BaseStat> ();	//vitalidad salud energia etc.
	public List<BaseStat> playerEstadisticas = new List<BaseStat> ();	//estadisticas como ataque, defensa, critico, etc.
	public List<BaseStat> playerElementos = new List<BaseStat> ();		//elementos neutro, fuego, viento, rayo, tierra, agua.
	private InnateElement elementoInnato = InnateElement.Neutro;		//elemento al que pertenece.

	public string PlayerNombre {
		get{ return playerNombre; }
		set{ playerNombre = value; }
	}

	public int PlayerLevel{
		get{ return playerLevel; }
	}

	public int CurrentXP{
		get{ return currentXP; }
	}
	
	public int RequiredXP{
		get{ return requiredXP; }
	}

	public InnateElement ElementoInnato{
		get{ return elementoInnato; }
		set{ elementoInnato = value; }
	}
	
	void Start(){

		//atributos
		playerAtributos.Add (new BaseFuerza());
		playerAtributos.Add (new BaseResistencia());
		playerAtributos.Add (new BaseSuerte());
		playerAtributos.Add (new BaseVoluntad());

		//vitalidades
		playerVitalidades.Add (new BaseSalud());
		playerVitalidades.Add (new BaseEnergia());

		//estadisticas

		//elementos

		//primer actualizacion
		VitalsUpdate ();
	}

	void Update(){
		VitalsUpdate ();
	}

	public void VitalsUpdate(){
		playerVitalidades[0].Valor =  ((int)(playerVitalidades[0].Valor + playerVitalidades[0].CalcularModValor()) + (int)(playerAtributos [1].CalcularModValor())) * playerLevel;
		Debug.Log(playerVitalidades[0].Valor);
		playerVitalidades[1].Valor = ((int)(playerVitalidades[1].CalcularModValor()) + (int)(playerAtributos [3].CalcularModValor())) * playerLevel;
	}

	public void StatisticUpdate(){}

	public void ElementsUpdate(){}

	public void SetUpClass(List<BaseStat> playerClassAtributos, List<BaseStat> playerClassVitalidades, List<BaseStat> playerClassEstadisticas, List<BaseStat> playerClassElementos){
		playerAtributos = playerClassAtributos;
		playerVitalidades = playerClassVitalidades;
		playerEstadisticas = playerClassEstadisticas;
		playerElementos = playerClassElementos;
	}

	public void AddExp(int exp){
		currentXP += exp;

		if(currentXP >= requiredXP){
			LevelUp();
		}

	}

	public void LevelUp(){
		playerLevel++;
		DetermineRequiredXP();
		VitalsUpdate();
	}

	private void DetermineRequiredXP(){
		float temp = (playerLevel * 500) + 250;
		requiredXP = (int)temp;
	}

}
