using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class DestroyAfterAnyButton : MonoBehaviour
{
    InputPlayer gameInput;

    private void Awake()
    {
        gameInput = new InputPlayer();
    }

    private void Update()
    {
        if (Input.anyKey){
            gameObject.SetActive(false);
        }
    }
}
