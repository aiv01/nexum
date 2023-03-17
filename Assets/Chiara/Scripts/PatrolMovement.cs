using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PatrolMovement : MonoBehaviour
{
    [SerializeField]
    private float movementDistance = 5;

    [SerializeField]
    private float idleTimer = 4.0f;
    private float currTimer = 0.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private int moveAnimIndex;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        foreach(var param in animator.parameters)
        {
            if(param.name == "IsMoving")
                moveAnimIndex = param.nameHash;
        }
    }

    void Update()
    {
        currTimer += Time.deltaTime;
        if (currTimer >= idleTimer)
        {
            PatrolMove();
            currTimer = 0;
        }

        animator.SetBool(moveAnimIndex, agent.velocity != Vector3.zero); 
    }

    private void PatrolMove()
    {
        transform.forward *= -1;
        agent.SetDestination(transform.localPosition + transform.forward * movementDistance);
    }

    private void OnEnable()
    {
        currTimer = 0;
    }
}
