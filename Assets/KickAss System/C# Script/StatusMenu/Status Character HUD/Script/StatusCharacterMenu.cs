using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusCharacterMenu : MonoBehaviour {

	public CharacterPreview characterPreviewCam;
	public Text pName, pStatus, pWeapons, pArmor;

	private BasePlayer player;
	private VitalsManager vm;
	private WeapomManager wm;
	private CanvasGroup cg;
	private CharacterPreview cp;

	// Use this for initialization
	void Start () {
		if(!player){
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasePlayer>();
		}

		if(!vm){
			vm = GameObject.FindGameObjectWithTag("Player").GetComponent<VitalsManager>();
		}

		if(!wm){
			wm = (WeapomManager)FindObjectOfType(typeof(WeapomManager));
		}

		if(!cg){
			cg = this.GetComponent<CanvasGroup>();
		}

		if(!cp){
			cp = Instantiate(characterPreviewCam, Vector3.zero, Quaternion.identity) as CharacterPreview;
			cp.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!player){
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasePlayer>();
		}

		if(!vm){
			vm = GameObject.FindGameObjectWithTag("Player").GetComponent<VitalsManager>();
		}

		if(!wm){
			wm = (WeapomManager)FindObjectOfType(typeof(WeapomManager));
		}

		if(!cg){
			cg = this.GetComponent<CanvasGroup>();
		}

		if(!cp){
			cp = Instantiate(characterPreviewCam, Vector3.zero, Quaternion.identity) as CharacterPreview;
			cp.gameObject.SetActive(false);
		}

	}

	void UpdateCharacterStats(){
		pName.text = player.PlayerName;

		pStatus.text = "Nivel: " + player.PlayerLevel + "\nExp: " + player.CurrentXP + "/" + player.RequiredXP + "\n" + "\nElemento: " + player.Element + "\n"
			+ "\nSalud: " + vm.curHealth + "/" + vm.maxHealth + "\nEnergia: " + vm.curEnergy + "/" + vm.maxEnergy + "\n" + "\nFuerza: " + player.Fuerza.Valor
			+ "\nResistencia: " + player.Resistencia.Valor + "\nVoluntad: " + player.Voluntad.Valor + "\nSuerte: " + player.Suerte.Valor + "\n"
			+ "\nFuego: " + player.Fuego.Valor + "\nViento: " + player.Viento.Valor + "\nRayo: " + player.Rayo.Valor + "\nTierra: " + player.Tierra.Valor
			+ "\nAgua: " + player.Agua.Valor;

		string tUpWeapon, tRightWeapon, tDownWeapon, tLeftWeapon;

		if(wm.upWeapon){
			tUpWeapon = wm.upWeapon.nombre;
		}else{
			tUpWeapon = "Nada Equipado";
		}

		if(wm.rightWeapon){
			tRightWeapon = wm.rightWeapon.nombre;
		}else{
			tRightWeapon = "Nada Equipado";
		}

		if(wm.downWeapon){
			tDownWeapon = wm.downWeapon.nombre;
		}else{
			tDownWeapon = "Nada Equipado";
		}

		if(wm.leftWeapon){
			tLeftWeapon = wm.leftWeapon.nombre;
		}else{
			tLeftWeapon = "Nada Equipado";
		}

		pWeapons.text = "Arma 1°: " + tUpWeapon + "\n" + "\nArma 2°:" + tRightWeapon + "\n" + "\nArma 3°:" + tDownWeapon + "\n" + "\nArma 4°:" + tLeftWeapon;
	}

	public void StatusCharacterMenuOn(){

		UpdateCharacterStats();
		if(cp)
			cp.gameObject.SetActive(true);

		cg.alpha = 1f;
		cg.interactable = true;
		cg.blocksRaycasts = true;

	}

	public void StatusCharacterMenuOff(){

		UpdateCharacterStats();
		if(cp)
			cp.gameObject.SetActive(false);

		cg.alpha = 0f;
		cg.interactable = false;
		cg.blocksRaycasts = false;

	}


}
