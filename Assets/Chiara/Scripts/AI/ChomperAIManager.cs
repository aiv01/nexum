using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(FollowTarget))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(PatrolMovement))]
public class ChomperAIManager : MonoBehaviour
{
    private FollowTarget follow;
    private EnemyAttack attack;
    private PatrolMovement patrol;

    private bool hasReachedGoal = false;
    private bool isFollowing = false;


    void Start()
    {
        patrol = GetComponent<PatrolMovement>();
        attack = GetComponent<EnemyAttack>();
        follow = GetComponent<FollowTarget>();

        //start in patrol
        follow.enabled = false;
        attack.enabled = false;
    }

    private void ReachTarget()
    {
        hasReachedGoal = true;

        patrol.enabled = false;
        attack.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasReachedGoal) return; //if at goal position no need to change state

        if(other.tag == "Goal")
        {
            if (other.gameObject.GetComponent<GoalAreaScript>().AddElement())
            {
                patrol.enabled = false;
                attack.enabled = false;
                follow.enabled = true;
                follow.SetTarget(other.transform, true);

                ReachTarget();
            }
            return;
        }

        if(other.tag == "Player")
        {
            patrol.enabled = false;
            follow.enabled = true;
            isFollowing = true;
            follow.SetTarget(other.transform);
        }
        if(other.tag == "Robot")
        {
            patrol.enabled = false;
            attack.enabled = true;
            attack.SetTarget(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (hasReachedGoal) return;
        attack.enabled = false;
        if (isFollowing) return;
        //from any state other than follow state return to patrol if not at goal position
        patrol.enabled = true;
        follow.enabled = false;
    }
}
