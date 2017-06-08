using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController instance = null;
    GameManager gm;
    Text playerName;
    Text highScore;
    List<PlayerProgress> pg;

    Button myButton;

    string theName;

    private string gameDataFileName = "data.json";

    private void Awake()
    {

        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        myButton = GetComponent<Button>(); // <-- you get access to the button component here
        myButton.onClick.AddListener(() => { LoadPlayer(); });  // <-- you assign a method to the button OnClick event here

        pg = new List<PlayerProgress>();
        playerName = GameObject.Find("playerName").GetComponent<Text>();
        highScore = GameObject.Find("HighScoreText").GetComponent<Text>();


    }

    public void LoadData()
    {
        gm = GameManager.instance;


        //TURNS OUT - this does not work with webgl!!!!!!! 
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        //string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        TextAsset txt = Resources.Load("data") as TextAsset;


        // Read the json from the file into a string
        string dataAsJson = txt.text;
        // Pass the json to JsonUtility, and tell it to create a GameData object from it
        SavedGameData sv = new SavedGameData();
        sv = JsonUtility.FromJson<SavedGameData>(dataAsJson);

        if (sv.saves != null)
        {
            pg = sv.saves;
        }

    }

    private void SaveData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        try
        {
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            SavedGameData sv = new SavedGameData();
            sv.saves = pg;

            string json = JsonUtility.ToJson(sv);


            //write json to file
            File.WriteAllText(filePath, json);
            Debug.Log("Saved game data!");
        }
        catch (System.Exception e)
        {

            Debug.LogError(e.ToString());
        }


    }

    public void LoadPlayer()
    {
        LoadData();

        //playerName = GameObject.Find("playerName").GetComponent<Text>();

        bool isFound = false;

        foreach (var item in pg)
        {
            if (item.name == playerName.text)
            {
                gm.playerName = item.name;
                gm.hiScore = item.highestScore;
                //gm.level = item.highestLevel - 1;

                highScore.text = "High Score: " + item.highestScore;
                isFound = true;
                break;
            }
        }

        if (!isFound)
        {
            gm.playerName = playerName.text;
            highScore.text = "High Score: 0";
        }

    }

    public void SavePlayer()
    {
        bool isFound = false;

        foreach (var item in pg)
        {
            if (item.name == playerName.text)
            {
                item.name = gm.playerName;
                item.highestScore = gm.hiScore;
                item.highestLevel = gm.level;
                isFound = true;
                break;
            }
        }

        //PlayerProgress item = pg.Find(x => x.name == playerName.text);

        if (!isFound)
        {
            PlayerProgress p = new PlayerProgress();
            p.name = gm.playerName;
            p.highestScore = gm.hiScore;
            p.highestLevel = gm.level;

            pg.Add(p);
        }

        SaveData();
    }
}
