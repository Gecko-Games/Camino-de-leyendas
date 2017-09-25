using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class SceneController : MonoBehaviour {

	public float speedToBlend = .5f;
	public BasePlayer playerObj;
	public Transform cameraObj;
	public AutoParent hudObj;

	public string raspawnPointName, hpName = "/Circular Bars/HP", mpName = "/Circular Bars/MP", expName = "/Circular Bars/EXP", textLevelName = "/Circular Bars/EXP/TextLevel";

	private CanvasGroup cg;
	public BasePlayer bp;
	public VitalsManager vm;
	public AutoParent ap;
	public Transform camTransform;
	private bool isLoaded = false;
	private Transform raspawnPoint;

	// Use this for initialization
	void Start () {

		cg = this.GetComponent<CanvasGroup>();
		isLoaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().isLoaded){
			
			if(!raspawnPoint){
				raspawnPoint = GameObject.Find(raspawnPointName).GetComponent<Transform>();
			}

			if(bp){
				if(cg.alpha > 0){
					cg.alpha -= Time.deltaTime * speedToBlend;
				}
			}else{
				if(!isLoaded){
					
					bp = (BasePlayer)Instantiate(playerObj, raspawnPoint.position, raspawnPoint.rotation);
					bp.name = playerObj.name;
					bp.Load();
					camTransform = (Transform)Instantiate(cameraObj, raspawnPoint.position, Quaternion.identity);

					ap = (AutoParent)Instantiate(hudObj, raspawnPoint.position, camTransform.rotation);
					ap.target = camTransform;
			
					vm = bp.gameObject.GetComponent<VitalsManager>();
					vm.hpBar = GameObject.Find("/" + ap.name + hpName).GetComponent<ProgresBar>();
					vm.euBar = GameObject.Find("/" + ap.name + mpName).GetComponent<ProgresBar>();
					vm.expBar = GameObject.Find("/" + ap.name + expName).GetComponent<ProgresBar>();
					vm.levelText = GameObject.Find("/" + ap.name + textLevelName).GetComponent<UnityEngine.UI.Text>();
					isLoaded = true;
				}
			}
		}
	}
}
