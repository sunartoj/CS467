using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class createEnemiestoFile : MonoBehaviour
{

    //public int points;
    //public int maxHealth;
    //public bool canDropItem;
    //public float chanceToDrop;

    private string enemyDataFileName = "enemyConfig.json";

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, enemyDataFileName);

        enemyConfigArray Enemies = new enemyConfigArray();

        enemyConfig enemy1 = new enemyConfig(65,65,true,0.5f);        
        enemyConfig enemy2 = new enemyConfig(100,100,true,0.15f);        
        enemyConfig enemy3 = new enemyConfig(150,125,true,0.15f);

        Enemies.enemies[0] = enemy1;
        Enemies.enemies[1] = enemy2;
        Enemies.enemies[2] = enemy3;


        string json = JsonUtility.ToJson(Enemies);

        File.WriteAllText(filePath, json);
    }
}
