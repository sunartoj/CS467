using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHighScore : MonoBehaviour {

	GameManager gm = GameManager.instance;
	private Text hsText;

	void DislayHighScore()
	{
		hsText = GameObject.Find ("highScoreText").GetComponent<Text> ();
		hsText.text = "High Score: ";// + gm.currScore;
	}
	void Start()
	{
		DislayHighScore ();
	}

}
