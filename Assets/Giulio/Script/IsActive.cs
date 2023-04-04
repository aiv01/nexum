using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
using System.Collections;

public enum NexumPlayer
{
    None = 0,
    Marie = 1,
    Robot = 2,
    Last
}

public class IsActive : MonoBehaviour
{
    [SerializeField] private CharacterController Ellen;
    [SerializeField] private CharacterController Robot;

    [SerializeField] private CinemachineVirtualCamera EllenCamera;
    [SerializeField] private CinemachineVirtualCamera RobotCamera;

    /*[SerializeField]*/ private PlayerController RobotPlayerController;
    /*[SerializeField]*/ private PlayerController EllenPlayerController;
    /*[SerializeField]*/ private Animator EllenAnim;
    /*[SerializeField]*/ private Animator RobotAnim;

    [SerializeField] private InputPlayer Key;

    private InputAction switchPlayer_;
    private InputAction pause_;

    [SerializeField]
    private UnityEngine.Events.UnityEvent onPauseRequrested;

    private NexumPlayer currentPlayer = NexumPlayer.None;

    private void Awake()
    {
        Key = new InputPlayer();

        RobotPlayerController = Robot.GetComponent<PlayerController>();
        RobotAnim = Robot.GetComponent<Animator>();
        EllenPlayerController = Ellen.GetComponent<PlayerController>();
        EllenAnim = Ellen.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SwitchCamera.Register(EllenCamera);
        SwitchCamera.Register(RobotCamera);
        if (currentPlayer == NexumPlayer.Robot)
            SwitchCamera.SwitchCam(RobotCamera);

        else
            SwitchCamera.SwitchCam(EllenCamera);

        switchPlayer_ = Key.Player.Switch;
        switchPlayer_.Enable();
        switchPlayer_.performed += Switch;

        pause_ = Key.Player.Pause;
        pause_.performed += Pause;
        pause_.Enable();
    }
    private void OnDisable()
    {
        switchPlayer_.Disable();

        SwitchCamera.UnRegister(EllenCamera);
        SwitchCamera.UnRegister(RobotCamera);

        pause_.performed -= Pause;
        pause_.Disable();
    }

    private void Switch(InputAction.CallbackContext callbackContext)
    {
        
        if (SwitchCamera.IsActiveCam(EllenCamera))
        {
            if(Ellen.isGrounded)
            {
              EllenPlayerController.enabled = false;
              EllenAnim.SetFloat("x", 0);
              EllenAnim.SetFloat("y", 0);
              RobotPlayerController.enabled = true;
              SwitchCamera.SwitchCam(RobotCamera);
                currentPlayer = NexumPlayer.Robot;
            }    
           
        }
        else if (SwitchCamera.IsActiveCam(RobotCamera))
        {
            //if (RobotController.isGrounded)
            //{
                RobotPlayerController.enabled = false;
                RobotAnim.SetFloat("x", 0);
                RobotAnim.SetFloat("y", 0);
                EllenPlayerController.enabled = true;
                SwitchCamera.SwitchCam(EllenCamera);
                currentPlayer = NexumPlayer.Marie;
            //}
           
        }
 
     }

    private void Pause(InputAction.CallbackContext callbackContext)
    {
        onPauseRequrested.Invoke();
        enabled = false;
    }
}
