using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int value;

    GameManager gm = GameManager.instance;

    void OnMouseDown()
    {
        Destroy(gameObject);
        gm.currGold += value;

        Debug.Log("Current gold:" + gm.currGold);
    }
}
