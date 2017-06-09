using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class loadTowers : MonoBehaviour {

    private static string enemyDataFileName = "towerConfig.json";

    public static towerConfig[] LoadTowers()
    {
        //this does not work with webgl
        //string filePath = Path.Combine(Application.streamingAssetsPath, enemyDataFileName);

        TextAsset txt = Resources.Load("towerConfig") as TextAsset;

        string dataAsJson = txt.text;

        towerConfigArray en = new towerConfigArray();
        en = JsonUtility.FromJson<towerConfigArray>(dataAsJson);

        return en.towers;
    }
}
