using UnityEngine.InputSystem;
using UnityEngine;
using ValvolaTest;

public class PlayerController : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField]  float playerSpeed = 2.0f;
    [SerializeField]  float SmoorhBlend = 0.1f;
    [SerializeField]  float sensitivity = 0.5f; 
    [SerializeField]  float turnSpeed = 10f;
    [SerializeField]  float groundDrag = 3f;

    [Header("Other")]
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animatorController;
    [SerializeField] Transform MainCamera;
    [SerializeField] LayerMask mask;

    [Header("InputPlayer")]
    InputPlayer PlayerController_;
    InputActionReference run;

    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] bool canIJump;
    [SerializeField] float ySpeed;

    private InputAction move_;
    private InputAction jump_;
    private InputAction run_;

    private Vector3 PlayerVelocity;
    private Vector3 DirectionTarget;
    private Vector3 move;
    private Vector2 movment;
    private Quaternion Rotation_;
    private float TurnSpeedMulti;
    bool ForwardUse = false;


    private void Awake()
    {
        PlayerController_ = new InputPlayer();
    }

    private void OnEnable()
    {
        move_ = PlayerController_.Player.Movment;
        jump_ = PlayerController_.Player.Jump;
        run_ = PlayerController_.Player.Run;

        run_.Enable();
        move_.Enable();
        jump_.Enable(); 
    }

    private void OnDisable()
    {
        move_.Disable();
        jump_.Disable(); 
        run_.Disable();
    }

    private void Start()
    {
        MainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        movment = move_.ReadValue<Vector2>();
        move = new Vector3(movment.x, 0, movment.y);
        ySpeed += (Physics.gravity.y * Time.deltaTime);

        UPDATE_Direction();

        if(isGrounded()) //jump
        {
            animatorController.SetBool("isGround", true);
            animatorController.SetBool("isJump", false);
            animatorController.SetBool("isFalling", false);
            ySpeed = 0f;

            if (PlayerController_.Player.Jump.triggered && canIJump)
            {
                ySpeed = jumpSpeed;
                animatorController.SetBool("isJump", true);
            }
        }
        else 
        {
            animatorController.SetBool("isGround", false);
            animatorController.SetBool("isJump", false);
            animatorController.SetBool("isFalling", true);
        }

        //if(PlayerController_.Player.Run.triggered)
        //{
        //    animatorController.SetBool("isRun", true);
        //} 


        if (movment != Vector2.zero && DirectionTarget.magnitude > 0.1f)
        {
            Vector3 lookDirection = DirectionTarget.normalized;
            Rotation_ = Quaternion.LookRotation(lookDirection, transform.up);
            var DifRotation = Rotation_.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (DifRotation < 0 || DifRotation > 0)
            {
                eulerY = Rotation_.eulerAngles.y;
            }

            var euler = new Vector3(0, eulerY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * TurnSpeedMulti * Time.deltaTime);
        }

        move = MainCamera.forward * move.z + MainCamera.right * move.x;
        move.y = ySpeed;


        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(PlayerVelocity * Time.deltaTime);

        animatorController.SetFloat("x", movment.x, SmoorhBlend, Time.deltaTime);
        animatorController.SetFloat("y", movment.y, SmoorhBlend, Time.deltaTime);
    }
    bool isGrounded()
    {

        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, .1f, mask);

    }
    

    void UPDATE_Direction()
    {
        Vector2 movment = move_.ReadValue<Vector2>();

        if (!ForwardUse)
        {
            TurnSpeedMulti = 1f;
            var forward = MainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            var right = MainCamera.transform.TransformDirection(Vector3.right);
            DirectionTarget = movment.x * right + movment.y * forward;
        }
        else
        {
            TurnSpeedMulti = 0.2f;
            var forward = transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            var right = transform.TransformDirection(Vector3.right);
            DirectionTarget = movment.x * right + Mathf.Abs(movment.y) * forward;
        }
    }
}
