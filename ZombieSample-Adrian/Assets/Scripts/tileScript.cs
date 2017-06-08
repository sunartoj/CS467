using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
    GameManager gm;

    //these are going to be used so that when a tile is placed, other classes can react
    public delegate void OnTilePlaced(GameObject tower);
    public static event OnTilePlaced OnTilePlacedEvent;

    //this is for loading tower data from file,  requirement
    public towerConfig[] towerData = new towerConfig[3];

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

    public void Setup(Point gridPos, Vector3 worldPos)
    {
        isEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;

        LevelManagerScript.Instance.Tiles.Add(gridPos, this);
    }

    #region for placing a tower

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
            if (MenuClick.ClickedBtn.TowerPrefab.tag == "PillBottlePowerUp")
            {
                if (gm.pillBottleCount <= 0)
                {
                    BuyTower();
                    return;
                }
                else
                {
                    gm.pillBottleCount--;
                    Debug.Log("Used a pill Bottle: " + gm.pillBottleCount);
                    //BuyTower();
                }

            }

            //if this is a record
            if (MenuClick.ClickedBtn.TowerPrefab.tag == "RecordPlayerPowerUp")
            {
                if (gm.recordPlayerCount <= 0)
                {
                    BuyTower();
                    return;
                }
                else
                {
                    gm.recordPlayerCount--;
                    Debug.Log("Used a record: " + gm.recordPlayerCount);
                    //BuyTower();
                }

            }

            //loads individual tower data from files. Partof reqs
            //Thisis kinda clunky but works. 
            towerData = loadTowers.LoadTowers();

            ninjaCtrl tl = MenuClick.ClickedBtn.TowerPrefab.GetComponent<ninjaCtrl>();

            //if you have enough gold; super clunky
            if (tl != null)
            {
                foreach (var twr in towerData)
                {
                    if (twr.name == tl.tag)
                    {
                        if (gm.currGold < twr.cost)
                        {
                            return;
                        }

                        gm.currGold -= twr.cost;
                    }
                }

            }


            //THIS is actually placing the tower!!! Custom pivot point on prefab needs to be done. See video 5.1
            Vector3 temp = new Vector3(transform.position.x - .1f, transform.position.y - .45f, transform.position.z);    //I had to adjust as they were coming out of the middle
            GameObject tower = Instantiate(MenuClick.ClickedBtn.TowerPrefab, temp, Quaternion.identity);

            tower.layer = GridPosition.Y + 8;       //because my user layers start at 8

            //this is to make sure the towers ordering are correct when they overlap
            tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y + 2;

            //makes htis tower a child object of tile
            tower.transform.SetParent(transform);

            //super clunky too
            foreach (var twr in towerData)
            {
                if (twr.name == tower.tag)
                {
                    ninjaCtrl tmp = tower.GetComponent<ninjaCtrl>();
                    tmp.maxHealth = twr.maxHealth;
                }
            }



            isEmpty = false;
            gm.DislayScoreScore();

            BuyTower();


        }



        //tower.transform.localPosition = Vector3.zero;
    }

    public void BuyTower()
    {
        // Send notification that this object is about placed
        if (OnTilePlacedEvent != null)
            OnTilePlacedEvent(MenuClick.ClickedBtn.TowerPrefab);


        //prevents you from accidentally clicking another spot
        MenuClick.ClickedBtn = null;
    }

    #endregion

}
