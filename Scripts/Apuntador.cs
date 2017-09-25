using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Apuntador : MonoBehaviour {
    [SerializeField]
    private Transform Target;

    Vector3 PosG;
    Vector3 Pos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void LateUpdate()
    {
        Pos = Camera.main.WorldToScreenPoint(Target.Find("Cube").position);
        if (Pos.z > 0)
        {
            PosG = Camera.main.WorldToScreenPoint(Target.Find("Cube").position);
        }
        transform.position = PosG;
    }
}
