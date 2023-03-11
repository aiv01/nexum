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
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animatorController;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform MainCamera;



    private void OnEnable()
    {
        movmentController.action.Enable();
    }

    private void OnDisable()
    {
        movmentController.action.Disable();
    }
    private void Start()
    {
        MainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 movment = movmentController.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movment.x,0,movment.y);
        Vector3 forward = transform.InverseTransformDirection(MainCamera.forward).normalized;
        Vector3 right = transform.InverseTransformDirection(MainCamera.right).normalized;

        move = MainCamera.forward * move.z + MainCamera.right* move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(playerVelocity * Time.deltaTime);


        Vector3 forwardRelativeInput = move.x * forward;
        Vector3 rightRelativeInput = move.y * right;
        Vector3 cameraRTElativeMovment = forwardRelativeInput + rightRelativeInput;


        transform.Translate(cameraRTElativeMovment);
        animatorController.SetFloat("x", movment.x,SmoorhBlend, Time.deltaTime);
        animatorController.SetFloat("y", movment.y, SmoorhBlend, Time.deltaTime);
    }
}
