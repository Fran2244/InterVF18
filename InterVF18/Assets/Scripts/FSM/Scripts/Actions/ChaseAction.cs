using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : Action {

    public override void ActEnter(StateController controller)
    {
        if (controller.target != null)
        {
            //controller.navMeshAgent.speed = controller.navMeshAgent.speed * 2;
        }
    }

    public override void Act(StateController controller)
    {
        if (controller.target != null)
        {
            controller.navMeshAgent.destination = controller.target.position;
            controller.navMeshAgent.isStopped = false;

            //TODO: Implement grtabbing a spy and dropping to prison.
        }
    }

    public override void ActExit(StateController controller)
    {
        if (controller.target != null)
        {
            //controller.navMeshAgent.speed = controller.navMeshAgent.speed / 2;
        }
    }
}
