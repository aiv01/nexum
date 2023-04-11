using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    bool requireHeavyObject;

    [SerializeField]
    Transform plate;

    [SerializeField]
    float neutralPlateDepth;
    [SerializeField]
    float pressedPlateDepth;

    [SerializeField]
    UnityEvent onPressed;

    [SerializeField]
    UnityEvent onReleased;

    int _entityPressing;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Premuto by " + other.name);
        if (requireHeavyObject && other.GetComponent<IsHeavyObject>() == null) return;

        _entityPressing++;
        if (_entityPressing > 1) return;

        onPressed.Invoke();
        plate.localPosition = transform.up * pressedPlateDepth;
        //Debug.Log("pressed by " + other.name);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Rilascitao da " + other.name);
        if (requireHeavyObject && other.GetComponent<IsHeavyObject>() == null) return;

        _entityPressing--;
        if (_entityPressing > 0) return;

        onReleased.Invoke();
        plate.localPosition = transform.up * neutralPlateDepth;
        //Debug.Log("releaded by " + other.name);

    }

}
