using UnityEngine;
using System.Collections;

public class scObstacle : MonoBehaviour {

    private scLevelController levelController;
    private bool isHit = false;

	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
	}
	
	void Update () {
	
	}

    void OnTriggerEnter( Collider other){
        if (other.collider.tag == "Player"){
            if (!isHit){

                float multiplier = 1;

                if (Random.Range(0,10) >= 5 ){
                    multiplier = -1;
                }

                Vector3 force = new Vector3(multiplier* 20,0,0);
                other.gameObject.rigidbody.AddForce(force, ForceMode.Impulse);
                isHit = true;
            }
        }
    }
}
