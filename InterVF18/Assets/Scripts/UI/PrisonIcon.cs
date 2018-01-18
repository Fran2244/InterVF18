using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PrisonIcon : MonoBehaviour {
    
    [SerializeField] Light bgLight;
    Vector3 dir = new Vector3();
    Transform cam;
    float sinMultiplier = 2.5f;
    float speed = 0.05f;
    float lightMultiplier = 1f;
    float originalVetical; 
	
    void Start()
    {
        originalVetical = transform.position.y;
        cam = Camera.main.transform;
    }

	void Update ()
    {
        transform.position = new Vector3(transform.position.x, originalVetical += Mathf.Sin(Time.time * sinMultiplier) * speed, transform.position.z);
        bgLight.intensity += Mathf.Sin(Time.time * sinMultiplier) * lightMultiplier;
        transform.LookAt(cam.position);
	}
}
