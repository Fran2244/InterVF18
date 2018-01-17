using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelProgression : MonoBehaviour {

    private static LevelProgression _instance = null;
    public static LevelProgression Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelProgression>();
            }
            return _instance;
        }
    }


    [SerializeField]
    GameObject content;

    [SerializeField]
    Color[] stickerColor;

    [SerializeField]
    GameObject prefabSticker;

    [SerializeField]
    float widthMultiplier;

    float totalWidth;
    float startTime;
    Vector2 startPos;
    RectTransform rectContent;

    public void Build(Wave[] waves)
    {
        rectContent = content.GetComponent<RectTransform>();
        int totalLastWaveEnemies = 0;
        for (int i = 0; i < waves[waves.Length - 1].waveEnemies.Length; ++i)
        {
            totalLastWaveEnemies += waves[waves.Length - 1].waveEnemies[i].enemyCount;
        }
        totalWidth = waves[waves.Length - 1].startTime + (totalLastWaveEnemies * waves[waves.Length - 1].spawnRate);
        int indexColor = 0;
        for (int i = 0; i < waves.Length; ++i)
        {
            GameObject sticker = Instantiate(prefabSticker, content.transform);
            ProgressSticker pS = sticker.GetComponent<ProgressSticker>();
            RectTransform rect = sticker.GetComponent<RectTransform>();
            int totalEnemy = 0;
            for (int j = 0; j < waves[i].waveEnemies.Length; ++j) {
                totalEnemy += waves[i].waveEnemies[j].enemyCount;
            }
            Vector2 v2 = rect.sizeDelta;
            v2 = new Vector2((totalEnemy * waves[i].spawnRate) * widthMultiplier, v2.y);
            rect.sizeDelta = v2;
            pS.Build(i, stickerColor[indexColor]);
            indexColor++;
            indexColor = indexColor % stickerColor.Length;
        }
        startTime = Time.time;
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }


    Vector2 newPos;
    private void Update()
    {
        float xPos = ((Time.time - startTime) / totalWidth) * (totalWidth * widthMultiplier);
        newPos = new Vector2(-xPos, rectContent.anchoredPosition.y);
        rectContent.anchoredPosition = newPos;
    }

}
