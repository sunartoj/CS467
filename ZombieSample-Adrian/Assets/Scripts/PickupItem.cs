using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public int value;

    private Text goldText;
    GameManager gm = GameManager.instance;

    private void Start()
    {
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "PillBottle")
        {
            gm.pillBottleCount++;
            Debug.Log("Picked up pill bottle: " + gm.pillBottleCount);

        }

        Destroy(gameObject);
        gm.currGold += value;
        DislayScoreScore();


    }

    void DislayScoreScore()
    {
        goldText.text = "Gold: " + gm.currGold;
    }
}
