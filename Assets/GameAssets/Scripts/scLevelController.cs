using UnityEngine;
using System.Collections;

public class scLevelController : MonoBehaviour {

    private const float BaseWorldSpeed = 15f;

    private float worldSpeed;

    private GameObject lastChunkEndPiece;

    private Object[] allChunks;

    private float spawnZ = 30;

    private int score = 0;
    private string scoreString = "Score: 0";

    private bool isPenaltySlowDown = false;

    private bool hasSpeedPowerUp = false;
    private float speedPowerUpTime = 0;
    private float speedPowerUpLength = 5;

	void Start () {
        worldSpeed = BaseWorldSpeed;
        allChunks = Resources.LoadAll("LevelChunks", typeof(GameObject));
        loadNextChunk();
        addToScore(10);
	}
	
	void Update () {
        chunkSpawningControl();
        penaltySlowDownControl();
        speedPowerUpControl();
	}

    public float getWorldSpeed() {
        return worldSpeed;
    }

    public void addToScore(int scoreToAdd){
       score += scoreToAdd;
       scoreString = "Score: " + score;
    }

    public string getScoreText(){
        return scoreString;
    }

    public void setPenaltyWorldSpeed(){
        if (!hasSpeedPowerUp){
            worldSpeed = 6;
            isPenaltySlowDown = true;
        }
    }

    public void speedPowerUpCollected(){
        hasSpeedPowerUp = true;
        worldSpeed = BaseWorldSpeed * 2f;
        speedPowerUpTime = 0;
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

    private void chunkSpawningControl(){
        if (lastChunkEndPiece != null) {
            if (lastChunkEndPiece.transform.position.z < 30) {
                loadNextChunk();
            }
        }
    }

    private void penaltySlowDownControl(){
        if (isPenaltySlowDown) {
            if (worldSpeed < BaseWorldSpeed) {
                worldSpeed += 10 * Time.deltaTime;
            }
            else {
                worldSpeed = BaseWorldSpeed;
                isPenaltySlowDown = false;
            }
        }
    }

    private void speedPowerUpControl(){
        if (hasSpeedPowerUp) {
            if (speedPowerUpTime < speedPowerUpLength) {
                speedPowerUpTime += 1 * Time.deltaTime;
            }
            else {
                if (worldSpeed > BaseWorldSpeed) {
                    worldSpeed -= 10 * Time.deltaTime;
                }
                else {
                    hasSpeedPowerUp = false;
                    speedPowerUpTime = 0;
                }
            }
        }
    }
}
