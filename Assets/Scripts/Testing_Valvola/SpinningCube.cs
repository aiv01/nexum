using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class SpinningCube : MonoBehaviour
{
    [SerializeField]
    Vector3 rotation;

    private void Update()
    {
        transform.Rotate(rotation);
    }
}
