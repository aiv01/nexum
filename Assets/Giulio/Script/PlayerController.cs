using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField]  float playerSpeed = 2.0f;
    [SerializeField]  float SmoorhBlend = 0.1f;
    [SerializeField]  float sensitivity = 0.5f; 
    [SerializeField]  float turnSpeed = 10f;
    [SerializeField]  float groundDrag = 3f;
    bool isRunning = false;
    bool isShoot = false;

    [Header("Other")]
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animatorController;
    [SerializeField] Transform MainCamera;
    [SerializeField] LayerMask mask;

    [Header("InputPlayer")]
    InputPlayer PlayerController_;
    private InputAction move_;
    private InputAction jump_;
    private InputAction run_;
    private InputAction gunUp_;
    private InputAction gunShoot_;

    [Header("Jump")]
    [SerializeField] bool canIJump;
    [SerializeField] float jumpSpeed;
    [SerializeField] float ySpeed;

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
        gunUp_ = PlayerController_.Player.GunAim;
        gunShoot_ = PlayerController_.Player.GunShoot;

        run_.Enable();
        move_.Enable();
        jump_.Enable();
        gunUp_.Enable();
        gunShoot_.Enable();
    }

    private void OnDisable()
    {
        move_.Disable();
        jump_.Disable(); 
        run_.Disable();
        gunUp_.Disable();
        gunShoot_.Disable();
    }

    private void Start()
    {
        MainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        input();
        UPDATE_Direction();
    }

    void MovePlayer()
    {
        movment = move_.ReadValue<Vector2>();
        move = new Vector3(movment.x, 0, movment.y);
        ySpeed += Physics.gravity.y * Time.deltaTime;//gravity

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

        if(isRunning)
        {
            movment *= 2f;
        }

        if(isShoot)
        {
            movment *= 0f;
            move *= 0f;
            ySpeed*= 0f;
        }

        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(PlayerVelocity * Time.deltaTime);

        animatorController.SetFloat("x", movment.x, SmoorhBlend, Time.deltaTime);
        animatorController.SetFloat("y", movment.y, SmoorhBlend, Time.deltaTime);
    }

    void input()
    {

        if (isGrounded()) //jump
        {
            animatorController.SetBool("isGround", true);
            animatorController.SetBool("isJump", false);
            animatorController.SetBool("isFalling", false);
            ySpeed = 0f;

            if (jump_.triggered && canIJump)
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

        if (run_.IsPressed()) //run
        {
            isRunning = !isRunning;
        }

        if(gunUp_.IsPressed()) // aim
        {
            isShoot = true;
            animatorController.SetLayerWeight(1, 1);
            animatorController.SetBool("isAiming", true);

            if (gunShoot_.triggered) // shoot
            {
                animatorController.SetTrigger("isShoting");
            }
        }
        else
        {
            animatorController.SetLayerWeight(0, 0);
            isShoot = false;
            animatorController.SetBool("isAiming", false);
        }



    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position + transform.up * .1f, Vector3.down, .5f, mask) && ySpeed < 0;
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
