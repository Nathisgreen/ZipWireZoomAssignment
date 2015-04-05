using UnityEngine;
using System.Collections;

public class scCollectable : MonoBehaviour {

    private scLevelController levelController;
    private bool isHit = false;

	// Use this for initialization
	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
	}
	
	// Update is called once per frame
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
