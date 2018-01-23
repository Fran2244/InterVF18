using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	bool isPaused = false;
	bool isSwitching = false;
	[SerializeField]
	CanvasGroup panelPause;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SwitchPause ();
		}
	}


	public void SwitchPause(){
		if (!isSwitching) {
			if (isPaused) {
				Time.timeScale = 1.0f;
				isSwitching = true;
				StartCoroutine (HidePausePanel ());
				isPaused = !isPaused;
			} else {
				Time.timeScale = 0.001f;
				isSwitching = true;
				StartCoroutine (ShowPausePanel ());
				isPaused = !isPaused;
			}
		}
	}

	IEnumerator ShowPausePanel ()
	{
		float starttime = Time.unscaledTime;
		while (panelPause.alpha < 1.0f) {
			panelPause.alpha = Mathf.Lerp (0.0f, 1f, Time.unscaledTime - starttime);
			yield return null;
		}
		isSwitching = false;
		panelPause.interactable = true;
		panelPause.blocksRaycasts = true;
	}

	IEnumerator HidePausePanel ()
	{
		
		float starttime = Time.unscaledTime;
		while (panelPause.alpha > 0f) {
			panelPause.alpha = Mathf.Lerp (1.0f, 0.0f, Time.unscaledTime - starttime);
			yield return null;
		}
		isSwitching = false;
		panelPause.interactable = false;
		panelPause.blocksRaycasts = false;
	}


}
