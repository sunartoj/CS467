using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class loadTowers : MonoBehaviour {

    private static string enemyDataFileName = "towerConfig.json";

    public static towerConfig[] LoadTowers()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, enemyDataFileName);
        string dataAsJson = File.ReadAllText(filePath);

        towerConfigArray en = new towerConfigArray();
        en = JsonUtility.FromJson<towerConfigArray>(dataAsJson);

        return en.towers;
    }
}
