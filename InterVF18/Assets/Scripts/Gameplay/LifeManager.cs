using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
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

    [SerializeField]
    PostProcessingBehaviour ppBehavior;

    [SerializeField]
    PostProcessingProfile profile;


	// Use this for initialization
	void Start () {
        currentLife = startingLife;
        txtLifeLeft.text = currentLife.ToString() + " / " + startingLife.ToString();
        ColorGradingModel.Settings s = profile.colorGrading.settings;
        s.basic.saturation = 1f;
        profile.colorGrading.settings = s;
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



        float startTime = Time.unscaledTime;
        

        while (profile.colorGrading.settings.basic.saturation > 0f)
        {
            ColorGradingModel.Settings s = profile.colorGrading.settings;
            s.basic.saturation = Mathf.Lerp(1f, 0f,Time.unscaledTime - startTime);
            profile.colorGrading.settings = s;
            
            yield return null;
        }

        startTime = Time.unscaledTime;
        while (pan.alpha < 1.0f)
        {
            pan.alpha = Mathf.Lerp(0f, 1f, Time.unscaledTime - startTime);
            yield return null;
        }
        pan.interactable = true;
        pan.blocksRaycasts = true;
        Time.timeScale = 0.0f;

    }




}
