using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scScoreUI : MonoBehaviour {

    private scLevelController levelController;

    private Text text;
 
	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
        text = GetComponent<Text>();
	}
	
	void Update () {
        text.text = levelController.getScoreText();	    
	}
}
