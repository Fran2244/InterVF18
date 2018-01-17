using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour
{
    public State currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;
    [HideInInspector] public int nextWayPoint;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        currentState.UpdateState(this);
	}

    public void TransitionToState(State nextState)
    {
        if (nextState != null && nextState != currentState)
        {
            currentState = nextState;
        }
    }
    public void SetPath(Transform[] path)
    {
        wayPoints = path;
    }

}
