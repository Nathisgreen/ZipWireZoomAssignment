using UnityEngine;
using System.Collections;

public class scLevelController : MonoBehaviour {
    private float worldSpeed = 20f;

    private GameObject lastChunkEndPiece;

    private Object[] allChunks;

    private float spawnZ = 30;

	// Use this for initialization
	void Start () {
        allChunks = Resources.LoadAll("LevelChunks", typeof(GameObject));
        loadNextChunk();
	}
	
	// Update is called once per frame
	void Update () {
        if (lastChunkEndPiece != null){
            if (lastChunkEndPiece.transform.position.z < 30){
                loadNextChunk();
            }
        }
	}

    public float getWorldSpeed() {
        return worldSpeed;
    }

    private void loadNextChunk(){
        int randomChunk = Random.Range(0, allChunks.Length);
        GameObject nextChunk = (GameObject) GameObject.Instantiate(allChunks[randomChunk]);
        nextChunk.transform.position = new Vector3(0, 0, spawnZ);

        foreach(Transform child in nextChunk.transform){
            if (child.gameObject.tag == "ChunkEnd"){
                lastChunkEndPiece = child.gameObject;
                break;
            }
        }
    }
}
