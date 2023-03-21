using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class PistolTarget : MonoBehaviour
{
    [SerializeField]
    UnityEvent hit;

    [SerializeField]
    UnityEvent reset;

    bool alreadyHitten = false;

    [SerializeField]
    float resetTime = 0f;
    private void Hit()
    {
        if (alreadyHitten) return;

        hit.Invoke();
        alreadyHitten = true;
        Debug.Log("Colpito");

        if (resetTime > 0)
            StartCoroutine(ResetAtTime());
    }

    IEnumerator ResetAtTime()
    {
        yield return new WaitForSeconds(resetTime);
        Debug.Log("Reset");
        reset.Invoke();
        alreadyHitten = false;
    }

}
