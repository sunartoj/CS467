using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;	
	public float levelStartDelay = 2f;						//Time to wait before starting level, in seconds.

	public int currScore { get; set; }
    public int currGold { get; set; }
    public int hiScore { get; set; }
    public int levelStartScore { get; set;  }
    public int levelStartGold { get; set; }
    public int level { get; set; }
    public int pillBottleCount { get; set; }
    public int recordPlayerCount { get; set; }
    private Text hsText;
    private Text goldText;
	private Text levelText;
	private GameObject levelImage;
	Button mainMenu;
	Button RestartLevel;

    #region DropTowers

    public TowerBtn ClickedBtn { get; set; }

    #endregion

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
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
		level = 0;
		currScore = 0;
        hiScore = 0;
        currGold = 200;
        pillBottleCount = 0;
        recordPlayerCount = 0;
    }

	//This is called each time a scene is loaded.
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
        Debug.Log("Initializing a level");

		//Scene s = SceneManager.GetActiveScene ();

        if (scene.name=="WinScene")
        {

            if (currScore > hiScore)
            {
                hiScore = currScore;
            }
        }
        else
        {
            if (scene.name == "MainMenu")
            {

            }
            else
            { 

                //Add one to our level number.
                level++;
                InitLevel();

            }

            Debug.Log("Loaded Level " + level);
        }

	}
		
    //this is for loading playing level
	void InitLevel()
	{
        levelStartScore = currScore;        //for reloading the level if died
        levelStartGold = currGold;

        Debug.Log ("init: " + level);
	}
		

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
        levelText.text = "Game Over . . .";     //change this to a scene with buttons to restart level
                                                //or go to main menu

        levelImage.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        RestartLevel.gameObject.SetActive(true);

        enabled = false;

        ResetCounts();

    }

    private void ResetCounts()
    {
        if (currScore > hiScore)
        {
            hiScore = currScore;
        }

        currScore = 0;
        currGold = 200;
        pillBottleCount = 0;
        recordPlayerCount = 0;
    }

    public void DislayScoreScore()
    {

    }

    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
    }

}
