﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement5 : MonoBehaviour {

	public float speed = 10f;
	private Transform target;
	private int wavepointIndex = 0;

	void Start() {
		target = Waypoints5.points [0];
	}

	void Update(){
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= 0.2f) {
			GetNextWaypoint ();
		}
	}

	void GetNextWaypoint() {
		if(wavepointIndex >= Waypoints5.points.Length -1) {
			Destroy (gameObject);
			return;
		}
		wavepointIndex++;
		target = Waypoints5.points [wavepointIndex];
	}
}