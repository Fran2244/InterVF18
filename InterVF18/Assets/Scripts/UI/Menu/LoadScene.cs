using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if (UNITY_EDITOR)
using UnityEditor.SceneManagement;
#endif

public class LoadScene : MonoBehaviour {

    [SerializeField]
    int sceneIndex;

    public void Load()
    {
        #if (UNITY_EDITOR)
                EditorSceneManager.LoadScene(sceneIndex);
        #else
                SceneManager.LoadScene(sceneIndex);
        #endif
    }
}
