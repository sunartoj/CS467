using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;

    GameManager gm;
    Button thisButton;

    private void Start()
    {
        gm = GameManager.instance;

        if (gameObject.name == "CowgirlBtn")
        {
            if (gm.level < 2 || gm.currGold < towerPrefab.GetComponent<ninjaCtrl>().cost)
            {
                //thisButton = GameObject.Find("CowgirlBtn").GetComponent<Button>();
                this.GetComponent<Button>().interactable = false;
            }
            else
            {
                this.GetComponent<Button>().interactable = true;
            }

        }

        if (gameObject.name == "RobotBtn")
        {
            if (gm.level < 3 || gm.currGold < towerPrefab.GetComponent<ninjaCtrl>().cost)
            {
                //thisButton = GameObject.Find("CowgirlBtn").GetComponent<Button>();
                this.GetComponent<Button>().interactable = false;
            }
            else
            {
                this.GetComponent<Button>().interactable = true;
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
