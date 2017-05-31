using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
    GameManager gm;

    public Point GridPosition
    {
        get;
        private set;
    }

    public bool isEmpty { get; set; }

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2),
                transform.position.y + (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    // Use this for initialization
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Point gridPos, Vector3 worldPos)
    {
        isEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;

        LevelManagerScript.Instance.Tiles.Add(gridPos, this);
    }

    #region for placing a tower
    //see game manager script

    //public GameObject item
    //{
    //    get
    //    {
    //        if (transform.childCount > 0)
    //        {
    //            return transform.GetChild(0).gameObject;
    //        }
    //        return null;
    //    }
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    if (!item)
    //    {
    //        Debug.Log("I dropped it!");
    //    }

    //    if (isEmpty)
    //    {
    //        //PlaceTower();
    //    }
    //    else
    //    {
    //        Debug.Log("Tile is not empty!");
    //    }
    //}

    private void OnMouseOver()
    {
        //Debug.Log(GridPosition.X + " " + GridPosition.Y);

        if (Input.GetMouseButtonDown(0))
        {
            
            if (isEmpty)
            {
                PlaceTower();
            }
            else
            {
                Debug.Log("Tile is not empty!");
            }

        }
    }

    private void PlaceTower()
    {
        //Debug.Log("Placed a tower on: " +GridPosition.X + " " + GridPosition.Y);

        if (!EventSystem.current.IsPointerOverGameObject() && MenuClick.ClickedBtn != null)
        {
            //if this is a pill bottle
            if (MenuClick.ClickedBtn.TowerPrefab.tag == "PillBottle")
            {
                if (gm.pillBottleCount <= 0)
                {
                    BuyTower();
                    return;
                }
                else
                {
                    Debug.Log("Used a pill Bottle");
                    gm.pillBottleCount--;
                }

            }

            //if this is a record
            if (MenuClick.ClickedBtn.TowerPrefab.tag == "RecordPlayer")
            {
                if (gm.recordPlayerCount <= 0)
                {
                    BuyTower();
                    return;
                }
                else
                {
                    Debug.Log("Used a record");
                    gm.recordPlayerCount--;
                }

            }

            //if you have enough gold
            ninjaCtrl tl = MenuClick.ClickedBtn.TowerPrefab.GetComponent<ninjaCtrl>();
            if (tl != null)
            {
                if (gm.currGold < tl.cost)
                {
                    return;
                }

                gm.currGold -= tl.cost;
            }


            //THIS is actually placing the tower!!! Custom pivot point on prefab needs to be done. See video 5.1
            Vector3 temp = new Vector3(transform.position.x - .1f, transform.position.y - .45f, transform.position.z);    //I had to adjust as they were coming out of the middle
            GameObject tower = Instantiate(MenuClick.ClickedBtn.TowerPrefab, temp, Quaternion.identity);

            tower.layer = GridPosition.Y + 8;       //because my user layers start at 8

            //this is to make sure the towers ordering are correct when they overlap
            tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y + 2;

            //makes htis tower a child object of tile
            tower.transform.SetParent(transform);

            isEmpty = false;
            gm.DislayScoreScore();

            BuyTower();

        }



        //tower.transform.localPosition = Vector3.zero;
    }

    public void BuyTower()
    {
        //prevents you from accidentally clicking another spot
        MenuClick.ClickedBtn = null;
    }

    #endregion

}
