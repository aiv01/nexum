using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[DisallowMultipleComponent]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    public InputPlayer PlayerController_;

    private System.Action interaction;

    private InputAction interactButton;
    private void Awake()
    {
        PlayerController_ = new InputPlayer();
    }
    private void OnEnable()
    {
        interactButton = PlayerController_.Player.Interact;
        interactButton.Enable();
    }

    private void OnDisable()
    {
        interactButton.Disable();
    }

    private void Update()
    {
        if (PlayerController_.Player.Interact.triggered)
        {
            TryToInteract();
        }
    }
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
