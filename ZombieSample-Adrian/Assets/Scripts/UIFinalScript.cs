using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScript : MonoBehaviour {

    GameManager gm;
    Text goldText;
    Text scoreText;

    // Use this for initialization
    void Start () {
        gm = GameManager.instance;
        scoreText = GameObject.Find("sText").GetComponent<Text>();
        goldText = GameObject.Find("gText").GetComponent<Text>();

        scoreText.text = "Score: " + gm.currScore;
        goldText.text = "Gold: " + gm.currGold;
    }
	
}
