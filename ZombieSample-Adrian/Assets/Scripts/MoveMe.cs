using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {

    public float initSpeed;

    public float currSpeed { get; set; }

	// Use this for initialization
	void Start () {
        currSpeed = initSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

    public void move()
    {
        transform.Translate(new Vector3(-currSpeed, 0, 0) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("zombie should stop");

        if (other.tag == "RecordPlayerPowerUp")  //recordPlayerPowerUp
        {
            print("Zombie stopped");
            currSpeed = 0;

            recordPlayerPowerUpScript rp = other.GetComponent<recordPlayerPowerUpScript>();
            rp.OnDestroyEvnt += Rp_OnDestroyHandler;
        }
    }

    private void Rp_OnDestroyHandler(MonoBehaviour instance)
    {
        Debug.Log("Record Player power up destroyed");
        currSpeed = initSpeed;
    }
}
