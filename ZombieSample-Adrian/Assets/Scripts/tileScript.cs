using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileScript : MonoBehaviour
{

    public Point GridPosition
    {
        get;
        private set;
    }

    public bool isEmpty { get; private set; }

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

        //THIS is actually placing the tower!!! Custom pivot point on prefab needs to be done. See video 5.1
        Vector3 temp = new Vector3(transform.position.x - .1f, transform.position.y - .45f, transform.position.z);    //I had to adjust as they were coming out of the middle
        GameObject tower = Instantiate(GameManager.instance.TowerPrefab, temp, Quaternion.identity);

        tower.layer = GridPosition.Y + 8;       //because my user layers start at 8

        //this is to make sure the towers ordering are correct when they overlap
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y + 2;

        //makes htis tower a child object of tile
        tower.transform.SetParent(transform);

        isEmpty = false;

        //tower.transform.localPosition = Vector3.zero;
    }

    #endregion

}
