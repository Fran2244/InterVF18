using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrowPath : MonoBehaviour {

    [SerializeField] Material arrow;
    [SerializeField] float width = 1f;
    [SerializeField] float scrollSpeed = 0.5f;

    Vector2 offset = new Vector2(0, 0);

    List<GameObject> chemins = new List<GameObject>();
    List<LineRenderer> lines = new List<LineRenderer>();
    List<int> activeCount = new List<int>();

    private bool isActive = false;
    public bool IsActive {
        get { return isActive; }
        set { }
    }

    void Start () {
        WaveManager wm = GetComponent<WaveManager>();
        GameObject flèches = new GameObject(); flèches.transform.parent = transform; flèches.name = "Flèches";

        for (int i = 0; i < wm.paths.Length; i++) {
            GameObject chemin = new GameObject(); chemin.transform.parent = flèches.transform; chemin.name = "Chemin" + i;
            chemins.Add(chemin);
            activeCount.Add(0);

            Transform[] path = wm.paths[i].GetPath();
            for (int j = 1; j < path.Length; j++) {
                GameObject lineObject = new GameObject(); lineObject.transform.parent = chemin.transform; lineObject.name = "Ligne" + j;
                lineObject.transform.rotation = Quaternion.Euler(90, 0, 0);

                LineRenderer line = lineObject.AddComponent<LineRenderer>();
                line.shadowCastingMode = ShadowCastingMode.Off;
                line.receiveShadows = false;
                line.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                line.material = arrow;
                line.startWidth = width;
                line.endWidth = width;
                line.alignment = LineAlignment.Local;
                line.positionCount = 2;
                line.SetPosition(0, path[j - 1].position);
                line.SetPosition(1, path[j].position);

                lines.Add(line);
            }
            chemin.SetActive(false);
        }
    }
	
	void Update () {
        offset.x -= Time.deltaTime * scrollSpeed;
        for (int i = 0; i < lines.Count; i++)
            lines[i].material.SetTextureOffset("_MainTex", offset);
    }

    public void ShowPath(int path) {
        if (activeCount[path]==0)
            chemins[path].SetActive(true);
        activeCount[path]++;
    }

    public void HidePath(int path) {
        activeCount[path]--;
        if (activeCount[path] == 0)
            chemins[path].SetActive(false);
    }
}
