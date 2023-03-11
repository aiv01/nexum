using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PatrolMovement : MonoBehaviour
{
    [SerializeField]
    private float movementDistance = 2;

    [SerializeField]
    private float idleTimer = 2.0f;
    private float currTimer = 0.0f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currTimer += Time.deltaTime;
        if(currTimer >= idleTimer)
        {
            PatrolMove();
            currTimer = 0;
        }
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            animator.SetBool("NearBase", true); //to stop walking animation
        }
        else
        {
            animator.SetBool("NearBase", false); //to start the walking animation
        }
    }

    private void PatrolMove()
    {
        transform.forward *= -1;
        agent.SetDestination(transform.localPosition + transform.forward * movementDistance);
    }
}
