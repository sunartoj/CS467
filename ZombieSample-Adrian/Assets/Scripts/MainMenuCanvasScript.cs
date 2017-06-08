using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuCanvasScript : Singleton<MainMenuCanvasScript> {

    GameManager gm;
    Text hsText;

    public static MainMenuCanvasScript instance = null;
    void Awake()
    {
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        gm = GameManager.instance;
        hsText = GameObject.Find("HighScoreText").GetComponent<Text>();
        hsText.text = "High Score: " + gm.hiScore.ToString();

        Scene s = SceneManager.GetActiveScene();

    }
	
}
