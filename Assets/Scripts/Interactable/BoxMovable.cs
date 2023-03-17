using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class BoxMovable : Interactable
{
    private bool isPushing = false;

    private CharacterController player;

    private Rigidbody myRB;

    [SerializeField]
    private Transform[] possiblePlayerLocation;
    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public override void Interact()
    {
        Debug.Log("interact");

        isPushing = !isPushing;

        if (isPushing)
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

            player.transform.position = transform.position;
        }
    }

    private void Update()
    {
        if (isPushing)
        {
            myRB.velocity = player.velocity;
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
        if (!isPushing)
            player = null;
        base.OnCustomTriggerExit(other);
    }
}
