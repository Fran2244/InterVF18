using UnityEngine;

public class ArrowPath : MonoBehaviour {

    [SerializeField] float scrollSpeed = 0.5f;

    Vector2 offset = new Vector2(0, 0);

    Material mat;

	void Start () {
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = transform.childCount;
        for (int i=0;i< transform.childCount; i++) {
            line.SetPosition(i, transform.GetChild(i).transform.position);
        }
        mat = line.material;
    }
	
	void Update () {
        offset.x -= Time.deltaTime * scrollSpeed;

        mat.SetTextureOffset("_MainTex", offset);

    }
}
