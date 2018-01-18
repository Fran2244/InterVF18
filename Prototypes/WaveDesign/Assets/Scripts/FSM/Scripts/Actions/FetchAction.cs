﻿using UnityEngine;

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
            controller.fetchObj.GetComponent<Collider>().enabled = false;
            controller.fetchObj.transform.SetParent(controller.gameObject.transform);
            controller.fetchObj.transform.localPosition = new Vector3(0.0f, 0.5f, 2.2f); 

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
            //controller.fetchObj.transform.SetParent(controller.target.gameObject.transform);
            if (ObjectManager.Instance.Enemies.Remove(controller.fetchObj))
            {
                Destroy(controller.fetchObj, 1.0f);
            }
        }

        controller.isChasing = false;
    }
}