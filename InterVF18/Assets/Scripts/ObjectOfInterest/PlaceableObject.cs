using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class PlaceableObject : MonoBehaviour {

    Transform player = null;
    float forwardCastdeviation = 1f;
    float sphereCastRadius = 0.5f;
    float maxCastDistance = 4f;
    float maxCastDistanceTrip = 0.5f;
    float minSpaceToPlaceGuard = 3.75f;
    float maxDistToPlaceTrip = 3.75f;
    float distGuardFromPlayer = 0.5f;
    public bool objectPlaced = false;
    Vector3 camOffset = new Vector3(0f,0.5f,0f);
    List<RaycastHit> hitList = new List<RaycastHit>();
    List<RaycastHit> hitList2 = new List<RaycastHit>();
    Transform objectsOfInterestParent;

    void Start()
    {
        objectsOfInterestParent = GameObject.Find("MegaManager").transform.Find("ObjectsOfInterest").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool PlaceObject(bool objectCanBePlaced)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (gameObject.tag == "Guard")
        {
            RaycastHit hit;
            if (Physics.Raycast(player.position, player.forward, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Environement") && hit.distance > minSpaceToPlaceGuard)
                {
                    if (objectCanBePlaced)
                    {
                        objectPlaced = true;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else if(gameObject.tag == "Cam")
        {
            hitList2.Clear();
            hitList = Physics.SphereCastAll(player.position, sphereCastRadius, player.forward, maxCastDistance).ToList();
            foreach(RaycastHit hit in hitList)
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Environement"))
                {
                    hitList2.Add(hit);
                }
            }
            if(hitList2.Count == 0)
            {
                return false;
            }

            if (hitList2.Count > 1)
            {
                if (objectCanBePlaced)
                {
                    hitList2 = hitList2.OrderBy(x => hitList2[0].distance).ToList();
                    transform.position = hitList2[0].point;
                    transform.Translate(camOffset);
                    transform.rotation = Quaternion.LookRotation(hitList2[0].normal + hitList2[1].normal);
                    transform.SetParent(objectsOfInterestParent);
                    objectPlaced = true;
                }
                return true;
            }
            else
            {
                if (objectCanBePlaced)
                {
                    transform.position = hitList2[0].point;
                    transform.rotation = Quaternion.LookRotation(hitList2[0].normal);
                    transform.Translate(camOffset);
                    transform.SetParent(objectsOfInterestParent);
                    objectPlaced = true;
                }
                return true;
            }
        }
        else if (gameObject.tag == "TripWire")
        {
            RaycastHit hit;
            if(Physics.Raycast(player.position, player.forward, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Environement") && hit.distance < maxDistToPlaceTrip)
                {
                    if (objectCanBePlaced)
                    {
                        transform.position = hit.point;
                        transform.rotation = Quaternion.LookRotation(hit.normal);
                        transform.SetParent(objectsOfInterestParent);
                        objectPlaced = true;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
