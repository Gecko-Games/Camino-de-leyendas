using UnityEngine;
using UnityEngine.UI;

public class MenuCharacterIcon : MonoBehaviour {
	public Color colorOn = Color.white, colorOff = Color.gray;
	Image image;
	void Awake(){
		image = gameObject.GetComponent<Image>();
	}

	void ChangeColor(Color col){
		image.color = col;
	}

	public void On(){
		ChangeColor(colorOn);
	}

	public void Off(){
		ChangeColor(colorOff);
	}
}
