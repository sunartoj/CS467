using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement3 : MonoBehaviour {

	public float speed = 10f;
	private Transform target;
	private int wavepointIndex = 0;

	void Start() {
		target = Waypoints3.points [0];
	}

	void Update(){
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= 0.2f) {
			GetNextWaypoint ();
		}
	}

	void GetNextWaypoint() {
		if(wavepointIndex >= Waypoints3.points.Length -1) {
			Destroy (gameObject);
			return;
		}
		wavepointIndex++;
		target = Waypoints3.points [wavepointIndex];
	}
}