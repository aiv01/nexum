using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class ActivatePause : MonoBehaviour
{
    [SerializeField]
    Canvas menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menu.gameObject.SetActive(true);
        }
    }
}
