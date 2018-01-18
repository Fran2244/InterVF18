using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MenuFunc : MonoBehaviour {

    public GameObject credits;

    public void ToggleCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }
}
