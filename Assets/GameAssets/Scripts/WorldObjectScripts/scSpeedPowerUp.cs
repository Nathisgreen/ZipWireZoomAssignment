using UnityEngine;
using System.Collections;

public class scSpeedPowerUp : MonoBehaviour {

    private scLevelController levelController;
    private bool isHit = false;

	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
	}
	
    void OnTriggerEnter( Collider other){
        if (other.collider.tag == "Player"){
            if (!isHit){
                levelController.speedPowerUpCollected();
                Destroy(this.gameObject);
                isHit = true;
            }
        }
    }
}

