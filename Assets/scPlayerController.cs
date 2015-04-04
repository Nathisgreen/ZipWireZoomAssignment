using UnityEngine;
using System.Collections;

public class scPlayerController : MonoBehaviour {

    private float moveForce = 30;
    private float maxHozVelocity = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x <= 1){
            rigidbody.AddForce(-rigidbody.transform.right * moveForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x >=-1) {
            rigidbody.AddForce(rigidbody.transform.right * moveForce, ForceMode.Force);

        }

        if (rigidbody.velocity.x > maxHozVelocity) {
            rigidbody.velocity = new Vector3(maxHozVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (rigidbody.velocity.x < -maxHozVelocity) {
            rigidbody.velocity = new Vector3(-maxHozVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }

	}
}
