using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Flame : MonoBehaviour {

    Transform smoke;
    float groundHeight;

	void Start()
    {
        smoke = transform.Find("Smoke");
        groundHeight = GameObject.FindGameObjectWithTag("Ground").transform.position.y + 0.1f;
    }
	
	void Update ()
    {
        smoke.transform.position = new Vector3(smoke.transform.position.x, groundHeight, smoke.transform.position.z);
	}
}