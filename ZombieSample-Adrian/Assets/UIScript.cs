using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

    GameManager gm;
    private Text hsText;
    private Text goldText;
    private Text levelText;
    private GameObject levelImage;
    private GameObject UIPanel;

    Scene s;

    public static UIScript instance = null;

    void Awake()
    {
        gm = GameManager.instance;
        goldText = GameObject.Find("GoldText").GetComponent<Text>();


        hsText = GameObject.Find("ScoreText").GetComponent<Text>();


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
        Debug.Log("UI inistialized");

        s = SceneManager.GetActiveScene();

        if (s.name == "MainMenu")
        {
            DisableUI();
        }
        else
        {

        }
    }

    public void DisableUI()
    {
        if (s.name == "MainMenu")
        {
            gameObject.SetActive(false);
        }

    }

    public void EnableUI()
    {
        gameObject.SetActive(true);
    }
}
