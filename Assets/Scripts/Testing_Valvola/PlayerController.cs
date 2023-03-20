using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValvolaTest
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed = .05f;

        private CharacterController myCC;


        public bool isActivePlayer;
        private void Awake()
        {
            myCC = GetComponent<CharacterController>();
            isActivePlayer = false;
        }

        private System.Action interaction;

        public void Move(Vector3 direction)
        {
            myCC.SimpleMove(direction * speed);
        }

        public void Rotate(float angle)
        {
            transform.Rotate(Vector3.up * angle);
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
}
