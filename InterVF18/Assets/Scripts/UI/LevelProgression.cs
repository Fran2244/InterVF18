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
            if (i < waves.Length - 1)
            {
                GameObject sticker = Instantiate(prefabSticker, content.transform);
                ProgressSticker pS = sticker.GetComponent<ProgressSticker>();
                RectTransform rect = sticker.GetComponent<RectTransform>();
                Vector2 v2 = rect.sizeDelta;
                v2 = new Vector2(waves[i+ 1].startTime - waves[i].startTime, v2.y);
                rect.sizeDelta = v2;
                pS.Build(i, stickerColor[indexColor]);
                indexColor++;
                indexColor = indexColor % stickerColor.Length;
            } else
            {
                GameObject sticker = Instantiate(prefabSticker, content.transform);
                ProgressSticker pS = sticker.GetComponent<ProgressSticker>();
                RectTransform rect = sticker.GetComponent<RectTransform>();
                Vector2 v2 = rect.sizeDelta;
                v2 = new Vector2(100000f, v2.y);
                rect.sizeDelta = v2;
                pS.Build(i, stickerColor[indexColor]);
                indexColor++;
                indexColor = indexColor % stickerColor.Length;
            }
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
