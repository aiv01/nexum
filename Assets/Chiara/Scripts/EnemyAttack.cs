using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
    [System.Serializable]
    public class AttackEvent : UnityEvent { }

    public AttackEvent attackEvent;

    [SerializeField]
    private float attackTimer = 3.0f;
    private float currTimer = 0.0f;

    private Transform target = null;

    private Animator animator;
    private int moveAnimIndex;
    private int attackAnimIndex;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackEvent.AddListener(Attack);

        foreach (var param in animator.parameters)
        {
            if (param.name == "IsMoving")
                moveAnimIndex = param.nameHash;
            if (param.name == "Attack")
                attackAnimIndex = param.nameHash;
        }
    }

    private void Update()
    {
        animator.SetBool(moveAnimIndex, false); //temporary, needs fixing
        currTimer += Time.deltaTime;
        if (currTimer >= attackTimer)
        {
            attackEvent?.Invoke();
            currTimer = 0.0f;
        }
    }

    private void Attack()
    {
        if(target != null)
        {
            transform.LookAt(target, Vector3.up);
            animator.SetTrigger(attackAnimIndex);
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


    private void OnEnable()
    {
        currTimer = attackTimer; //attacks as soon as target is in sight
        transform.LookAt(target, Vector3.up);
    }

    private void OnDisable()
    {
        target = null;
    }
}
