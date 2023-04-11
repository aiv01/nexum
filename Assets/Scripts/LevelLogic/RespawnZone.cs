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
    Transform[] objectToCkeck;

    Dictionary<Transform, Vector3> respawPositionDict;
    //UnityEngine.Events.UnityEvent Respawn;
    private void Awake()
    {
        respawPositionDict = new Dictionary<Transform, Vector3>();
    }

    private void OnEnable()
    {
        foreach (var t in objectToCkeck)
        {
            respawPositionDict[t] = t.position;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (respawPositionDict.ContainsKey(other.transform))
        {
            Debug.Log(respawPositionDict[other.transform]);
            var otherCC = other.GetComponent<CharacterController>();
            if (otherCC == null)
            {
                other.transform.position = respawPositionDict[other.transform];
            }

            else
            {
                otherCC.enabled = false;
                other.transform.position = respawPositionDict[other.transform];
                otherCC.enabled = true;
            }
        }
    }
}
