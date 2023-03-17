using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
        OnCustomTriggerEnter(other);
    }
    protected virtual void OnCustomTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        other.SendMessage("AddInteraction", this, SendMessageOptions.DontRequireReceiver);

    }


    private void OnTriggerExit(Collider other)
    {
        OnCustomTriggerExit(other);
    }
    protected virtual void OnCustomTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        other.SendMessage("RemoveInteraction", this, SendMessageOptions.DontRequireReceiver);
    }
}
