using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Coffre")]
public class CoffreAction : Action
{
    public override void ActEnter(StateController controller)
    {
        if (controller.target != null)
        {
            controller.documents = Instantiate(VisibilityManager.Instance.documents, controller.gameObject.transform);
            controller.target = VisibilityManager.Instance.getNearCoffre(controller.transform);
            controller.navMeshAgent.speed = controller.navMeshAgent.speed * 2;
            controller.hasDocuments = true;
        }
    }

    public override void Act(StateController controller)
    {
        if (controller.target != null)
        {
            controller.navMeshAgent.destination = controller.target.position;
            controller.navMeshAgent.isStopped = false;
        }
    }

    public override void ActExit(StateController controller)
    {
        if (controller.target != null)
        {
            controller.navMeshAgent.speed = controller.navMeshAgent.speed / 2;
            Destroy(controller.documents, 1.0f);
        }

        controller.hasDocuments = false;
    }
}
