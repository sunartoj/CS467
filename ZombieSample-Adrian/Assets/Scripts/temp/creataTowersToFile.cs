using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class creataTowersToFile : MonoBehaviour
{
    //public string name;
    //public int maxHealth;
    //public int cost;

    private string towerDataFileName = "towerConfig.json";


    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, towerDataFileName);

        towerConfigArray Towers = new towerConfigArray();

        towerConfig tower1 = new towerConfig("ninja", 350, 25 );
        towerConfig tower2 = new towerConfig("Cowgirl", 400, 50);
        towerConfig tower3 = new towerConfig("Robot", 400, 100);

        Towers.towers[0] = tower1;
        Towers.towers[1] = tower2;
        Towers.towers[2] = tower3;


        string json = JsonUtility.ToJson(Towers);

        File.WriteAllText(filePath, json);
    }
}
