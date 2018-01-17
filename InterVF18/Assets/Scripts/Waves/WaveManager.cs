using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    #region Singleton
    private static WaveManager _instance = null;
    public static WaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WaveManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField]
    float startDelay;

    [SerializeField]
    float TimeBetweenWaves;

    [SerializeField]
    Wave[] waves;

    [SerializeField]
    Path[] paths;

    private int currentWave;

    void Start()
    {
        currentWave = 0;
        StartCoroutine(WaitAndSpawn(startDelay));
    }

    IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (currentWave < waves.Length)
        {
            StartCoroutine(SpawnNextWave(waves[currentWave]));
            currentWave++;
        } else
        {
            //TODO: Win current level.
        }

    }

    IEnumerator SpawnNextWave(Wave wave)
    {
        for (int i = 0; i < wave.waveEnemies.Length; i++)
        {
            for (int j = 0; j < wave.waveEnemies[i].enemyCount; j++)
            {
                GameObject enemy = Instantiate(wave.waveEnemies[i].prefab);
                enemy.GetComponent<StateController>().wayPoints = paths[wave.pathIndex].GetPath();
                Transform[] path = paths[wave.pathIndex].GetPath();
                enemy.transform.position = path[0].position;
                yield return new WaitForSeconds(wave.spawnRate);
            }
        }
    }

}
