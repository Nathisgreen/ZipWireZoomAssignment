using UnityEngine;
using System.Collections;

public class scRotate : MonoBehaviour {
    public float rotateSpeed = 45;
    private Vector3 workVector = new Vector3();
	
	void Update () {
        workVector.z = rotateSpeed * Time.deltaTime;
        transform.Rotate(workVector);
	}
}
