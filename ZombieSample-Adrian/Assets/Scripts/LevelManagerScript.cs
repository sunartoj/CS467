﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : Singleton<LevelManagerScript>
{

    #region Setup TIles

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private Transform map;

    public Dictionary<Point, tileScript> Tiles { get; set; }

    public float TileSizeX
    {
        get
        {
            float x = tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

            //this is getting the size of the sprite
            return x;
        }
    }

    public float TileSizeY
    {
        get
        {
            float x = tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y;

            //this is getting the size of the sprite
            return x;
        }
    }

    void CreateLevel()
    {
        Tiles = new Dictionary<Point, tileScript>();

        int mapXSize = 8;
        int mapYSize = 5;

        Vector3 maxTile = Vector3.zero;

        //this gets the topleft of what is visible on main camera
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(110, 580));     //(0,Screen.height) = top left corner

        //this is for creating the tiles in position
        for (int y = 0; y < mapYSize; y++)
        {
            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(x, y, worldStart);
            }
        }

        maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.position;

    }

    void PlaceTile(int x, int y, Vector3 worldStart)
    {
        int tileIndex = 0;

        tileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<tileScript>();
        newTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .35f);      //opacity

        //thisis to assign point to a  tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + TileSizeX * x, worldStart.y - TileSizeY * y, 0));

    }

    #endregion

    //these are going to be used so that when an item is picked up (mainly for enabling/disabling button)
    public delegate void OnLevelLoaded(int level);
    public static event OnLevelLoaded OnLevelLoadedEvent;

    public GameObject[] theZombie;

    GameManager gm;
    UIScript uis;

    float nextSpawnTime;
    Animator cannonAnim;        //allows us to control animation

    //for enemies
    int maxEnemies;
    int zombieCount;

    GameObject[] zombies;
    GameObject[] spawnPts;
    bool spawned = false;       //at least 1 zomnbie has been spawned
    bool win = false;
    int maxEnemyType;
    private int enemyLevel;     //at level + 1 or max (3 currently, for number of enemy kinds). 
                                //Used so thatonly 1 kind of enemy on level1 , 2 kinds of level 2, three at level 3 or more. 

    void Awake()
    {
        uis = UIScript.instance;
        gm = GameManager.instance;
        spawnPts = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Use this for initialization
    void Start()
    {
        Scene s = SceneManager.GetActiveScene ();

        Debug.Log("We are currently on scene: " + s.name);

        if (s.name == "MainMenu")
        {
            uis.HidePanel();
            uis.HideLevelImage();
        }
        else if (s.name == "WinScene")
        {
            uis.DisableUI();
        }
        else
        {
            uis.EnableUI();
            uis.ShowPanel();
            uis.ShowLevelImage();

            Invoke("HideLevelImage", 3);

            CreateLevel();
            nextSpawnTime = Time.time + 5f;

            //how many enemies per level
            maxEnemies = 1;
            zombieCount = 0;
            maxEnemyType = 3;
            enemyLevel = gm.level > maxEnemyType ? maxEnemyType : gm.level;

            // Send notification that this object is about placed
            if (OnLevelLoadedEvent != null)
                OnLevelLoadedEvent(gm.level);

            Debug.Log("Created new level");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            Invoke("LoadNext", 2);
            return;
        }
        if (zombieCount < maxEnemies && nextSpawnTime < Time.time)
        {
            nextSpawnTime = Time.time + Random.Range(3f, 5f);      //reset when we can spawn next. Maybe randomize?
            createSpawn();
        }
        else if (nextSpawnTime < Time.time)
        {
            zombies = GameObject.FindGameObjectsWithTag("Zombie");
            if (zombies.Length == 0 && spawned)
            {
                win = true;
                Debug.Log("Win!");
                //Invoke("LoadNext", 2);
                //LoadNext();
            }
        }
    }

    public void createSpawn()
    {
        GameObject point = spawnPts[Random.Range(0, 5)];
        GameObject temp = Instantiate(theZombie[Random.Range(0, enemyLevel)], point.transform.position, Quaternion.identity); //the quaternion means no rotation
        temp.layer = point.layer;
        zombieCount++;
        spawned = true;

        //Debug.Log ("Zombie Number: " + zombieCount);
    }

    //load scene. I only have one at this time...
    void LoadNext()
    {
        if (gm.level == 2)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerable Wait()
    {
        yield return new WaitForSeconds(3);
    }

    void HideLevelImage()
    {
        uis.HideLevelImage();
    }
}
