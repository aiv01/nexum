using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class NearInteractable : Interactable
{
    [SerializeField]
    UnityEvent interacted;

    [SerializeField]
    UnityEvent reset;

    [SerializeField]
    float resetTime;

    public override void Interact()
    {
        Debug.Log("Interacted");
        GetComponent<Collider>().enabled = false;
        interacted.Invoke();

        if (resetTime > 0)
            StartCoroutine(ResetAtTime());
    }
    IEnumerator ResetAtTime()
    {
        yield return new WaitForSeconds(resetTime);
        Debug.Log("Reset");
        reset.Invoke();
        GetComponent<Collider>().enabled = true;
    }


}
