using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public int value;

    private Text goldText;
    GameManager gm = GameManager.instance;

    //these are going to be used so that when an item is picked up (mainly for enabling/disabling button)
    public delegate void OnItemPickedUp(GameObject item);
    public static event OnItemPickedUp OnItemPickedUpEvent;

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

        if (gameObject.tag == "RecordPlayer")
        {
            gm.recordPlayerCount++;
            Debug.Log("Picked up record player: " + gm.recordPlayerCount);

        }

        // Send notification that this object is about placed
        if (OnItemPickedUpEvent != null)
            OnItemPickedUpEvent(gameObject);

        Destroy(gameObject);
        gm.currGold += value;
        DislayScoreScore();


    }

    void DislayScoreScore()
    {
        goldText.text = "Gold: " + gm.currGold;
    }
}
