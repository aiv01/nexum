using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class NewInteractable : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interac");
    }
}
