using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PatrolMovement : MonoBehaviour
{
    //[SerializeField]
    //private float movementDistance = 5;

    [SerializeField]
    private Transform[] patrolPoints;
    private int currPatrolIdx;

    [SerializeField]
    private float idleTimer = 4.0f;
    private float currTimer = 0.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private int moveAnimIndex;
    private bool isIdle = true;
    private bool patrolForward = true; //to know in which direction the patrol is going

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        foreach(var param in animator.parameters)
        {
            if(param.name == "IsMoving")
                moveAnimIndex = param.nameHash;
        }

        transform.position = patrolPoints[0].position;
        currPatrolIdx++;
    }

    void Update()
    {
        if (isIdle)
        {
            currTimer += Time.deltaTime;
        }
        if (currTimer >= idleTimer)
        {
            currTimer = 0;
            PatrolMove();
        }
        if (!isIdle)
        {
            isIdle = agent.remainingDistance < agent.stoppingDistance;
        }

        animator.SetBool(moveAnimIndex, agent.velocity != Vector3.zero);
    }

    private void PatrolMove()
    {
        isIdle = false;
        agent.SetDestination(patrolPoints[currPatrolIdx].position);
        if (patrolForward)
        {
            currPatrolIdx++;
        }
        else
        {
            currPatrolIdx--;
        }
        CheckPatrolPointIdx();
    }

    private void CheckPatrolPointIdx() //return true if moving forward on points array
    {
        if (patrolForward)
        {
            if(currPatrolIdx == patrolPoints.Length - 1)
            {
                patrolForward = false;
            }
            else
            {
                patrolForward = true;
            }
        }
        else
        {
            if(currPatrolIdx <= 0)
            {
                patrolForward = true;
            }
            else
            {
                patrolForward = false;
            }
        }
    }

    private void OnEnable()
    {
        currTimer = 0;
    }
    private void OnDisable()
    {
        if (moveAnimIndex == 0) return;
        animator.SetBool(moveAnimIndex, false);
    }
}
