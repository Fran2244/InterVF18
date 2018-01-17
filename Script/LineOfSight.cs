using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

    [SerializeField]
    float viewAngle;

    [SerializeField]
    float viewDistance;

    [SerializeField]
    Light spotLight;

    private void Start()
    {
        spotLight.spotAngle = viewAngle;
        spotLight.range = viewDistance;
    }

    public float ViewAngle
    {
        get
        {
            return viewAngle;
        }
    }
    
    public float ViewDistance
    {
        get
        {
            return viewDistance;
        }
    }


}
