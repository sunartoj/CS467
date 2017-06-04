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
		level = 1;
		currScore = 0;
        hiScore = 0;
        currGold = 2000;
        pillBottleCount = 0;
        recordPlayerCount = 0;

    }

	//This is called each time a scene is loaded.
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//Scene s = SceneManager.GetActiveScene ();
		hsText = GameObject.Find ("ScoreText").GetComponent<Text> ();

        if (scene.name=="WinScene")
        {
            //fidn object first, because cant find it if it is inactive.
            levelImage = GameObject.Find("lvlImage");
            levelImage.SetActive(true);
            levelText = GameObject.Find("lvlText").GetComponent<Text>();

            mainMenu = GameObject.Find("MenuButton").GetComponent<Button>();
            RestartLevel = GameObject.Find("RestartLevelButton").GetComponent<Button>();

            mainMenu.gameObject.SetActive(true);
            RestartLevel.gameObject.SetActive(false);

            levelText.text = "Winner!";

            goldText = GameObject.Find("GoldText").GetComponent<Text>();
            goldText.text = "Gold: " + currGold;

            hsText = GameObject.Find("ScoreText").GetComponent<Text>();
            hsText.text = "Score: " + currScore;

            ResetCounts();
        }
        else
        {
            if (scene.name != "MainMenu")
            {
                hsText.text = "Score: " + currScore;

                goldText = GameObject.Find("GoldText").GetComponent<Text>();
                goldText.text = "Gold: " + currGold;

                //Add one to our level number.
                level++;
                InitLevel();
            }
            else
            {
                hsText.text = "High Score: " + hiScore;
            }

            Debug.Log("Loaded Level " + level);
        }

	}
		
    //this is for loading playing level
	void InitLevel()
	{
		//fidn object first, because cant find it if it is inactive.
		levelImage = GameObject.Find ("lvlImage");
		levelText = GameObject.Find ("lvlText").GetComponent<Text> ();

		mainMenu = GameObject.Find ("MenuButton").GetComponent<Button> ();
		RestartLevel = GameObject.Find ("RestartLevelButton").GetComponent<Button> ();

        mainMenu.gameObject.SetActive (false);
		RestartLevel.gameObject.SetActive (false);

		levelImage.SetActive (false);
		levelText.text = "Level " + level;

		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

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
        goldText.text = "Gold: " + currGold;
    }

    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
    }

}
