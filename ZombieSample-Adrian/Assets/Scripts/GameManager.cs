using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerDataController pdc;
    public static GameManager instance = null;
    public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.

    public string playerName { get; set; }
    public int currScore { get; set; }
    public int currGold { get; set; }
    public int hiScore { get; set; }
    public int levelStartScore { get; set; }
    public int levelStartGold { get; set; }
    public int level { get; set; }
    public int pillBottleCount { get; set; }
    public int recordPlayerCount { get; set; }
    private Text scoreText;
    private Text goldText;
    private Text levelText;
    public bool isReload;  //some stupid bug on reloading main menu...


    #region DropTowers

    public TowerBtn ClickedBtn { get; set; }

    #endregion

    // Use this for initialization
    void Awake()
    {
        isReload = false;

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
        currGold = 100;
        pillBottleCount = 0;
        recordPlayerCount = 0;
        playerName = "";

    }

    //This is called each time a scene is loaded.
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Initializing a level");

        //Scene s = SceneManager.GetActiveScene ();

        if (scene.name == "MainMenu")
        {

            level = 0;
            ResetCounts();
            Debug.Log("Reset counts at main menu on Game Manager");
        }
        else if (scene.name == "GameOver")
        {
            //
        }
        else
        {
            //Add one to our level number.
            level++;
            InitLevel();
            
            //Debug.Log("Loaded Level " + level);
        }

    }

    //this is for loading playing level
    void InitLevel()
    {
        levelStartScore = currScore;        //for reloading the level if died
        levelStartGold = currGold;

        Debug.Log("init: " + level);
    }

    public void SaveGame()
    {
        SetHighScore();

        //pdc = PlayerDataController.instance;

        //pdc.SavePlayer();
    }

    private void SetHighScore()
    {
        if (currScore > hiScore)
        {
            hiScore = currScore;
        }
    }

    void ResetCounts()
    {
        SetHighScore();

        currScore = 0;
        currGold = 100;
        pillBottleCount = 0;
        recordPlayerCount = 0;
    }

    public void DislayScoreScore()
    {
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        scoreText.text = "Score: " + currScore;
        goldText.text = "Gold: " + currGold;
    }

    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
    }

}
