using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class PistolTarget : MonoBehaviour
{
    [SerializeField]
    UnityEvent hit;

    bool alreadyHitten = false;

    private void Hit()
    {
        if (alreadyHitten) return;

        hit.Invoke();
        alreadyHitten = true;
        Debug.Log("Colpito");
    }
}
