using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class loadEnemies
{
    private static string enemyDataFileName = "enemyConfig.json";

    public static enemyConfig[] LoadEnemies()
    {
        //this doe snot work for webgl
        //string filePath = Path.Combine(Application.streamingAssetsPath, enemyDataFileName);
        //string dataAsJson = File.ReadAllText(filePath);

        TextAsset txt = Resources.Load("enemyConfig") as TextAsset;
        string dataAsJson = txt.text;

        enemyConfigArray en = new enemyConfigArray();
        en = JsonUtility.FromJson<enemyConfigArray>(dataAsJson);

        return en.enemies;
    }
}
