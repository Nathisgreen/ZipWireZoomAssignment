using UnityEngine;
using System.Collections;

public class scPlayerController : MonoBehaviour {

    private const float HorizontalInputDeadZone =1f;
    private const float HorizontalMoveForce = 50f;
    private const float MaxHorizontalVelocity = 5f;

	void FixedUpdate () {

        if (checkLeftInput() && transform.position.x <= HorizontalInputDeadZone) {
            rigidbody.AddForce(-rigidbody.transform.right * HorizontalMoveForce, ForceMode.Force);
        }

        if (checkRightInput() && transform.position.x >= -HorizontalInputDeadZone) {
            rigidbody.AddForce(rigidbody.transform.right * HorizontalMoveForce, ForceMode.Force);
        }

        if (rigidbody.velocity.x >= MaxHorizontalVelocity) {
            rigidbody.velocity = new Vector3(MaxHorizontalVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (rigidbody.velocity.x <= -MaxHorizontalVelocity) {
            rigidbody.velocity = new Vector3(-MaxHorizontalVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        }
	}

    private bool checkLeftInput(){
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || 
            (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width/2);
    }

    private bool checkRightInput() {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ||
            (Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2);
    }
}
