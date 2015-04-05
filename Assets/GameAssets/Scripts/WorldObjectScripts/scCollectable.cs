using UnityEngine;
using System.Collections;

public class scCollectable : MonoBehaviour {

    private scLevelController levelController;
    private bool isHit = false;
    public GameObject particleSystemToPlay;

	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
	}

    void OnTriggerEnter( Collider other){
        if (other.collider.tag == "Player"){
            if (!isHit){
                levelController.addToScore(10);

                if (particleSystemToPlay != null) {
                    createParticleSystem();
                }
                Destroy(this.gameObject);
                isHit = true;
            }
        }
    }

    private void createParticleSystem(){
        GameObject part = (GameObject)GameObject.Instantiate(particleSystemToPlay, transform.position, Quaternion.identity);
        part.transform.position = transform.position;
        part.AddComponent<scParticleController>();
    }
}
