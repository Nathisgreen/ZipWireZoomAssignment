using UnityEngine;
using System.Collections;

public class scRotate : MonoBehaviour {

    private Vector3 workVector = new Vector3();

	void Start () {
	
	}
	
	void Update () {
        workVector.z = 45 * Time.deltaTime;
        transform.Rotate(workVector);
	}
}
