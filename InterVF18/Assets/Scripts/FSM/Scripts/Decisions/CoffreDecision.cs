using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Coffre")]
public class CoffreDecision : Decision
{
    [SerializeField] private float coffreDistance = 3.0f;

    public override bool Decide(StateController controller)
    {
        return ((controller.target.position - controller.transform.position).sqrMagnitude <= (coffreDistance * coffreDistance));
    }
}
