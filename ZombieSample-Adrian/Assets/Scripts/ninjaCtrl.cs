using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaCtrl : MonoBehaviour {
	
	int maxHealth = 350;
	int currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag=="Zombie") {
			TakeDamage (1);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag=="Zombie") {
			TakeDamage (1);
			//Debug.Log ("Took Damage");
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		//Debug.Log ("Took damage: " + currentHealth); 
		if (currentHealth <= 0) {
			Destroy (gameObject);
		}
	}
}
