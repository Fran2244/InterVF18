using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterVisibility : MonoBehaviour {

    [SerializeField]
    GameObject visibleIcon;

    [SerializeField]
    GameObject detectionIndicator;

    public bool isChased = false;

    bool isTracked;
    public bool IsTracked
    {
        get
        {
            return isTracked;
        }
    }
    public void TrackCharacter(float trackingTime)
    {
        isTracked = true;
        StartCoroutine(StopTracking(trackingTime));
    }

    [HideInInspector] public bool isChecked;
    bool isVisible;
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        set
        {
            isVisible = value;
            //TODO: Add visibility logic here.
            visibleIcon.SetActive(isVisible);
            if (isVisible)
            {
                VisibilityManager.Instance.AddVisibleObject(gameObject);
            } else
            {
                VisibilityManager.Instance.RemoveVisibleObject(gameObject);
            }
        }
    }

    void Awake()
    {
        detectionIndicator.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        isChecked = false;
    }

    IEnumerator StopTracking(float timer)
    {
        yield return new WaitForSeconds(timer);
        isTracked = false;
    }


}
