using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverScript : MonoBehaviour {

	GameManager gm = GameManager.instance;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag=="Zombie") {
			gm.GameOver ();
		}
	}
}
