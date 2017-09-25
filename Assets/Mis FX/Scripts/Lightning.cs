using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public bool procedural = false;

	public float maxZ = 8f, posRange = .15f, radius = 1f, speedLightningRaise = 1.5f, colorTimeModifier = 10f;

	public int noSegment = 12;

	public Color color = Color.white;

	private float tempZ = 0f;

	private Vector2 midPoint;

	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {

		lineRenderer = this.GetComponent<LineRenderer>();

		lineRenderer.SetColors (color, color);

		lineRenderer.SetVertexCount (noSegment);

		midPoint = new Vector2 (Random.Range(-radius, radius), Random.Range(-radius, radius));

		if(tempZ < maxZ){
			tempZ += (Time.deltaTime * 10f) * speedLightningRaise;
		}else if(tempZ > maxZ){
			tempZ -= (Time.deltaTime * 10f) * speedLightningRaise;
		}

		for (int i = 0; i < noSegment-1; i++) {

			float z = ((float)i * tempZ / (float)(noSegment-1));

			float x = -midPoint.x*z*z/(float)(tempZ*2f) + z*midPoint.x/2f;

			float y = -midPoint.y*z*z/(float)(tempZ*2f) + z*midPoint.y/2f;

			lineRenderer.SetPosition(i, new Vector3(x + Random.Range(-posRange, posRange),y + Random.Range(-posRange, posRange), z));
		}

		lineRenderer.SetPosition (0, Vector3.zero);
		lineRenderer.SetPosition (noSegment-1, new Vector3(0f, 0f, tempZ));
	}
	
	// Update is called once per frame
	void Update () {

		color.a -= colorTimeModifier * Time.deltaTime;

		lineRenderer.SetColors (color, color);

		if (color.a <= 0f)
			Destroy (this.gameObject);

		if(procedural){
			midPoint = new Vector2 (Random.Range(-radius, radius), Random.Range(-radius, radius));

			if(tempZ < maxZ){
				tempZ += (Time.deltaTime * 10f) * speedLightningRaise;
			}else if(tempZ > maxZ){
				tempZ -= (Time.deltaTime * 10f) * speedLightningRaise;
			}

			for (int i = 0; i < noSegment-1; i++) {

				float z = ((float)i * tempZ / (float)(noSegment-1));

				float x = -midPoint.x*z*z/(float)(tempZ*2f) + z*midPoint.x/2f;

				float y = -midPoint.y*z*z/(float)(tempZ*2f) + z*midPoint.y/2f;

				lineRenderer.SetPosition(i, new Vector3(x + Random.Range(-posRange, posRange),y + Random.Range(-posRange, posRange), z));
			}

			lineRenderer.SetPosition (0, Vector3.zero);
			lineRenderer.SetPosition (noSegment-1, new Vector3(0f, 0f, tempZ));
		}

	}
}
