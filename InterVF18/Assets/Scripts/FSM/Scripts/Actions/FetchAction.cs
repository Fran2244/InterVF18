using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Fetch")]
public class FetchAction : Action
{
    public override void ActEnter(StateController controller)
    {
        if (controller.target != null)
        {
            StateController targetState = controller.target.gameObject.GetComponent<StateController>();
            targetState.currentState = null;
            targetState.navMeshAgent.enabled = false;
            controller.fetchObj = controller.target.gameObject;
            controller.fetchObj.transform.SetParent(controller.gameObject.transform);

            controller.target = VisibilityManager.Instance.prisonPos;
            controller.navMeshAgent.speed = controller.navMeshAgent.speed * 2;
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
            controller.navMeshAgent.speed = controller.navMeshAgent.speed / 2;
            controller.fetchObj.transform.SetParent(controller.target.gameObject.transform);
        }
    }
}
