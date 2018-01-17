using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Fetch")]
public class FetchDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return (controller.target == null);
    }
}
