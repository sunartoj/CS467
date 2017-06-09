using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startShooting : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie" && gameObject.tag == "Gun")
        {
            anim = GetComponentInParent<Animator>();
            anim.SetBool("isFiring", true);
        }
    }
}
