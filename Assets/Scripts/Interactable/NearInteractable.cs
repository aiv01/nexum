using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class NearInteractable : MonoBehaviour
{
    [SerializeField]
    UnityEvent interacted;


    public void Activate()
    {
        Debug.Log("Interacted");
        GetComponent<Collider>().enabled = false;
        interacted.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        other.SendMessage("AddInteraction", this, SendMessageOptions.DontRequireReceiver);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        other.SendMessage("RemoveInteraction", this, SendMessageOptions.DontRequireReceiver);
    }
}
