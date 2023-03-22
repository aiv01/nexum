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

    [SerializeField]
    bool turnOnOff;

    bool isOn;

    public override void Interact()
    {
        if (turnOnOff)
        {
            if (!isOn)
            {
                interacted.Invoke();
            }
            else
            {
                reset.Invoke();
            }
            isOn = !isOn;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(ReEnableCollider());
            return;
        }


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

    IEnumerator ReEnableCollider()
    {
        yield return new WaitForSeconds(.01f);
        GetComponent<Collider>().enabled = true;

    }


}
