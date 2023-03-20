using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class HoldableObject : Interactable
{
    private bool isHolding = false;

    private CharacterController player;

    private Rigidbody myRB;

    [SerializeField]
    private Vector3 holdOffset = Vector3.up * 2;

    [SerializeField]
    private Transform[] possiblePlayerLocation;
    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public override void Interact()
    {
        Debug.Log("interact");

        isHolding = !isHolding;

        /*if (isHolding)
        {
            float minDist = 50f;
            Transform realPosition = null;
            foreach (Transform t in possiblePlayerLocation)
            {
                var tDist = Vector3.Distance(player.transform.position, t.position);
                if (tDist < minDist)
                {
                    minDist = tDist;
                    realPosition = t;
                }
            }

            player.transform.position = realPosition.position;
        }*/
    }

    private void Update()
    {
        if (isHolding)
        {
            //myRB.velocity = player.velocity;
            transform.position = player.transform.position + holdOffset;
        }
    }

    protected override void OnCustomTriggerEnter(Collider other)
    {
        base.OnCustomTriggerEnter(other);
        if (player == null)
            player = other.GetComponent<CharacterController>();
    }

    protected override void OnCustomTriggerExit(Collider other)
    {
        if (!isHolding)
            player = null;
        base.OnCustomTriggerExit(other);
    }
}
