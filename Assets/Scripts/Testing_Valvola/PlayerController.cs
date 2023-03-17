using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Collider myCollider;

    public bool isActivePlayer;
    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        isActivePlayer = false;
    }
    public void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private System.Action interaction;

    public void TryToInteract()
    {
        if (interaction == null) return;

        if (interaction.GetInvocationList().Length > 0)
        {
            interaction.Invoke();
            interaction = () => { };
        }
    }
    public void AddInteraction(Interactable other)
    {
        interaction += other.Interact;
    }

    public void RemoveInteraction(Interactable other)
    {
        interaction -= other.Interact;
    }

}
