using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton pattern, to allow build manager be accessed without a reference
	public static BuildManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogError ("More than one build manager exists");
			return;
		}
		instance = this;
	}

	public GameObject standardTurretPrefab;

	void Start() {
		turretToBuild = standardTurretPrefab;
	}

	private GameObject turretToBuild;

	public GameObject GetTurretToBuild (){
		return turretToBuild;
	}
}
