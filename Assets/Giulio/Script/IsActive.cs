using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
using System.Collections;

public enum NexumPlayer
{
    None = 0,
    Marie = 1,
    Starfire = 2,
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

    bool CannotSwitch = false;

    NexumPlayer currentPlayer = NexumPlayer.None;

    [SerializeField]
    private UnityEngine.Events.UnityEvent onPauseRequrested;

    private AudioMgr globalAudioMgr;

    private void Awake()
    {
        Key = new InputPlayer();



        RobotPlayerController = Robot.GetComponent<PlayerController>();
        RobotAnim = Robot.GetComponent<Animator>();
        EllenPlayerController = Ellen.GetComponent<PlayerController>();
        EllenAnim = Ellen.GetComponent<Animator>();

        globalAudioMgr = GameObject.Find("AudioMgr").GetComponent<AudioMgr>();
    }


    private void OnEnable()
    {
        SwitchCamera.Register(EllenCamera);
        SwitchCamera.Register(RobotCamera);

        if (currentPlayer == NexumPlayer.Starfire)
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

    public void ChangeSwitchState(bool state)
    {
        Debug.Log("Switch state:" + state);
        CannotSwitch = state;
    }
    private void Switch(InputAction.CallbackContext callbackContext)
    {
        
        if (SwitchCamera.IsActiveCam(EllenCamera))
        {
            if (Ellen.isGrounded && !CannotSwitch)
            {
                EllenPlayerController.enabled = false;
                EllenAnim.SetFloat("x", 0);
                EllenAnim.SetFloat("y", 0);
                RobotPlayerController.enabled = true;
                currentPlayer = NexumPlayer.Starfire;
                SwitchCamera.SwitchCam(RobotCamera);
            }
            else
                globalAudioMgr.CannotSwitch();
           
        }
        else if (SwitchCamera.IsActiveCam(RobotCamera))
        {
                RobotPlayerController.enabled = false;
                RobotAnim.SetFloat("x", 0);
                RobotAnim.SetFloat("y", 0);
                EllenPlayerController.enabled = true;
            currentPlayer = NexumPlayer.Marie;
                SwitchCamera.SwitchCam(EllenCamera);
        }
 
     }

    private void Pause(InputAction.CallbackContext callbackContext)
    {
        onPauseRequrested.Invoke();
        enabled = false;
    }
}
