using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scScoreUI : MonoBehaviour {

    private scLevelController levelController;

    private Text text;
 
	// Use this for initialization
	void Start () {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scLevelController>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        text.text = levelController.getScoreText();	    
	}
}
