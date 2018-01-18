using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    public EnemyPathj[] paths;

    int[] pathUsed;

    int waveCount;
    int enemyCount;
    bool isStarted;

    ArrowPath arrowPath;

    void Start()
    {
        arrowPath = GetComponent<ArrowPath>();

        isStarted = false;
        waveCount = 0;
        enemyCount = 0;
        pathUsed = new int[paths.Length];
        for(int i = 0; i < pathUsed.Length; ++i)
        {
            pathUsed[i] = 0;
        }
        for (int i = 0; i < waves.Length; ++i)
        {
            StartCoroutine(WaitAndSpawn(waves[i]));
        }
        LevelProgression.Instance.Build(waves);


    }

    IEnumerator WaitAndSpawn(Wave wave)
    {
        yield return new WaitForSeconds(wave.startTime);
        StartCoroutine(SpawnNextWave(wave));
    }

    IEnumerator SpawnNextWave(Wave wave)
    {
        arrowPath.ShowPath(wave.pathIndex);
        waveCount++;
        for (int i = 0; i < wave.waveEnemies.Length; i++)
        {
            for (int j = 0; j < wave.waveEnemies[i].enemyCount; j++)
            {

                GameObject enemy = Instantiate(wave.waveEnemies[i].prefab);
                StateController controller = enemy.GetComponent<StateController>();
                controller.GetComponent<NavMeshAgent>().enabled = false;
                controller.wayPoints = paths[wave.pathIndex].GetPath();
                Transform[] path = paths[wave.pathIndex].GetPath();
                enemy.transform.position = path[0].position;
                controller.GetComponent<NavMeshAgent>().enabled = true;
                controller.GetComponent<MoneyValue>().moneyValue = (int)wave.resourcesKill;
                enemyCount++;
                yield return new WaitForSeconds(wave.spawnRate);
            }
        }
        arrowPath.HidePath(wave.pathIndex);
    }

}
