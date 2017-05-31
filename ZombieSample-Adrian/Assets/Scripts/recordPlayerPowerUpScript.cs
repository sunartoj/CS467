using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordPlayerPowerUpScript : MonoBehaviour
{

    //used to handle when this object is destroyed
    public delegate void OnDestroyDelegate(MonoBehaviour instance);
    public event OnDestroyDelegate OnDestroyEvnt;

    void OnDestroy()
    {

        GameObject parent = this.transform.parent.gameObject;
        tileScript tl = parent.GetComponent<tileScript>();
        tl.isEmpty = true;

        print("Record Player destroyed");

        // Send notification that this object is about to be destroyed
        if (this.OnDestroyEvnt != null) this.OnDestroyEvnt(this);
    }
}
