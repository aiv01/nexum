using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class DestructibleWall : MonoBehaviour
{
    Rigidbody[] MyRBs;

    private void Awake()
    {
        MyRBs = GetComponentsInChildren<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (var rb in MyRBs)
            rb.AddExplosionForce(5, transform.position, 1);
        }
    }
}
