using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Fetch")]
public class FetchDecision : Decision
{
    [SerializeField] private float fetchDistance = 1.0f;

    public override bool Decide(StateController controller)
    {

        return ((controller.target.position - controller.transform.position).sqrMagnitude <= (fetchDistance * fetchDistance));
    }
}
