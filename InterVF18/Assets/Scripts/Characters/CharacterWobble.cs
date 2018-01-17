using UnityEngine;

public class CharacterWobble : MonoBehaviour {

    [SerializeField] float cycleTimeFixed = 4f;
    [SerializeField] float cycleTimeRandom = 2f;
    float cycleTime = 4f;
    float cyclePosition = 0f;
    bool cycleDirectionUp = true;

    float yInitial;
    [SerializeField] float yChangeFixed = 5;
    [SerializeField] float yChangeRandom = 0.1f;
    float yChange;

    void Start() {
        yInitial = transform.localPosition.y;
        CalculateChanges();
    }

    void CalculateChanges() {
        cycleTime = cycleTimeFixed + Random.Range(0f, cycleTimeRandom);
        cycleDirectionUp = !cycleDirectionUp;
        yChange = yChangeFixed + Random.Range(0f, yChangeRandom);
    }

    void Update() {
        cyclePosition += Time.deltaTime;
        while (cyclePosition >= cycleTime) {
            cyclePosition -= cycleTime;
            cycleDirectionUp = !cycleDirectionUp;
            CalculateChanges();
        }
        float y = yInitial + yChange * (1 - Mathf.Cos(2 * Mathf.PI * cyclePosition / cycleTime));

        Vector3 pos = transform.localPosition;
        pos.y = y;
        transform.localPosition = pos;
    }
}