using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Patrol")]
public class PatrolDecision : Decision
{
    [SerializeField] private float distance = 3.0f;

    public override bool Decide(StateController controller)
    {

        return ((controller.target.position - controller.transform.position).sqrMagnitude <= (distance * distance));
    }
}
