using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour
{
    [System.Serializable]
    public class SaveEvent : UnityEvent { }

    public SaveEvent save;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            save?.Invoke();
        }
    }
}
