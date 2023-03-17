using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float SmoorhBlend = 0.1f;
    [SerializeField] private float sensitivity = .5f; 
    [SerializeField] private float turnSpeed = 10f; 
   
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animatorController;
    [SerializeField] private Transform MainCamera;
    [SerializeField] private InputPlayer PlayerController_;

    private Animation Ani;
    private InputAction move_;

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
        Ani = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        move_ = PlayerController_.Player.Movment;
        move_.Enable();
    }

    private void OnDisable()
    {
        move_.Disable();
    }

    private void Start()
    {
        MainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        movment = move_.ReadValue<Vector2>();
        move = new Vector3(movment.x, 0, movment.y);
            
        UPDATE_Direction();
        if (movment != Vector2.zero && DirectionTarget.magnitude > 0.1f)
        {
            Vector3 lookDirection = DirectionTarget.normalized;
            Rotation_ = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = Rotation_.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = Rotation_.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * TurnSpeedMulti * Time.deltaTime);
        }

        move = MainCamera.forward * move.z + MainCamera.right* move.x;
        move.y = 0f;
    
        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(PlayerVelocity * Time.deltaTime);

         
        animatorController.SetFloat("x", movment.x, SmoorhBlend, Time.deltaTime);
        animatorController.SetFloat("y", movment.y, SmoorhBlend, Time.deltaTime);
  
        
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
