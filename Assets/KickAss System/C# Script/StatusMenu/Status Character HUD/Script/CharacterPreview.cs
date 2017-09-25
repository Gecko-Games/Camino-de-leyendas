using UnityEngine;
using System.Collections;

public class CharacterPreview : MonoBehaviour {

	public float angle = 180f;
	public Vector3 posOffset = new Vector3(0f, 1f, 0f);
	public Transform target;
	private Transform t;

	// Use this for initialization
	void Start () {
		t = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!target){
			target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}else{
			t.position = (target.position + posOffset);
			t.Rotate(Vector3.up, angle);
		}
	}
}
