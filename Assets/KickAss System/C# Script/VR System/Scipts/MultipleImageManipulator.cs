using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("LobinuxSoft/Virtual Reality/Multiple image manipulator")]
public class MultipleImageManipulator : MonoBehaviour {

	[Range(0f,1f)]public float mFillAmount;
	public bool colorsFromPorctentaje = false;

	public Color minColor;
	[Range(0f,1f)]public float minValue = .25f;

	public Color midColor;
	[Range(0f,1f)]public float midValue = .25f;

	public Color fullColor;
	[Range(0f,1f)]public float fullValue = .25f;

	public Image[] images;
	

	// Update is called once per frame
	void Update () {

		foreach(Image tempImg in images){

			if(!colorsFromPorctentaje){
				tempImg.color = minColor;
				tempImg.fillAmount = mFillAmount;
			}else{
				if(mFillAmount <= minValue){

					tempImg.color = minColor;
					tempImg.fillAmount = mFillAmount;

				}else if(mFillAmount > minValue && mFillAmount <= midValue){

					tempImg.color = midColor;
					tempImg.fillAmount = mFillAmount;

				}else if(mFillAmount > midValue && mFillAmount <= fullValue){

					tempImg.color = fullColor;
					tempImg.fillAmount = mFillAmount;

				}
			}
		}
	}
}