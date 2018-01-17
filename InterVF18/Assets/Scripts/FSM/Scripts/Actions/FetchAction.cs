using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Fetch")]
public class FetchAction : Action {

    public override void Act(StateController controller)
    {
        if (controller.wayPoints.Length > 0)
        {
            controller.navMeshAgent.destination = controller.target.position;
            controller.navMeshAgent.isStopped = false;

            //TODO: Implement grtabbing a spy and dropping to prison.
        }
    }
}
