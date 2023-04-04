using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerEnterExit : MonoBehaviour
{
    [SerializeField]
    UnityEvent TriggerEnter;

    [SerializeField]
    UnityEvent TriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit.Invoke();
    }
}
