using UnityEngine;
using System.Collections;

public class scOceanController : MonoBehaviour {

    private const float UnitSize = 1.5f;
    private const int OceanPieceWidth = 10;
    private const int OceanPieceLength = 10;

    private const float OceanYPosition = -1;

    private Vector3 workVector = new Vector3();

    private GameObject lastCreatedOceanPiece;
    private Vector3 lastCreatedOceanPieceStartPosition;
    private float spawnDistance = 0;
    
	// Use this for initialization
	void Start () {
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
                createOceanPiece(lastCreatedOceanPieceStartPosition);
            }
        }
    }

    private void createInitalOcean(){
        for (int i = 0; i < 3; i++){

            workVector.x = -(OceanPieceWidth * UnitSize)/2;
            workVector.y = OceanYPosition;
            workVector.z = i * (OceanPieceLength * UnitSize);

            createOceanPiece(workVector);
        }
    }

    private void createOceanPiece(Vector3 position){
        GameObject oceanPiece = (GameObject)GameObject.Instantiate(Resources.Load("OceanSection"));

        oceanPiece.transform.position = workVector;

        lastCreatedOceanPiece = oceanPiece;
        lastCreatedOceanPieceStartPosition = oceanPiece.transform.position;

        spawnDistance = OceanPieceLength * UnitSize;
    }
}
