using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private float maxTargetMovement = 1;
    private float sqrTargetMovement;
    private Vector3 prevTargetPos;

    private Transform target = null;

    private NavMeshAgent agent;
    private Animator animator;
    private int moveAnimIndex;

    private float setDestinationTimer = 0.25f;
    private float currTimer = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        foreach (var param in animator.parameters)
        {
            if (param.name == "IsMoving")
                moveAnimIndex = param.nameHash;
        }

        sqrTargetMovement = maxTargetMovement * maxTargetMovement;
    }

    private void Update()
    {
        currTimer += Time.deltaTime;
        if(currTimer >= setDestinationTimer)
        {
            if((prevTargetPos - target.position).sqrMagnitude > sqrTargetMovement)
            {
                agent.SetDestination(target.position);
                prevTargetPos = target.position;
            }
        }
        animator.SetBool(moveAnimIndex, agent.velocity != Vector3.zero);
        Debug.Log("Update follow " + gameObject.name);
    }

    public void SetTarget(Transform newTarget, bool isGoal = false)
    {
        target = newTarget;
        prevTargetPos = target.position;

        if (isGoal)
        {
            agent.SetDestination(newTarget.position);
            agent.stoppingDistance = Random.Range(0.5f, 1f);
        }
    }

    private void OnEnable()
    {
        currTimer = 0;
    }

    private void OnDisable()
    {
        target = null;
    }
}
