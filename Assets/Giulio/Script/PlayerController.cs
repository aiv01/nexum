using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float RotationSpeed = 4f;

    [SerializeField] private InputActionReference movmentController;
    [SerializeField] private InputActionReference jumpController;
    [SerializeField]private CharacterController controller;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform MainCamera;


    private void OnEnable()
    {
        movmentController.action.Enable();
        jumpController.action.Enable();
    }

    private void OnDisable()
    {
        movmentController.action.Disable();
        jumpController.action.Disable();
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
        Vector3 move = new Vector3(movment.x,0,movment.y);
        move = MainCamera.forward * move.z + MainCamera.right* move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (jumpController.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movment != Vector2.zero)
        {
            float targetangle = Mathf.Atan2(movment.x,movment.y) * Mathf.Rad2Deg + MainCamera.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f,targetangle,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
        }
    }
}
