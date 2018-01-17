using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}
