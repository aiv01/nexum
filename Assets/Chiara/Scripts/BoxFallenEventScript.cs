using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class BoxFallenEventScript : MonoBehaviour
{
    [System.Serializable]
    public class BoxFallenEvent : UnityEvent { }

    public BoxFallenEvent boxEvent;

    private void OnCollisionEnter(Collision collision)
    {
        boxEvent?.Invoke();
    }
}
