using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class PauseMenuMgr : MonoBehaviour
{

    [SerializeField]
    InputPlayer gameInput;

    [SerializeField]
    GameObject firstSelectedControl;

    [SerializeField]
    UnityEvent onPauseLeft;

    [SerializeField]
    Canvas Menu;

    GameObject selectedControl => UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

    private void Awake()
    {
        gameInput = new InputPlayer();
    }

    private void OnEnable()
    {
        Menu.gameObject.SetActive(true);

        SelectFirstControl();
        
        
        gameInput.Menu.Enable();

        gameInput.Menu.Cancel.performed += HandleCancelPressed;
        gameInput.Menu.Navigate.performed += HandleNevigationPerformed;
        Time.timeScale = .0f;
    }
    private void OnDisable()
    {
        Menu.gameObject.SetActive(false);

        Time.timeScale = 1.0f;
        gameInput.Menu.Disable();
    }
    private void HandleCancelPressed(InputAction.CallbackContext ctx)
    {
        onPauseLeft.Invoke();
        enabled = false;
    }

    private void HandleNevigationPerformed(InputAction.CallbackContext ctx)
    {
        if (
            (ctx.control.device is Gamepad || ctx.control.device is Keyboard) &&
            ctx.action.ReadValue<Vector2>().sqrMagnitude > .01f &&
            selectedControl == null
            )
            SelectFirstControl();
    }
    void SelectFirstControl()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelectedControl);
    }
}
