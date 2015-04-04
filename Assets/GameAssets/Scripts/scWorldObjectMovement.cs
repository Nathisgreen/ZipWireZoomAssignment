using UnityEngine;
using System.Collections;

public class scWorldObjectMovement : MonoBehaviour {

    private const float DestroyPosition = -5;
    
    private scLevelController levelController;

    private Vector3 workVector = new Vector3();

	// Use this for initialization
	void Start () {
        levelController = GameObject.FindWithTag("LevelController").GetComponent<scLevelController>();
	}
	
	// Update is called once per frame
	void Update () {
        workVector = transform.position;

        workVector.z -= levelController.getWorldSpeed() * Time.deltaTime;

        transform.position = workVector;

        if (transform.position.z < DestroyPosition){
            if (this.gameObject.tag == "ChunkEnd"){
                Destroy(transform.parent.gameObject);
            }
        }
	}
}
