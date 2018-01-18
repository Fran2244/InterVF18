using System;
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

    public Transform[] prisons;
    public Transform[] coffres;
    [SerializeField] private State guardChaseState;
    [SerializeField] public GameObject documents;
    [SerializeField] public GameObject originalDocuments;

    [SerializeField]
    LayerMask detectionLayerMask;
    public LayerMask DetectionLayerMask
    {
        get
        {
            return detectionLayerMask;
        }
    }

    internal Transform getNearPrison(Transform transform)
    {
        if (prisons.Length > 0)
        {
            float nearPrison = float.MaxValue;
            Transform prison = null;
            for (int i = 0; i < prisons.Length; i++)
            {
                if (Mathf.Abs((transform.position - prisons[i].position).sqrMagnitude) < nearPrison)
                {
                    nearPrison = Mathf.Abs((transform.position - prisons[i].position).sqrMagnitude);
                    prison = prisons[i];
                }
            }

            return prison;
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    internal Transform getNearCoffre(Transform transform)
    {
        if (coffres.Length > 0)
        {
            float nearCoffre = float.MaxValue;
            Transform coffre = null;
            for (int i = 0; i < coffres.Length; i++)
            {
                if (Mathf.Abs((transform.position - prisons[i].position).sqrMagnitude) < nearCoffre)
                {
                    nearCoffre = Mathf.Abs((transform.position - prisons[i].position).sqrMagnitude);
                    coffre = coffres[i];
                }
            }

            return coffre;
        }
        else
        {
            throw new NotImplementedException();
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

    private void Update()
    {
        if (visibleObjects.Count > 0)
        {
            for (int i = 0; i < visibleObjects.Count; i++)
            {
                CharacterVisibility character = visibleObjects[i].GetComponent<CharacterVisibility>();
                if (character != null && !character.isChased)
                {
                    StateController guard = null;
                    float nearestGuardDist = float.MaxValue;
                    for (int j = 0; j < ObjectManager.Instance.Guards.Count; j++)
                    {
                        StateController checkGuard = ObjectManager.Instance.Guards[j].GetComponent<StateController>();
                        if (checkGuard != null && !checkGuard.isChasing)
                        {
                            float distance = Mathf.Abs((character.transform.position - checkGuard.transform.position).sqrMagnitude);
                            if (nearestGuardDist > distance)
                            {
                                guard = checkGuard;
                                nearestGuardDist = distance;
                            }

                        }
                    }
                    if (guard != null)
                    {
                        character.isChased = true;
                        guard.isChasing = true;
                        guard.target = character.transform;
                        guard.TransitionToState(guardChaseState);
                    }
                }
            }
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
