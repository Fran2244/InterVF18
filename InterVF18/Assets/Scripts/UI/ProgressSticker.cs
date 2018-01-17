using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressSticker : MonoBehaviour {

    [SerializeField]
    Text txtWaveEnemies;

    [SerializeField]
    Image imgColor;

    public void Build(int WaveNumber, Color color)
    {
        txtWaveEnemies.text = "Wave #" + WaveNumber.ToString();
        imgColor.color = color;
    }


}
