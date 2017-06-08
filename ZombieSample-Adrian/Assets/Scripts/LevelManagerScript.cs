using System.Collections;
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

    //this is for loading zombie data from file
    public enemyConfig[] enemyData = new enemyConfig[3];

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

        spawnPts = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Use this for initialization
    void Start()
    {
        gm = GameManager.instance;

        enemyData = loadEnemies.LoadEnemies();

        Scene s = SceneManager.GetActiveScene ();
        Debug.Log("We are currently on scene: " + s.name);

        if (s.name == "MainMenu")
        {
            if (gm.isReload)
            {
                uis.HidePanel();
                uis.HideLevelImage();
            }

        }
        else if (s.name == "WinScene")
        {
            uis.HideLevelImage();
            uis.DisableUI();
        }
        else if (s.name == "GameOver")
        {
            uis.HideLevelImage();
            uis.DisableUI();
            Debug.Log("Levelmngr Game Over....");
        }
        else
        {
            uis.EnableUI();
            uis.ShowPanel();
            uis.ShowLevelImage();
            uis.UpdateText();

            Invoke("HideLevelImage", 3);

            CreateLevel();
            nextSpawnTime = Time.time + 5f;

            //how many enemies per level
            maxEnemies = 10 * gm.level + (gm.level-1)*5;
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
            return;
        }
        if (zombieCount < maxEnemies && nextSpawnTime < Time.time)
        {
            nextSpawnTime = Time.time + Random.Range(2f, 3f);      //reset when we can spawn next. Maybe randomize?
            createSpawn();
        }
        else if (nextSpawnTime < Time.time)
        {
            zombies = GameObject.FindGameObjectsWithTag("Zombie");
            if (zombies.Length == 0 && spawned)
            {
                win = true;
                Debug.Log("Win!");
                Invoke("LoadNext", 2);
                //LoadNext();
            }
        }
    }

    public void createSpawn()
    {
        GameObject point = spawnPts[Random.Range(0, 5)];
        int whichZombie = Random.Range(0, enemyLevel);
        GameObject temp = Instantiate(theZombie[whichZombie], point.transform.position, Quaternion.identity); //the quaternion means no rotation
        temp.layer = point.layer;

        enemyHealth eh = temp.GetComponent<enemyHealth>();

        //enemyData - this is the data saved in file we need to set values from
        eh.points = enemyData[whichZombie].points;
        eh.maxHealth = enemyData[whichZombie].maxHealth;
        eh.canDropItem = enemyData[whichZombie].canDropItem;
        eh.chanceToDrop = enemyData[whichZombie].chanceToDrop;

        zombieCount++;
        spawned = true;

        //Debug.Log ("Zombie Number: " + zombieCount);
    }

    //load scene. I only have one at this time...
    void LoadNext()
    {
        if (gm.level == 3)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(gm.level + 1);
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
