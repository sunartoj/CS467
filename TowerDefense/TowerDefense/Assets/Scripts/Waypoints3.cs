using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints3 : MonoBehaviour {

	public static Transform[] points;

	void Awake() { //load the childs into the points array
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++) {
			points[i] = transform.GetChild (i);
		}
	}
}