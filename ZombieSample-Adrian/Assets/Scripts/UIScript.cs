using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    GameManager gm;
    private Text scoreText;
    private Text goldText;
    private Text levelText;
    private GameObject levelImage;
    private GameObject UIPanel;

    Button mainMenu;
    Button RestartLevel;

    Scene s;

    public static UIScript instance = null;

    void Awake()
    {

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

    private void Start()
    {
        gm = GameManager.instance;

        Debug.Log("UI inistialized");

        levelImage = GameObject.Find("lvlImage");
        UIPanel = GameObject.Find("UIPanel");

        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelText = GameObject.Find("lvlText").GetComponent<Text>();

        mainMenu = GameObject.Find("MenuButton").GetComponent<Button>();
        RestartLevel = GameObject.Find("RestartLevelButton").GetComponent<Button>();

        UpdateText();
        HideLevelImage();
        HidePanel();
    }

    public void DisableUI()
    {
        gameObject.SetActive(false);
    }

    public void EnableUI()
    {
        gameObject.SetActive(true);
    }

    public void GameOver()
    {

        //change this to a scene with buttons to restart level or go to main menu
        //levelText.text = "Game Over . . .";    
        //levelImage.SetActive(true);
        //mainMenu.gameObject.SetActive(true);
        //RestartLevel.gameObject.SetActive(true);

        SceneManager.LoadScene(5);

    }

    //Hides black image used between levels
    public void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);
    }
    public void ShowLevelImage()
    {
        //SHows the level user is on
        levelImage.SetActive(true);
        levelText.text = "Level " + gm.level;
        HideButtons();
    }

    public void HidePanel()
    {
        //Disable the panel gameObject.
        UIPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        //Disable the levelImage gameObject.
        UIPanel.SetActive(true);
    }

    public void UpdateText()
    {
        scoreText.text = "Score: " + gm.currScore;
        goldText.text = "Gold: " + gm.currGold;
    }

    public void HideButtons()
    {
        mainMenu.gameObject.SetActive(false);
        RestartLevel.gameObject.SetActive(false);
    }
}
