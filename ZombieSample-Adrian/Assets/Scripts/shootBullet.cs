using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour {
	public GameObject theProjectile;
	public float shootTime; //delay in shooting a spore
	public Transform shootFrom;

	float nextShootTime;
	Animator cannonAnim;		//allows us to control animation

	// Use this for initialization
	void Start () {
		nextShootTime = 0f;		//shoot immediately
	}
	
	// Update is called once per frame
	void Update () {
		if (nextShootTime < Time.time) {
			nextShootTime = Time.time + shootTime; 		//reset when we can shoot next. Maybe randomize?

			Instantiate (theProjectile, shootFrom.position, Quaternion.identity); //the quaternion means no rotation
		}
	}
}
