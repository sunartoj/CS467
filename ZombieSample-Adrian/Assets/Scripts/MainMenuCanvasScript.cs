using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasScript : MonoBehaviour {

    GameManager gm;
    Text hsText;

	// Use this for initialization
	void Start () {
        gm = GameManager.instance;
        hsText = GameObject.Find("HighScoreText").GetComponent<Text>();
        hsText.text = "High Score: " + gm.hiScore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
