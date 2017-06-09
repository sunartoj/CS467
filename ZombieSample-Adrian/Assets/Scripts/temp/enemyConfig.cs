using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class enemyConfig
{
    public int points;
    public int maxHealth;
    public bool canDropItem;
    public float chanceToDrop;

    public enemyConfig(int points, int maxhealth, bool candropitem, float chance)
    {
        this.points = points;
        this.maxHealth = maxhealth;
        this.canDropItem = candropitem;
        this.chanceToDrop = chance;
    }
}
