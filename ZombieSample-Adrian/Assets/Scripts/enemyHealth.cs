using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemyHealth : MonoBehaviour {

	public int maxHealth;
	int currentHealth;
	public int points;

	GameManager gm = GameManager.instance;
	private Text hsText;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		hsText = GameObject.Find ("ScoreText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//this is for when character takes damage
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		//Debug.Log ("Current Health: " + currentHealth); 
		if (currentHealth <= 0) {
			Destroy (gameObject);

			gm.currScore += points;
			DislayScoreScore ();
		}
	}

	void DislayScoreScore()
	{		
		hsText.text = "Score: " + gm.currScore;
	}
}
