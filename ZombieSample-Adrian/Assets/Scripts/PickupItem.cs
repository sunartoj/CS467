using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickupItem : MonoBehaviour
{

    public int value;

    private Text goldText;
    private Text scoreText;
    GameManager gm = GameManager.instance;

    //these are going to be used so that when an item is picked up (mainly for enabling/disabling button)
    public delegate void OnItemPickedUp(GameObject item);
    public static event OnItemPickedUp OnItemPickedUpEvent;

    private void Start()
    {
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Had an issue iwht detecting colliders with mousedown. So used raycast, I hope
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            //this gnores other layers except for "pickups"
            LayerMask mask = 1 << LayerMask.NameToLayer("Pickups");
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, mask);

            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                return;
            }

            if (hit)
            {
                string itemTag = hit.transform.tag;

                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "PillBottle")
                {
                    gm.pillBottleCount++;
                    Debug.Log("Picked up pill bottle: " + gm.pillBottleCount);

                }

                if (hit.transform.tag == "RecordPlayer")
                {
                    gm.recordPlayerCount++;
                    Debug.Log("Picked up record player: " + gm.recordPlayerCount);

                }

                gm.currGold += value;
                gm.DislayScoreScore();

                // Send notification that this object is about placed
                if (OnItemPickedUpEvent != null)
                    OnItemPickedUpEvent(hit.transform.gameObject);

            }
        }
    }

    /// <summary>
    /// This method does not work when there is another game object behind the item
    /// </summary>
    //void OnMouseDown()
    //{
    //    if (gameObject.tag == "PillBottle")
    //    {
    //        gm.pillBottleCount++;
    //        Debug.Log("Picked up pill bottle: " + gm.pillBottleCount);

    //    }

    //    if (gameObject.tag == "RecordPlayer")
    //    {
    //        gm.recordPlayerCount++;
    //        Debug.Log("Picked up record player: " + gm.recordPlayerCount);

    //    }

    //    // Send notification that this object is about placed
    //    if (OnItemPickedUpEvent != null)
    //        OnItemPickedUpEvent(gameObject);

    //    Destroy(gameObject, 0.15f);
    //    gm.currGold += value;
    //    DislayScoreScore();


    //}

}
