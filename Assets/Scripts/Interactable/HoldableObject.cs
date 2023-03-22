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
    private Collider activationCollider;

    [SerializeField]
    private Transform dropZone;

    [SerializeField]
    private ParticleSystem visibleDropPosition;
    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        activationCollider = GetComponent<Collider>();
    }

    public override void Interact()
    {
        Debug.Log("interact");

        isHolding = !isHolding;
        activationCollider.enabled = false;
        StartCoroutine(ReactivateCollider());

        if (!isHolding)
        {
            Debug.Log("BUTTO PER TERRA LE COSE");
            transform.position = dropZone.position;
            visibleDropPosition.transform.position = Vector3.down * -10;
        }

        #region old
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
        #endregion

    }

    IEnumerator ReactivateCollider()
    {
        yield return new WaitForSeconds(.5f);
        activationCollider.enabled = true;
    }

    private void Update()
    {
        if (isHolding)
        {
            //myRB.velocity = player.velocity;
            transform.position = player.transform.position + holdOffset;
            transform.rotation = player.transform.rotation;

            RaycastHit hitfo;
            if (Physics.Raycast(dropZone.position, Vector3.down, out hitfo, 50f, layerMask: 8))
            {
                visibleDropPosition.transform.position = hitfo.point;
            }
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
