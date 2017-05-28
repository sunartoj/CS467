using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

	public GameObject gameManager;
	private Text hsText;

	// Use this for initialization
	void Start () 
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (GameManager.instance == null)
			//Instantiate gameManager prefab
			Instantiate(gameManager);

		//DislayHighScore ();
	}

	void DislayHighScore()
	{
		GameManager gm = GameManager.instance;

		hsText = GameObject.Find ("highScoreText").GetComponent<Text> ();
		hsText.text = "High Score: " + gm.hiScore;
	}
}
