using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillBottlePickupScript : MonoBehaviour
{

    GameManager gm;
    Button thisButton;

    void Start()
    {
        gm = GameManager.instance;
        thisButton = this.GetComponent<Button>();

        //for when an item is picked up, enable button
        PickupItem.OnItemPickedUpEvent += PickupItem_OnItemPickedUpEvent;
    }

    private void PickupItem_OnItemPickedUpEvent(GameObject item)
    {

        if (item.tag == "PillBottle")
        {
            thisButton.interactable = true;
        }
        Destroy(item);
    }
}
