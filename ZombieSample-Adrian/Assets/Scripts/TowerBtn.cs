using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;

    public static TowerBtn instance = null;

    GameManager gm;

    Button thisButton;

    private void Start()
    {
        gm = GameManager.instance;

        //for when an item is placed on a tile (disable the button if no more of the items)
        tileScript.OnTilePlacedEvent += Ts_OnTilePlacedEvent;
        LevelManagerScript.OnLevelLoadedEvent += LevelManagerScript_OnLevelLoadedEvent;

        InitButtons();

    }

    private void LevelManagerScript_OnLevelLoadedEvent(int level)
    {
        InitButtons();
    }

    private void InitButtons()
    {
        if (gameObject.name == "CowgirlBtn")
        {
            if (gm.level >= 2  /*&& gm.currGold > towerPrefab.GetComponent<ninjaCtrl>().cost*/ )
            {
                //thisButton = GameObject.Find("CowgirlBtn").GetComponent<Button>();
                this.GetComponent<Button>().interactable = true;
            }
            else
            {
                this.GetComponent<Button>().interactable = false;
            }

        }

        else if (gameObject.name == "RobotBtn")
        {
            if (gm.level >= 3 /*&& gm.currGold > towerPrefab.GetComponent<ninjaCtrl>().cost*/)
            {
                //thisButton = GameObject.Find("CowgirlBtn").GetComponent<Button>();
                this.GetComponent<Button>().interactable = true;
            }
            else
            {
                this.GetComponent<Button>().interactable = false;
            }

        }
        else if (gameObject.name == "PillBottle")
        {
            if (gm.pillBottleCount > 0)
            {
                Debug.Log("Pill bottle count: " + gm.pillBottleCount);
                this.GetComponent<Button>().interactable = true;
            }
            else
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
        else if (gameObject.name == "RecordPlayerBtn")
        {
            if (gm.recordPlayerCount > 0)
            {
                Debug.Log("Record Player count: " + gm.recordPlayerCount);
                this.GetComponent<Button>().interactable = true;
            }
            else
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Ts_OnTilePlacedEvent(GameObject tower)
    {
        if (tower.name == "PillBottlePowerUp")
        {
            if (TowerPrefab.tag == "PillBottlePowerUp")
            {
                Debug.Log("Placed a pill bottle");

                if (gm.pillBottleCount <= 0)
                {
                    this.GetComponent<Button>().interactable = false;
                }
            }
        }

        else if (tower.name == "recordPlayerPowerUp")
        {
            Debug.Log("Placed a record player");

            if (TowerPrefab.tag == "RecordPlayerPowerUp")
            {
                if (gm.recordPlayerCount <= 0)
                {
                    this.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }

    }

}
