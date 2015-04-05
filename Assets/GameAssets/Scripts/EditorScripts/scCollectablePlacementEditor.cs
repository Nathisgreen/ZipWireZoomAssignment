using UnityEngine;
using System.Collections;

//Editor script to lock collectables to positions where they can actually be collected by the player
[ExecuteInEditMode]
public class scCollectablePlacementEditor : MonoBehaviour {

    private Vector3 lastPos;

    private float circumference = 2.215f; //distance between player and anchor point

    private Vector3 anchorPoint;


	void Start () {
        anchorPoint = new Vector3(0, circumference, 0);
	}
	
	void Update () {

        //only apply logic when this object has moved in the editor
        if (transform.position != lastPos){

            anchorPoint.z = transform.position.z;
            Vector3 dir = transform.position - (anchorPoint);

            dir.Normalize();

            transform.position = (anchorPoint) + (dir * (circumference -0.5f));

            lastPos = transform.position;
        }
	
	}
}
