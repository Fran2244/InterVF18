using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressSticker : MonoBehaviour {

    [SerializeField]
    Text txtWaveEnemies;

    public void Build(int WaveNumber)
    {
        txtWaveEnemies.text = "Wave #" + (WaveNumber + 1).ToString();
    }


}
