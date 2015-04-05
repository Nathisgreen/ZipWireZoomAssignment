using UnityEngine;
using System.Collections;
using System.Text;
public class scLevelController : MonoBehaviour {
    private float worldSpeed = 15f;

    private GameObject lastChunkEndPiece;

    private Object[] allChunks;

    private float spawnZ = 30;

    private int score = 0;
    private string scoreString = "Score: 0";

	// Use this for initialization
	void Start () {
        allChunks = Resources.LoadAll("LevelChunks", typeof(GameObject));
        loadNextChunk();
        addToScore(10);
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

    public void addToScore(int scoreToAdd){
        score += scoreToAdd;
        scoreString = "Score " +score.ToString();
    }

    public string getScoreText(){
        return scoreString;
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
