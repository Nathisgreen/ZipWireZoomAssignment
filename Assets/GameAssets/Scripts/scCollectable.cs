using UnityEngine;
using System.Collections;

public class scCollectable : MonoBehaviour {

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
                levelController.addToScore(10);
                Destroy(this.gameObject);
                isHit = true;
            }
        }
    }
}
