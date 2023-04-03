using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class PauseMenuMgr : MonoBehaviour
{
    [SerializeField]
    Canvas Menu;

    [SerializeField]
    InputPlayer gameInput;


    [SerializeField]
    UnityEvent Enable;
    [SerializeField]
    UnityEvent Disable;
    private void OnEnable()
    {
        Menu.gameObject.SetActive(true);
        Enable.Invoke();
        Time.timeScale = 0.0f;

    }
    private void OnDisable()
    {
        Menu.gameObject.SetActive(false);
        Disable.Invoke();
        Time.timeScale = 1.0f;
    }
}
