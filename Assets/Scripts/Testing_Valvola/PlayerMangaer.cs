using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ValvolaTest
{
    [DisallowMultipleComponent]
    public class PlayerMangaer : MonoBehaviour
    {
        [SerializeField]
        PlayerController[] players;
        private int currentPlayerNum;

        [SerializeField]
        CinemachineVirtualCamera aimCamera;

        [SerializeField]
        Transform targetTrasform;

        public bool ControlAim = false;

        PlayerController currentPlayer
        {
            get { return players[currentPlayerNum]; }
        }

        private void Start()
        {
            currentPlayerNum = 0;
            currentPlayer.isActivePlayer = true;
        }

        private void Update()
        {
            Vector3 moveDir = Vector3.zero;

            moveDir += transform.right * Input.GetAxis("Vertical") * -1;
            moveDir += transform.forward * Input.GetAxis("Horizontal");

            moveDir = moveDir.sqrMagnitude > 1 ? moveDir.normalized : moveDir;
            currentPlayer.Move(moveDir);

            currentPlayer.Rotate(Input.GetAxis("Horizontal_2" ));

            if (Input.GetKeyDown(KeyCode.RightControl))
                SwitchPlayer();

            if (Input.GetKeyDown(KeyCode.E))
                currentPlayer.TryToInteract();

            if (ControlAim)
            {
                if (Input.GetMouseButton(1))
                    Aim();
                else
                    aimCamera.Priority = 0;
            }
        }

        private void Aim()
        {
            aimCamera.Priority = 11;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Shoot");
                var ray = Camera.main.ViewportPointToRay(Vector2.one * .5f);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 5);

                RaycastHit hitFo;
                if (Physics.Raycast(ray, out hitFo, 10f))
                {
                    hitFo.collider.SendMessage("Hit", null, SendMessageOptions.DontRequireReceiver);
                }
            }

        }
        private void SwitchPlayer()
        {
            currentPlayer.isActivePlayer = false;
            currentPlayerNum += 1;
            currentPlayerNum = currentPlayerNum % players.Length;
            currentPlayer.isActivePlayer = true;
        }
    }
}