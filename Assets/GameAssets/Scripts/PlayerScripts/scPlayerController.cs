using UnityEngine;
using System.Collections;

public class scPlayerController : MonoBehaviour {

    private const float HorizontalInputDeadZone =1f;
    private const float HorizontalMoveForce = 50f;
    private const float MaxHorizontalVelocity = 5f;
    

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x <= HorizontalInputDeadZone) {
            rigidbody.AddForce(-rigidbody.transform.right * HorizontalMoveForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x >= -HorizontalInputDeadZone) {
            rigidbody.AddForce(rigidbody.transform.right * HorizontalMoveForce, ForceMode.Force);
        }

        if (rigidbody.velocity.x > MaxHorizontalVelocity) {
            rigidbody.velocity = new Vector3(MaxHorizontalVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (rigidbody.velocity.x < -MaxHorizontalVelocity) {
            rigidbody.velocity = new Vector3(-MaxHorizontalVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }

	}
}
