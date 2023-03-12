using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
    [System.Serializable]
    public class AttackEvent : UnityEvent { }

    public AttackEvent attackEvent;

    [SerializeField]
    private Transform target; //temporary, modify to detect player on its own

    [SerializeField]
    private float targetDistance = 4.0f;
    private float sqrTargetDistance;

    [SerializeField]
    private float attackTimer = 3.0f;
    private float currTimer = 0.0f;

    private bool isAttacking = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackEvent.AddListener(Attack);
        sqrTargetDistance = targetDistance * targetDistance;
    }

    private void Update()
    {
        currTimer += Time.deltaTime;
        if(currTimer >= attackTimer)
        {
            if(isAttacking)
            {
                attackEvent?.Invoke();
            }
            currTimer = 0.0f;
        }
    }
    private void FixedUpdate()
    {
        if((transform.position - target.position).sqrMagnitude < sqrTargetDistance)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if(isAttacking)
            transform.LookAt(target, Vector3.up); //if too heavy remove/replace with lighter function
    }

    private void Attack()
    {
        if(target != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}
