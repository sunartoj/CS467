using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverScript : MonoBehaviour {

    GameManager gm;
    UIScript ui;

    private void Start()
    {
        gm = GameManager.instance;
        ui = UIScript.instance;
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag=="Zombie") {
			ui.GameOver ();
		}
	}
}
