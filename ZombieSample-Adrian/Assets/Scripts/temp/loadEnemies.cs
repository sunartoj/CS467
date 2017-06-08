using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class loadEnemies
{
    private static string enemyDataFileName = "enemyConfig.json";

    public static enemyConfig[] LoadEnemies()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, enemyDataFileName);
        string dataAsJson = File.ReadAllText(filePath);

        enemyConfigArray en = new enemyConfigArray();
        en = JsonUtility.FromJson<enemyConfigArray>(dataAsJson);

        return en.enemies;
    }
}
