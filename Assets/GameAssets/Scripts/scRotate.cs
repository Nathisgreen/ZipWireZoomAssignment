using UnityEngine;
using System.Collections;

public class scRotate : MonoBehaviour {

    private Vector3 workVector = new Vector3();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        workVector.z = 45 * Time.deltaTime;
        transform.Rotate(workVector);
	}
}
