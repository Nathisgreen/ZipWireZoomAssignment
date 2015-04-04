using UnityEngine;
using System.Collections;

public class scPlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.rigidbody.AddForce(new Vector3(500, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
