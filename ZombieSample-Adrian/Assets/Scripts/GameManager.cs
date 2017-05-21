using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;	
	public float levelStartDelay = 2f;						//Time to wait before starting level, in seconds.

	public int highScore { get; set; }
	private Text hsText;
	private Text levelText;
	private int level = 0;	
	private GameObject levelImage;
	//private bool doingSetup;


	// Use this for initialization
	void Awake () {
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);	

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);	

	}

	void Start()
	{
//		Scene s = SceneManager.GetActiveScene ();
//		if (s.name != "MainMenu") {
//			InitLevel ();			
//		}

		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
		

	void InitLevel()
	{
		//fidn object first, because cant find it if it is inactive.
		levelImage = GameObject.Find ("lvlImage");
		levelText = GameObject.Find ("lvlText").GetComponent<Text> ();
		levelImage.SetActive (false);
		levelText.text = "Level " + level;

		hsText = GameObject.Find ("ScoreText").GetComponent<Text> ();
		hsText.text = "Score: " + highScore;

		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		Debug.Log ("init: " + level);
	}

	//This is called each time a scene is loaded.
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode
		mode)
	{
		//Add one to our level number.
		level++;
		//Call InitGame to initialize our level.

		Debug.Log ("Loaded Level " + level);
		InitLevel();
	}

//	void OnEnable()
//	{
//		//Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as
//		//this script is enabled.
//		SceneManager.sceneLoaded += OnLevelFinishedLoading;
//	}
//	void OnDisable()
//	{
//		//Tell our ‘OnLevelFinishedLoading’ function to stop listening for a scene change event as soon as this script is disabled.
//		//Remember to always have an unsubscription for every delegate you subscribe to!
//		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
//	}

	//Hides black image used between levels
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);

		//Set doingSetup to false allowing player to move again.
		//doingSetup = false;
	}

	public void GameOver()
	{
		levelText.text = "Game Over . . .";		//change this to a scene with buttons to restart level
												//or go to main menu

		levelImage.SetActive (true);
		enabled = false;

	}

	IEnumerator SomeDelay()
	{
		yield return new WaitForSeconds (3);
	}

}
