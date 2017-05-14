using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMeByForce : MonoBehaviour
{
    private float push = 4f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.AddForce((Vector2.right * push),ForceMode2D.Force);
	}
}
