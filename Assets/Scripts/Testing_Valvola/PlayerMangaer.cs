using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class PlayerMangaer : MonoBehaviour
{
    [SerializeField]
    PlayerController[] players;
    private int currentPlayerNum;

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

        if (Input.GetKeyDown(KeyCode.RightControl))
            SwitchPlayer();

        if (Input.GetKeyDown(KeyCode.E))
            currentPlayer.TryToInteract();
    }

    private void SwitchPlayer()
    {
        currentPlayer.isActivePlayer = false;
        currentPlayerNum += 1;
        currentPlayerNum = currentPlayerNum % players.Length;
        currentPlayer.isActivePlayer = true;
    }
}
