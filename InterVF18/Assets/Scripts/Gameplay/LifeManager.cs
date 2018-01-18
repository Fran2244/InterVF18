using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    CanvasGroup defeatPanel;

    [SerializeField]
    Text txtLifeLeft;

	// Use this for initialization
	void Start () {
        currentLife = startingLife;
        txtLifeLeft.text = currentLife.ToString() + " / " + startingLife.ToString();
	}
	
	public void LosaALife()
    {
        currentLife--;
        txtLifeLeft.text = currentLife.ToString() + " / " + startingLife.ToString();
        if (currentLife <= 0)
        {
            //TODO: Losing logic here.
           
            StartCoroutine(FadePanel(defeatPanel));
        }
    }

    IEnumerator FadePanel(CanvasGroup pan)
    {
        float startTime = Time.time;
        while (pan.alpha < 1.0f)
        {
            pan.alpha = Mathf.Lerp(0f, 1f, Time.time - startTime);
            yield return null;
        }
        pan.interactable = true;
        pan.blocksRaycasts = true;
        Time.timeScale = 0.0f;
    }

}
