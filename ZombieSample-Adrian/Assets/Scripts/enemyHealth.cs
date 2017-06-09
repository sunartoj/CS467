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

    bool dead;

    Animator anim;
    MoveMe mm;

	public int currentHealth { get; set; }

	GameManager gm = GameManager.instance;
	private Text hsText;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		hsText = GameObject.Find ("ScoreText").GetComponent<Text> ();

        anim = GetComponent<Animator>();
        mm = GetComponent<MoveMe>();

        dead = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//this is for when character takes damage
	public void TakeDamage(int damage)
	{

        //when the animationis playing, the zombie gets hit again and wil have a chance to drop again.
        //this stops that
        if (dead)
        {
            return;
        }

		currentHealth -= damage;
		//Debug.Log ("Current Health: " + currentHealth); 
		if (currentHealth <= 0) {

            mm.currSpeed = 0;
            anim.SetBool("isDead", true);

            dead = true;

			Destroy (gameObject, 1.5f);

            

            if (canDropItem)
            {
                float rVal = Random.value;
                if (rVal < chanceToDrop)
                {
                    //Debug.Log("Rval: " + rVal);
                    //GameObject newDrop =  Instantiate(theDrop, new Vector3(transform.position.x - .1f, transform.position.y - .45f, transform.position.z), Quaternion.identity);
                    GameObject newDrop =  Instantiate(theDrop, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    newDrop.GetComponent<SpriteRenderer>().sortingOrder = 15;
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
