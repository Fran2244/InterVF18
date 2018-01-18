using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrowPath : MonoBehaviour {

    [SerializeField] float scrollSpeed = 0.5f;

    Vector2 offset = new Vector2(0, 0);

    [SerializeField] Material arrow;

    List<LineRenderer> lines = new List<LineRenderer>();

    private bool isActive = false;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            if (isActive)
                ActivateArrows();
            else
                DeactivateArrow();
        }
    }

    void Start () {
        for (int i=1;i< transform.childCount; i++) {
            LineRenderer line = transform.GetChild(i).gameObject.AddComponent<LineRenderer>();
            line.shadowCastingMode = ShadowCastingMode.Off;
            line.receiveShadows = false;
            line.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            line.material = arrow;
            line.startWidth = 1;
            line.endWidth = 1;
            line.alignment = LineAlignment.Local;
            line.positionCount = 2;
            line.SetPosition(0, transform.GetChild(i-1).transform.position);
            line.SetPosition(1, transform.GetChild(i).transform.position);
            lines.Add(line);
        }
    }
	
	void Update () {
        offset.x -= Time.deltaTime * scrollSpeed;
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].material.SetTextureOffset("_MainTex", offset);
        }
    }

    void ActivateArrows()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].enabled = true;
        }
    }

    void DeactivateArrow()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].enabled = false;
        }
    }

}
