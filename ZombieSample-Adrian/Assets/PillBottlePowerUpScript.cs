using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBottlePowerUpScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            Debug.Log("A zombie entered");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            Debug.Log("A zombie exited");
        }
    }
}
