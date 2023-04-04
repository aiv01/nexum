using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class RespawnZone : MonoBehaviour
{
    //[SerializeField]
    //Transform[] checkpoints;

    //public int currentCheckpoint = 0;

    //private void OnCollisionEnter(Collision collision)
    //{

    //    collision.transform.position = Vector3.up * 105;
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    other.GetComponent<CharacterController>().Move(Vector3.up * 1000);
    //}
    [SerializeField]
    UnityEngine.Events.UnityEvent Respawn;

    private void OnTriggerEnter(Collider other)
    {
        Respawn.Invoke();
    }
}
