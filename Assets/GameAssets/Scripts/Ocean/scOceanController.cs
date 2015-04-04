using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scOceanController : MonoBehaviour {

    private const float UnitSize = 1.5f;
    private const int OceanPieceWidth = 10;
    private const int OceanPieceLength = 10;

    private const float OceanYPosition = -1;

    private Vector3 workVector = new Vector3();

    private GameObject lastCreatedOceanPiece;
    private Vector3 lastCreatedOceanPieceStartPosition;
    private float spawnDistance = 0;

    private scLevelController levelController;
    
	// Use this for initialization
	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
        createInitalOcean();
	}
	
	// Update is called once per frame
	void Update () {
        checkSpawnNextPiece();
	}

    public float getUnitSize(){
        return UnitSize;
    }

    public int getPieceWidth(){
        return OceanPieceWidth;
    }

    public int getPieceLength(){
        return OceanPieceLength;
    }

    private void checkSpawnNextPiece(){
        if (lastCreatedOceanPiece != null){
            if (Vector3.Distance(lastCreatedOceanPieceStartPosition,lastCreatedOceanPiece.transform.position) > spawnDistance){
                createOceanPiece(lastCreatedOceanPieceStartPosition - transform.position, levelController.getWorldSpeed());
            }
        }
    }

    private void createInitalOcean(){
        for (int i = 0; i < 3; i++){

            workVector.x = -(OceanPieceWidth * UnitSize)/2;
            workVector.y = OceanYPosition;
            workVector.z = i * (OceanPieceLength * UnitSize);

            createOceanPiece(workVector, 0);
        }
    }

    private void createOceanPiece(Vector3 position, float worldMoveSpeed){
        GameObject oceanPiece = (GameObject)GameObject.Instantiate(Resources.Load("OceanSection"));

        oceanPiece.transform.position = workVector;

        oceanPiece.GetComponent<scOceanSection>().initSection();

        if (lastCreatedOceanPiece != null){
            oceanPiece.GetComponent<scOceanSection>().buildMeshSection(
                lastCreatedOceanPiece.GetComponent<scOceanSection>().getVerticies(), lastCreatedOceanPiece, worldMoveSpeed);
        }else{
            oceanPiece.GetComponent<scOceanSection>().buildMeshSection(new List<Vector3>(), lastCreatedOceanPiece, worldMoveSpeed);
        }

        lastCreatedOceanPiece = oceanPiece;
        lastCreatedOceanPieceStartPosition = oceanPiece.transform.position;

        spawnDistance = OceanPieceLength * UnitSize;
    }
}
