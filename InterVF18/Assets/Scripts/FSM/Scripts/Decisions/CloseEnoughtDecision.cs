using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/CloseEnoughtDecision")]
public class CloseEnoughtDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return ((controller.target.position - controller.transform.position).sqrMagnitude <= controller.closeEnoughtDistance * controller.closeEnoughtDistance);
    }
}
