using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Transform enemyPrefab1;
	public Transform enemyPrefab2;
	public Transform enemyPrefab3;
	public Transform enemyPrefab4;
	public Transform enemyPrefab5;

	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;
	public Transform spawnPoint5;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	public Text waveCountdownText;

	private int waveIndex = 1;


	void Update() {
		if (countdown <= 0f) {
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;

		waveCountdownText.text = Mathf.Round(countdown).ToString ();
	
	}

	IEnumerator SpawnWave() {
		Debug.Log ("Wave Incoming!");
		for (int i = 0; i < waveIndex; i++) {
			SpawnEnemy ();
			yield return new WaitForSeconds (0.5f);
		}
		waveIndex++;

	}

	void SpawnEnemy() {
		Instantiate (enemyPrefab1, spawnPoint1.position, spawnPoint1.rotation);
		Instantiate (enemyPrefab2, spawnPoint2.position, spawnPoint2.rotation);
		Instantiate (enemyPrefab3, spawnPoint3.position, spawnPoint3.rotation);
		Instantiate (enemyPrefab4, spawnPoint4.position, spawnPoint4.rotation);
		Instantiate (enemyPrefab5, spawnPoint5.position, spawnPoint5.rotation);
	}
}
