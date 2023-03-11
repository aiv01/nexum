using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float SmoorhBlend = 0.1f;
    [SerializeField] private float sensitivity = .5f; 
   
    [SerializeField] private InputActionReference movmentController;
    [SerializeField] private InputActionReference lookController;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animatorController;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform MainCamera;


    private void OnEnable()
    {
        movmentController.action.Enable();
        lookController.action.Enable();
    }

    private void OnDisable()
    {
        movmentController.action.Disable();
        lookController.action.Disable();
    }
    private void Start()
    {
        MainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded; 

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movment = movmentController.action.ReadValue<Vector2>();
        Vector2 mousePos = lookController.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movment.x,0,movment.y);

        move = MainCamera.forward * move.z + MainCamera.right* move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(playerVelocity * Time.deltaTime);


        animatorController.SetFloat("x", movment.x,SmoorhBlend, Time.deltaTime);
        animatorController.SetFloat("y", movment.y, SmoorhBlend, Time.deltaTime);
    }
}
