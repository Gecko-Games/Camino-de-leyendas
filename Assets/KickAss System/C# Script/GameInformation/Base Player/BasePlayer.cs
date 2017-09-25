using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class BasePlayer : MonoBehaviour {

	string filePath;

	public BasePlayer(){
		PlayerID = 0;
		PlayerName = string.Empty;
		PlayerLevel = 1;
		CurrentXP = 0;
		RequiredXP = 1000;
		Element = InnateElement.Neutro;
		ElementClass = new PerteneciaElemental ();
		Salud = new BaseSalud();
		Energia = new BaseEnergia();
		Fuerza = new BaseFuerza ();
		Resistencia = new BaseResistencia ();
		Voluntad = new BaseVoluntad();
		Suerte = new BaseSuerte ();
		Fuego = new BaseStat ("Fuego", "", 1, 1, .1f);
		Viento = new BaseStat ("Viento", "", 1, 1, .1f);
		Rayo = new BaseStat ("Rayo", "", 1, 1, .1f);
		Tierra = new BaseStat ("Tierra", "", 1, 1, .1f);
		Agua = new BaseStat ("Agua", "", 1, 1, .1f);
	}

	public int PlayerID{ get; set; }

	//Get and Set Stat
	public string PlayerName{ get; set; }

	//Atributos referentes a la experiencia
	public int PlayerLevel{ get; set; }

	public int CurrentXP{ get; set; }
	
	public int RequiredXP{ get; set; }

	//Elemento Innato
	public InnateElement Element{ get; set; }

	//Clase base del personaje y con la que se lo modifica
	public PerteneciaElemental ElementClass{ get; set;}

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

	//Moneda del juego
	public int Credits{ get; set; }


	void Awake(){
		filePath = this.gameObject.name + this.tag;
	}

	void Start(){
		PlayerID = this.gameObject.GetInstanceID();

		//Load();
	}

	public void AddExp(int exp){
		CurrentXP += exp;
		if(CurrentXP >= RequiredXP){
			LevelUp();
		}
	}

	public void LevelUp(){
		
		DetermineRequiredXP();

		PlayerLevel++;

		Element = ElementClass.Element;
		Salud.Valor += (int)(ElementClass.Salud.CalcularModValor() * PlayerLevel);
		Voluntad.Valor += (int)(ElementClass.Voluntad.CalcularModValor() * PlayerLevel);
		Fuerza.Valor += (int)(ElementClass.Fuerza.CalcularModValor() * PlayerLevel);
		Resistencia.Valor += (int)(ElementClass.Resistencia.CalcularModValor() * PlayerLevel);
		Energia.Valor += (int)(ElementClass.Energia.CalcularModValor() * PlayerLevel);
		Suerte.Valor += (int)(ElementClass.Suerte.CalcularModValor() * PlayerLevel);
		Fuego.Valor += (int)(ElementClass.Fuego.CalcularModValor() * PlayerLevel);
		Viento.Valor += (int)(ElementClass.Viento.CalcularModValor() * PlayerLevel);
		Rayo.Valor += (int)(ElementClass.Rayo.CalcularModValor() * PlayerLevel);
		Tierra.Valor += (int)(ElementClass.Tierra.CalcularModValor() * PlayerLevel);
		Agua.Valor += (int)(ElementClass.Agua.CalcularModValor() * PlayerLevel);

		CurrentXP = 0;

		//Save();

		return;
	}

	private void DetermineRequiredXP(){
		float temp = (PlayerLevel * 1000) + 500;
		RequiredXP = (int)temp;
	}

	public BasePlayerSaveInfo PlayerSaveInfo(){
		BasePlayerSaveInfo pInfo = new BasePlayerSaveInfo ();
		pInfo.id = PlayerID;
		pInfo.nombre = PlayerName;
		pInfo.nivel = PlayerLevel;
		pInfo.curXP = CurrentXP;
		pInfo.reqXP = RequiredXP;
		pInfo.elementoInnato = Element;
		pInfo.elementClass = ElementClass;
		pInfo.salud = Salud;
		pInfo.voluntad = Voluntad;
		pInfo.fuerza = Fuerza;
		pInfo.resistencia = Resistencia;
		pInfo.energia = Energia;
		pInfo.suerte = Suerte;
		pInfo.fuego = Fuego;
		pInfo.viento = Viento;
		pInfo.rayo = Rayo;
		pInfo.tierra = Tierra;
		pInfo.agua = Agua;
		return pInfo;
	}

	public void PlayerLoadInfo(BasePlayer bp){
		PlayerID = bp.PlayerID;
		PlayerName = bp.PlayerName;
		PlayerLevel = bp.PlayerLevel;
		CurrentXP = bp.CurrentXP;
		RequiredXP = bp.RequiredXP;
		Element = bp.Element;
		ElementClass = bp.ElementClass;
		Salud = bp.Salud;
		Voluntad = bp.Voluntad;
		Fuerza = bp.Fuerza;
		Resistencia = bp.Resistencia;
		Energia = bp.Energia;
		Suerte = bp.Suerte;
		Fuego = bp.Fuego;
		Viento = bp.Viento;
		Rayo = bp.Rayo;
		Tierra = bp.Tierra;
		Agua = bp.Agua;
	}

	public void Save(){

		BasePlayerSaveInfo data = PlayerSaveInfo();
		SaveAndLoadSystem.Save<BasePlayerSaveInfo>(data, filePath);

	}

	public void Load(){

		if(SaveAndLoadSystem.FileExist(filePath)){

			BasePlayerSaveInfo data = SaveAndLoadSystem.Load<BasePlayerSaveInfo>(filePath);

			PlayerLoadInfo(data.TransformInfoToPlayer());

		}

	}
}
