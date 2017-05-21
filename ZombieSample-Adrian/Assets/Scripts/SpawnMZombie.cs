using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMZombie : MonoBehaviour {
	public GameObject[] theZombie;
	public Transform spawnFrom;

	float nextSpawnTime;
	Animator cannonAnim;		//allows us to control animation

	// Use this for initialization
	void Start () {
		nextSpawnTime = Random.Range(8f, 15f);		//shoot immediately
	}

	// Update is called once per frame
	void Update () {
		if (nextSpawnTime < Time.time) {
			nextSpawnTime = Time.time + Random.Range(10f, 15f); 		//reset when we can spawn next. Maybe randomize?

			GameObject temp = Instantiate (theZombie[Random.Range (0, 2)], spawnFrom.position, Quaternion.identity); //the quaternion means no rotation
			temp.layer = gameObject.layer;

			//Debug.Log("Created on layer" + temp.layer.ToString());
		}
	}
}
