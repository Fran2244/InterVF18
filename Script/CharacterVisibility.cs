using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterVisibility : MonoBehaviour {

    [SerializeField]
    GameObject visibleIcon;

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

    private void Update()
    {
        isChecked = false;
    }

}
