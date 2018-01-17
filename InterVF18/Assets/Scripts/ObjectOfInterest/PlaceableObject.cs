using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class PlaceableObject : MonoBehaviour {

    Transform player = null;
    float sphereCastRadius = 0.5f;
    float maxCastDistance = 1f;
    float minSpaceToPlaceGuard = 1f;
    float distGuardFromPlayer = 0.5f;
    public bool objectPlaced = false;
    Vector3 camOffset = new Vector3(0f,0.3f,0f);
    List<RaycastHit> hitList = new List<RaycastHit>();
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
            hitList = Physics.SphereCastAll(player.position, sphereCastRadius, player.forward, maxCastDistance).ToList();
            hitList = hitList.OrderBy(x => Vector2.Distance(player.position, x.transform.position)).ToList();
            if (Vector3.Distance(hitList[0].transform.position, player.position) >= minSpaceToPlaceGuard)
            {
                if (objectCanBePlaced)
                {
                    transform.position = player.forward * distGuardFromPlayer;
                    transform.rotation = Quaternion.LookRotation(player.position - transform.position);
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
        else if(gameObject.tag == "Cam")
        {
            hitList = Physics.SphereCastAll(player.position, sphereCastRadius, player.forward, maxCastDistance, LayerMask.NameToLayer("Default")).ToList();
            foreach(RaycastHit hit in hitList)
            {
                if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Environement"))
                {
                    hitList.Remove(hit);
                }
            }
            if(hitList.Count == 0)
            {
                return false;
            }

            if (hitList.Count > 1)
            {
                if (objectCanBePlaced)
                {
                    hitList = hitList.OrderBy(x => Vector2.Distance(player.position, x.transform.position)).ToList();
                    transform.position = hitList[0].point;
                    transform.Translate(camOffset);
                    transform.rotation = Quaternion.LookRotation(hitList[0].normal + hitList[1].normal);
                    transform.SetParent(objectsOfInterestParent);
                    objectPlaced = true;
                }
                return true;
            }
            else
            {
                if (objectCanBePlaced)
                {
                    transform.position = hitList[0].point;
                    transform.rotation = Quaternion.LookRotation(hitList[0].normal);
                    transform.SetParent(objectsOfInterestParent);
                    objectPlaced = true;
                }
            }
            return true;
        }
        else if (gameObject.tag == "TripWire")
        {
            hitList = Physics.SphereCastAll(player.position, sphereCastRadius, player.forward, maxCastDistance, LayerMask.NameToLayer("Default")).ToList();
            foreach (RaycastHit hit in hitList)
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Environement"))
                {
                    hitList.Remove(hit);
                }
            }
            if (hitList.Count == 0)
            {
                return false;
            }

            if (hitList.Count > 1)
            {
                if (objectCanBePlaced)
                {
                    hitList = hitList.OrderBy(x => Vector2.Distance(player.position, x.transform.position)).ToList();
                    transform.position = hitList[0].point;
                    transform.rotation = Quaternion.LookRotation(hitList[0].normal + hitList[1].normal);
                    transform.SetParent(objectsOfInterestParent);
                    objectPlaced = true;
                }
                return true;
            }
            else
            {
                if (objectCanBePlaced)
                {
                    transform.position = hitList[0].point;
                    transform.rotation = Quaternion.LookRotation(hitList[0].normal);
                    transform.SetParent(objectsOfInterestParent);
                    objectPlaced = true;
                }
                return true;
            }
        }
        return false;
    }
}
