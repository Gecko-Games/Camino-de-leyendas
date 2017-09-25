using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class ProgresBar : MonoBehaviour {

	public float currentValue = 0f, max = 100f;
	public bool visible = false, txtPorcent = false;
	public Text txtBar;
	private Image bar;
	private CanvasGroup cg;
	float tempFA = 0f;

	// Use this for initialization
	void Start () {
		bar = this.GetComponent<Image>();
		cg = this.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateProgresBar();
	}

	public void SetCurrentValue(int newValue){
		currentValue = newValue;
	}

	public void SetMax(int newValue){
		max = newValue;
	}

	public void UpdateProgresBar(){

		if(visible){
			tempFA = 1f;
			cg.alpha = tempFA;
		}else{
			if(cg.alpha > 0){
				tempFA -= Time.deltaTime;
				cg.alpha = tempFA;
			}
		}

		if(currentValue > max){
			currentValue = max;
		}else if(currentValue < 0f){
			currentValue = 0f;
		}

		if(txtBar){
			if(txtPorcent){
				txtBar.text = (int)(currentValue/max*100) + "%";
			}else{
				txtBar.text = (int)(currentValue) + "/" + max;
			}
		}

		bar.fillAmount = currentValue/max;
	}
}
