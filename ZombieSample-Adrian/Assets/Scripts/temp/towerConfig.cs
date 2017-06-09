using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class towerConfig
{
    public string name;
    public int maxHealth;
    public int cost;


    public towerConfig(string name, int maxhealth, int cost)
    {
        this.name = name;
        this.maxHealth = maxhealth;
        this.cost = cost;
    }
}
