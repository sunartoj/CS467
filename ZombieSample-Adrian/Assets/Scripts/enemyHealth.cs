using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemyHealth : MonoBehaviour {

    public int points;
    public int maxHealth;
    public bool canDropItem;
    public GameObject theDrop;
    public float chanceToDrop;

	int currentHealth;

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

            if (canDropItem)
            {
                float rVal = Random.value;
                if (rVal < chanceToDrop)
                {
                    //Debug.Log("Rval: " + rVal);
                    Instantiate(theDrop, new Vector3(transform.position.x - .1f, transform.position.y - .45f, transform.position.z), Quaternion.identity);
                }
            }

			gm.currScore += points;
			DislayScoreScore ();
		}
	}

	void DislayScoreScore()
	{		
		hsText.text = "Score: " + gm.currScore;
	}
}
