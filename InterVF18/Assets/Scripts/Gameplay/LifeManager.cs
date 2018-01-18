using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    private static LifeManager _instance = null;
    public static LifeManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<LifeManager>();
            return _instance;
        }
    }

    [SerializeField]
    int startingLife;

    int currentLife;

	// Use this for initialization
	void Start () {
        currentLife = startingLife;
	}
	
	public void LosaALife()
    {
        currentLife--;
        if (currentLife <= 0)
        {
            //TODO: Losing logic here.
        }
    }
}
