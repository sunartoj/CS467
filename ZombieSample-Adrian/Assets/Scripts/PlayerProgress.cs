using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProgress
{

    /// <summary>
    /// You CANNOT use properties when serializing in Unity.....they come out as blank. 
    /// </summary>
    public string name;
    public int highestLevel;
    public int highestScore;
}
