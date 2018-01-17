using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if (UNITY_EDITOR)
using UnityEditor.SceneManagement;
#endif
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    [SerializeField]
    int sceneIndex;

    [SerializeField]
    CanvasGroup imgFader;

    [SerializeField]
    CanvasGroup[] menuItems;

    GameObject rootObj;

    public void Load()
    {
        Transform root = transform.root;
        rootObj = root.gameObject;
        DontDestroyOnLoad(rootObj);
        StartCoroutine(LoadSceneFade());
    }

    IEnumerator LoadSceneFade()
    {
        float startTime = Time.time;
        imgFader.alpha = 0f;
        while (imgFader.alpha < 1.0f)
        {
            imgFader.alpha = Mathf.Lerp(0f, 1f, Time.time - startTime);
            yield return null;
        }
#if (UNITY_EDITOR)
        EditorSceneManager.LoadScene(sceneIndex);
#else
                SceneManager.LoadScene(sceneIndex);
#endif
        foreach(CanvasGroup obj in menuItems)
        {
            obj.alpha = 0f;
        }
        startTime = Time.time;
        while (imgFader.alpha > 0.0f)
        {
            imgFader.alpha = Mathf.Lerp(1f, 0f, Time.time - startTime);
            yield return null;
        }
        Destroy(rootObj);
    }

}
