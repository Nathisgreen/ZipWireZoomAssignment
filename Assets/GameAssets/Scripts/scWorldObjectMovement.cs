using UnityEngine;
using System.Collections;

public class scWorldObjectMovement : MonoBehaviour {

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
	}
}
