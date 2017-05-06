using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag=="Zombie") {
			enemyHealth eh = other.gameObject.GetComponent<enemyHealth> ();
			eh.TakeDamage (damage);

			Destroy (gameObject);
		}
	}
}
