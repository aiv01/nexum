using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class PlayerInteraction : MonoBehaviour
{
    private System.Action interaction;

    public void TryToInteract()
    {
        if (interaction == null) return;

        if (interaction.GetInvocationList().Length > 0)
        {
            interaction.Invoke();
            interaction = () => { };
        }
    }
    public void AddInteraction(Interactable other)
    {
        interaction += other.Interact;
    }
    public void RemoveInteraction(Interactable other)
    {
        interaction -= other.Interact;
    }
}
