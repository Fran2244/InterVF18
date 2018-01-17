using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave {

    public float startTime;

    public float spawnRate;

    public float resourcesStart;

    public float resourcesKill;

    public int pathIndex;

    public WaveEnemies[] waveEnemies;
}

[System.Serializable]
public struct WaveEnemies
{
    public GameObject prefab;

    public int enemyCount;

}