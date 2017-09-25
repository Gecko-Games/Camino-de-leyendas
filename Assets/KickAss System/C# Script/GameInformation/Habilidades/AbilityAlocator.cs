using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityAlocator : MonoBehaviour {

	public List<Habilidad> listaHabilidades = new List<Habilidad>();
	public AbilityCaster aCaster;
	
	public void AbilitySel(string habilidadNombre){
		aCaster = GameObject.Find("PlayerSphere").GetComponent<AbilityCaster>();
		for(int i = 0; i < listaHabilidades.Count; i++){
			if(listaHabilidades[i].name == habilidadNombre){
				aCaster.AddAbilityToCast (listaHabilidades[i].gameObject);
			}
		}
	}
}
