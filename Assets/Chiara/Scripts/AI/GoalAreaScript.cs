using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class GoalAreaScript : MonoBehaviour
{
    [System.Serializable]
    public class AllHaveReachedGoalAreaEvent : UnityEvent { }

    public AllHaveReachedGoalAreaEvent areaIsFullEvent;

    [SerializeField]
    private int fullNumber = 3; //how many elements are required to fill goal area and invoke event
    private int currElementNumber = 0;

    public bool AddElement()
    {
        if (currElementNumber == fullNumber) return false; //if already full

        currElementNumber++;
        CheckIfFull();
        return true;
    }

    private void CheckIfFull()
    {
        if(currElementNumber == fullNumber)
        {
            areaIsFullEvent?.Invoke();
        }
    }
}
