using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChomperHit : MonoBehaviour
{
    private Animator animator;
    private int hitAnimIndex;

    private void Start()
    {
        animator = GetComponent<Animator>();

        foreach (var param in animator.parameters)
        {
            if (param.name == "IsHit")
                hitAnimIndex = param.nameHash;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bulletComp;
        if(collision.gameObject.TryGetComponent<Bullet>(out bulletComp))
        {
            animator.SetTrigger(hitAnimIndex);
        }
    }
}
