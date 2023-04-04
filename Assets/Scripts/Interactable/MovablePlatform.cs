using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class MovablePlatform : MonoBehaviour
{
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] float speed;

    bool isOpening = false;

    private void Update()
    {
        Vector3 mov = Vector3.zero;

        if (isOpening)
             mov = Vector3.MoveTowards(transform.localPosition, moveDirection, speed * Time.deltaTime);
        else
            mov = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);

        transform.localPosition = mov;

    }
    public void OpenC()
    {
        isOpening = true;
    }
    public void Close()
    {
        isOpening = false;
    }
}
