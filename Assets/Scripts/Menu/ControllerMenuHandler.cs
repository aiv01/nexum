using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class ControllerMenuHandler : MonoBehaviour
{

    [SerializeField]
    InputPlayer gameInput;

    [SerializeField]
    GameObject firstSelectedControl;

    [SerializeField]
    UnityEvent onCancelPressed;

    GameObject selectedControl => UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;




    private void Awake()
    {
        gameInput = new InputPlayer();
    }

    private void OnEnable()
    {
        SelectFirstControl();
        gameInput.Menu.Enable();
        Cursor.lockState = CursorLockMode.None;
        gameInput.Menu.Cancel.performed += HandleCancelPressed;
        gameInput.Menu.Navigate.performed += HandleNevigationPerformed;
    }
    private void OnDisable()
    {
        gameInput.Menu.Cancel.performed -= HandleCancelPressed;
        Cursor.lockState = CursorLockMode.Locked;
        gameInput.Menu.Navigate.performed -= HandleNevigationPerformed;
        gameInput.Menu.Disable();
    }
    private void HandleCancelPressed(InputAction.CallbackContext ctx)
    {
        onCancelPressed.Invoke();
        enabled = false;
    }

    private void HandleNevigationPerformed(InputAction.CallbackContext ctx)
    {

        //myAS.PlayOneShot(changeOptionSound);
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
