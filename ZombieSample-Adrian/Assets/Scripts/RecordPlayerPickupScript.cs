using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayerPickupScript : MonoBehaviour {

    GameManager gm;
    Button thisButton;

    // Use this for initialization
    void Start ()
    {
        gm = GameManager.instance;
        thisButton = this.GetComponent<Button>();

        //for when an item is picked up, enable button
        PickupItem.OnItemPickedUpEvent += PickupItem_OnItemPickedUpEvent;
    }

    private void PickupItem_OnItemPickedUpEvent(GameObject item)
    {

        if (item.tag == "RecordPlayer")
        {

            if (gm.recordPlayerCount > 0 )
            {
                thisButton.interactable = true;
            }
        }

        Destroy(item);
    }
}
