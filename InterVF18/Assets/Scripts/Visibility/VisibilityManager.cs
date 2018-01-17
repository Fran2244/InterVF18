using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour {

    #region Singleton
    static VisibilityManager _instance = null;
    public static VisibilityManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VisibilityManager>();
            }
            return _instance;
        }

    }
    #endregion

    [SerializeField]
    LayerMask detectionLayerMask;
    public LayerMask DetectionLayerMask
    {
        get
        {
            return detectionLayerMask;
        }
    }


    List<GameObject> visibleObjects;
    public void AddVisibleObject(GameObject obj)
    {
        if (!visibleObjects.Contains(obj))
            visibleObjects.Add(obj);
    }
    public void RemoveVisibleObject(GameObject obj)
    {
        if (visibleObjects.Contains(obj)) {
            visibleObjects.Remove(obj);
        }
    }


    public List<GameObject> VisibleObjects
    {
        get
        {
            return visibleObjects;
        }

    }

    private void Awake()
    {
        visibleObjects = new List<GameObject>();   
    }

    void LateUpdate()
    {
        foreach (GameObject enemy in ObjectManager.Instance.Enemies)
        {
            CharacterVisibility visibility = enemy.GetComponent<CharacterVisibility>();
            if (visibility.IsTracked)
            {
                visibility.IsVisible = true;
                visibility.isChecked = true;
            }
            else
            {
                visibility.IsVisible = false;
                visibility.isChecked = false;
                foreach (GameObject detect in ObjectManager.Instance.Detectors)
                {
                    if (!visibility.isChecked)
                    {
                        LineOfSight LoS = detect.GetComponent<LineOfSight>();
                        Vector3 rayCastDir = enemy.transform.position - detect.transform.position;
                        RaycastHit rCH;
                        if (Physics.Raycast(detect.transform.position, rayCastDir, out rCH, LoS.ViewDistance, detectionLayerMask))
                        {
                            if (rCH.transform == enemy.transform && Vector3.Angle(rayCastDir, detect.transform.forward) < LoS.ViewAngle * 0.5f)
                            {
                                visibility.IsVisible = true;
                                visibility.isChecked = true;
                                break;
                            }
                        }
                    }
                }
                if (visibility.isChecked)
                {
                    continue;
                }
            }
        }
    }

}
