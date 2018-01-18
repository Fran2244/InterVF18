using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour {

    #region Singleton
    private static ObjectManager _instance = null;
    public static ObjectManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<ObjectManager>();
            }
            return _instance;
        }
    }
    #endregion

    List<GameObject> guards;
    List<GameObject> enemies;
    List<GameObject> detectors;

    [SerializeField]
    CanvasGroup panelWon;

    public List<GameObject> Guards
    {
        get
        {
            return guards;
        }
    }
    public List<GameObject> Enemies
    {
        get
        {
            return enemies;
        }
    }
    public List<GameObject> Detectors
    {
        get
        {
            return detectors;
        }
    }

    void Awake()
    {
        guards = new List<GameObject>();
        enemies = new List<GameObject>();
        detectors = new List<GameObject>();
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    /// <summary>
    /// Add a detector the the detector list.
    /// </summary>
    /// <param name="detector">Must have a LineOfSight component or it wont be added.</param>
    public void AddDetector(GameObject detector)
    {
        if (!detectors.Contains(detector) && detector.GetComponent<LineOfSight>())
            detectors.Add(detector);
    }

    public void AddGuard(GameObject guard)
    {
        if (!guards.Contains(guard))
            guards.Add(guard);
    }

    public void RemoveGuard(GameObject guard)
    {
        if (guards.Contains(guard))
            guards.Remove(guard);
    }
    public void RemoveDetector(GameObject detector)
    {
        if (detectors.Contains(detector))
            detectors.Remove(detector);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
        if (enemies.Count == 0 && WaveManager.Instance.isFinished == true)
        {
            StartCoroutine(GameWon());
        }
    }
    IEnumerator GameWon()
    {
        float startTime = Time.unscaledTime;
        while (panelWon.alpha < 1.0f)
        {
            panelWon.alpha = Mathf.Lerp(0f, 1f, Time.unscaledTime - startTime);
            yield return null;
        }
        panelWon.interactable = true;
        panelWon.blocksRaycasts = true;
        Time.timeScale = 0.0f;
    }
}
