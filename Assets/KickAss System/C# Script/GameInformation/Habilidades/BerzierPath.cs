using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BerzierPath : MonoBehaviour {

	public GameObject start, middle, end;
	public int numberOfPoints = 20;
	public Vector3[] spacePoints;

//	void Start(){
//		spacePoints = new Vector3[numberOfPoints];
//	}

	void Update() 
	{
		// check parameters and components
		if (spacePoints == null || start == null || middle == null || end == null)
		{
			return; // no points specified
		} 

		// update line renderer

		if (numberOfPoints > 0)
		{
			spacePoints = new Vector3[numberOfPoints];
		}

		middle.transform.rotation = start.transform.rotation;
		end.transform.rotation = start.transform.rotation;

		// set points of quadratic Bezier curve
		Vector3 p0 = start.transform.position;
		Vector3 p1 = middle.transform.position;
		Vector3 p2 = end.transform.position;
		float t = 0f; 
		Vector3 position;
		for(int i = 0; i < numberOfPoints; i++) 
		{
			t = i / (numberOfPoints - 1f);
			position = (1f - t) * (1f - t) * p0 
				+ 2f * (1f - t) * t * p1
				+ t * t * p2;
			spacePoints[i] = position;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(start.transform.position, .5f);
		Gizmos.DrawSphere(middle.transform.position, .5f);
		Gizmos.DrawSphere(end.transform.position, .5f);

		Gizmos.color = Color.red;
		for(int i = 0; i < numberOfPoints; i++) 
		{
			Gizmos.DrawSphere(spacePoints[i], .2f);
		}
	}
}
