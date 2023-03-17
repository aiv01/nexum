using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class BoxMovable : Interactable
{
    private bool isPushing = false;

    private PlayerController player;

    [SerializeField]
    private Transform[] possiblePlayerLocation;

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
            //TODO: Sposta il blocco
        }
    }

    //protected override void OnCustomTriggerEnter(Collider other)
    //{
    //    base.OnCustomTriggerEnter(other);
    //    player = other.GetComponent<PlayerController>();
    //}

    //protected override void OnCustomTriggerExit(Collider other)
    //{
    //    player = null;
    //    base.OnCustomTriggerExit(other);
    //}
}
