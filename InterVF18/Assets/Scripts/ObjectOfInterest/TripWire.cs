using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWire : MonoBehaviour {

    [SerializeField]
    Transform firePoint;

    LineRenderer rend;

    RaycastHit rCH;

	void Start () {
        rend = GetComponent<LineRenderer>();
        rend.SetPosition(0, firePoint.position);
	}
	
	void Update () {
		if (Physics.Raycast(firePoint.position, firePoint.forward, out rCH, 10000f, VisibilityManager.Instance.DetectionLayerMask)){
            rend.SetPosition(1, rCH.point);
            if (rCH.transform.GetComponent<CharacterVisibility>())
            {
                rCH.transform.GetComponent<CharacterVisibility>().TrackCharacter(5f);
            }
        }
	}
}
