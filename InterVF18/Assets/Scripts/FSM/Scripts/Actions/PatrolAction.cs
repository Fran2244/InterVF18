using UnityEngine;
using AI;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        if (controller.wayPoints.Length > 0)
        {
            controller.navMeshAgent.destination = controller.wayPoints[controller.nextWayPoint].position;
            controller.navMeshAgent.isStopped = false;

            if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
            {
                controller.nextWayPoint = (controller.nextWayPoint + 1);
                if (controller.nextWayPoint >= controller.wayPoints.Length - 1)
                    CleanObject(controller);
            }
        }
    }

    void CleanObject(StateController controller)
    {
        if (controller.hasDocuments)
        {
            LifeManager.Instance.LosaALife();
        }
    }
}
