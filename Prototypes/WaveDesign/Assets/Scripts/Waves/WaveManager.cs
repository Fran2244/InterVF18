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
    Wave[] waves;

    [SerializeField]
    Path[] paths;

    int[] pathUsed;

    void Start()
    {
        pathUsed = new int[paths.Length];
        for(int i = 0; i < pathUsed.Length; ++i)
        {
            pathUsed[i] = 0;
        }
        for (int i = 0; i < waves.Length; ++i)
        {
            StartCoroutine(WaitAndSpawn(waves[i]));
        }

        //TODO: Build level progression
        LevelProgression.Instance.Build(waves);


    }

    IEnumerator WaitAndSpawn(Wave wave)
    {
        yield return new WaitForSeconds(wave.startTime);
        StartCoroutine(SpawnNextWave(wave));
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
