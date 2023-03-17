using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[DisallowMultipleComponent]
public class DoorOpenClose : MonoBehaviour
{
    enum DoorState
    {
        Opening,
        Closing,
        Idle
    }

    [SerializeField]
    bool isInverted = false;

    [SerializeField]
    Vector3 doorOpenLocation;

    [SerializeField]
    Transform innerDoor;

    [SerializeField]
    float doorSpeed = 5f;

    DoorState currentState = DoorState.Idle;

    private void Start()
    {
        if (isInverted) currentState = DoorState.Opening;
    }

    public void OpenDoor() {
        currentState = isInverted? DoorState.Closing: DoorState.Opening;
    }

    private void Update()
    {
        if (currentState == DoorState.Idle) return;

        switch (currentState)
        {
            case DoorState.Opening:
                innerDoor.localPosition = Vector3.MoveTowards(innerDoor.localPosition, doorOpenLocation, Time.deltaTime * doorSpeed);
                break;
            case DoorState.Closing:
                innerDoor.localPosition = Vector3.MoveTowards(innerDoor.localPosition, Vector3.zero, Time.deltaTime * doorSpeed);
                break;
        }

        if (innerDoor.localPosition == doorOpenLocation || innerDoor.localPosition == Vector3.zero)
            currentState = DoorState.Idle;
    }

    public void CloseDoor()
    {
        currentState = isInverted ? DoorState.Opening : DoorState.Closing;
    }
}
