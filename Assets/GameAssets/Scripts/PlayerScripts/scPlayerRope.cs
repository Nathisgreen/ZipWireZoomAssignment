using UnityEngine;
using System.Collections;

public class scPlayerRope : MonoBehaviour {

    public GameObject startPosition;
    public GameObject endPosition;
    public Material lineMaterial;

    private LineRenderer lineRenderer;


	// Use this for initialization
	void Start () {

        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPosition.transform.position);
        lineRenderer.SetPosition(1, endPosition.transform.position);

        lineRenderer.SetWidth(0.1f, 0.05f);
        lineRenderer.material = lineMaterial;
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, startPosition.transform.position);

	}
}
