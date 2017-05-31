using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {

    [SerializeField]
    private float initSpeed;

    public float currSpeed { get; set; }

	// Use this for initialization
	void Start () {
        currSpeed = initSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        move();
	}

    public void move()
    {
        transform.Translate(new Vector3(-currSpeed, 0, 0) * Time.deltaTime);
    }
}
