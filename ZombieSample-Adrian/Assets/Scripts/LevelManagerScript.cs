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


    public GameObject[] theZombie;

    GameManager gm;

    float nextSpawnTime;
    Animator cannonAnim;        //allows us to control animation

    //for enemies
    int maxEnemies;
    int zombieCount;

    GameObject[] zombies;
    GameObject[] spawnPts;
    bool spawned = false;       //at least 1 zomnbie has been spawned
    bool win = false;

    void Awake()
    {
        gm = GameManager.instance;
        spawnPts = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Use this for initialization
    void Start()
    {
        CreateLevel();
        nextSpawnTime = Time.time + 5f;


        //how many enemies per level
        maxEnemies = gm.level * 10;
        zombieCount = 0;

        Debug.Log("Created new level");
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
                LoadNext();
            }
        }
    }

    public void createSpawn()
    {
        GameObject point = spawnPts[Random.Range(0, 5)];
        GameObject temp = Instantiate(theZombie[Random.Range(0, 2)], point.transform.position, Quaternion.identity); //the quaternion means no rotation
        temp.layer = point.layer;
        zombieCount++;
        spawned = true;

        //Debug.Log ("Zombie Number: " + zombieCount);
    }

    void LoadNext()
    {
        SceneManager.LoadScene(1);
    }

}
