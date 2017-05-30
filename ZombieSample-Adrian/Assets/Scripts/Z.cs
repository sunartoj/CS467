using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : MonoBehaviour {

    public static TowerBtn ClickedBtn { get; set; }

    public void PickTower(TowerBtn towerBtn)
    {
        ClickedBtn = towerBtn;
        Debug.Log("I clicked :" + ClickedBtn.TowerPrefab);
       
    }


}
