using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class SimplestMover : MonoBehaviour
{

    [SerializeField]
    Transform pointHit;
    private void Update()
    {
        RaycastHit hitfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitfo, 8f, 8))
        {
            pointHit.transform.position = hitfo.point;
        }

        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.position += transform.up * Input.GetAxis("Vertical") * Time.deltaTime;
        
    }
}
