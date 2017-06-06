using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBottlePowerUpScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            enemyHealth eh = other.gameObject.GetComponent<enemyHealth>();
            Debug.Log("A zombie entered with health: " + eh.currentHealth);
            eh.TakeDamage((int)(eh.currentHealth * 0.10));
            Debug.Log("Zombie health after: " + eh.currentHealth);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            //Debug.Log("A zombie exited");
        }
    }

    void OnDestroy()
    {
        print("Pill bottle destroyed");
        GameObject parent = this.transform.parent.gameObject;

        tileScript tl =  parent.GetComponent<tileScript>();
        tl.isEmpty = true;
    }
}
