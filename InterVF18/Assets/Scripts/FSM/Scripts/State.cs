using UnityEngine;
using AI;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color gizmoColor = Color.gray;

    public void StateEnter(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].ActEnter(controller);
        }
    }

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

	private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

	private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decided = transitions[i].decision.Decide(controller);

            if (decided)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }

    public void StateExit(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].ActExit(controller);
        }
    }
}
