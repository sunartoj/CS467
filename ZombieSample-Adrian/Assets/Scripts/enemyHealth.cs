using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour {

	public int maxHealth;
	int currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		Debug.Log ("Current Health: " + currentHealth); 
		if (currentHealth <= 0) {
			Destroy (gameObject);
		}
	}
}
