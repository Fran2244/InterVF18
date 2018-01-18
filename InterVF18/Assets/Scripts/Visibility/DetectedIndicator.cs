using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DetectedIndicator : MonoBehaviour {

    float rotationSpeed = 2f;
    Transform lowerRing;

	void Start ()
    {
        lowerRing = transform.Find("LowerRing");
        GetComponent<Renderer>().enabled = false;
	}

    void FixedUpdate()
    {
        transform.Rotate(transform.up, rotationSpeed);
        lowerRing.Rotate(lowerRing.up, -rotationSpeed);
    }
}
