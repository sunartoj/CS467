using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaCtrl : MonoBehaviour {

    public int maxHealth;
    public int cost;
	int currentHealth;

    Animator enemyAnim;

    //used to handle when this object is destroyed
    public delegate void OnDestroyDelegate(MonoBehaviour instance);
    public event OnDestroyDelegate OnDestroyEvnt;

    // Use this for initialization
    void Start () {
		currentHealth = maxHealth;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag=="Zombie")
        {
            enemyAnim = other.GetComponent<Animator>();
            enemyAnim.SetBool("isAttacking", true);

            TakeDamage (1);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag=="Zombie")
        {


			TakeDamage (1);
			//Debug.Log ("Took Damage");
		}
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            enemyAnim = other.GetComponent<Animator>();
            enemyAnim.SetBool("isAttacking", false);
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

    void OnDestroy()
    {

        GameObject parent = this.transform.parent.gameObject;
        tileScript tl = parent.GetComponent<tileScript>();
        tl.isEmpty = true;

        // Send notification that this object is about to be destroyed
        if (this.OnDestroyEvnt != null) this.OnDestroyEvnt(this);
    }
}
