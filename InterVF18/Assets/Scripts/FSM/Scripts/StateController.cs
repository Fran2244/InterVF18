using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour
{
    public State currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public GameObject fetchObj;

    public Transform target = null;
    public Transform eye;
    public bool isChasing = false;
    public bool hasDocuments;
    public GameObject documents;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (currentState == null)
        {
            return;
        }
        currentState.UpdateState(this);
	}

    public void TransitionToState(State nextState)
    {
        if (nextState != null && nextState != currentState)
        {
            currentState.StateExit(this);
            currentState = nextState;
            currentState.StateEnter(this);
        }
    }
    public void SetPath(Transform[] path)
    {
        wayPoints = path;
    }

}
