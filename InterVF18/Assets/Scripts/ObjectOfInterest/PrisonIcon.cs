using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PrisonIcon : MonoBehaviour {

    [SerializeField] GameObject icon;
    [SerializeField] Light bgLight;
    Transform cam;
    float sinMultiplier = 1f;
    float lightMultiplier = 1f;
    float originalVetical; 
	
    void Start()
    {
        originalVetical = icon.transform.position.y;
        Transform cam = Camera.main.transform;
    }

	void Update ()
    {
        icon.transform.position = new Vector3(icon.transform.position.x, originalVetical += Mathf.Sin(Time.deltaTime) * sinMultiplier, icon.transform.position.z);
        bgLight.intensity += Mathf.Sin(Time.deltaTime) * lightMultiplier;
        icon.transform.LookAt(cam.position);
	}
}
