using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class IsActive : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ellen;
    [SerializeField] private CinemachineVirtualCamera Robot;
    [SerializeField] private InputPlayer Key;
    [SerializeField] private PlayerController EllenController;
    [SerializeField] private PlayerController RobotController;
    [SerializeField] public bool IsOperativeEllen = true;
    [SerializeField] public bool IsOperativeRobot = false;

    private InputAction switchPlayer_;


    private void Awake()
    {
        Key = new InputPlayer();
    }

    private void OnEnable()
    {
        SwitchCamera.Register(Ellen);
        SwitchCamera.Register(Robot);

        switchPlayer_ = Key.Player.Switch;
        switchPlayer_.Enable();
        switchPlayer_.performed += Switch;
    }
    private void OnDisable()
    {
        switchPlayer_.Disable();

        SwitchCamera.UnRegister(Ellen);
        SwitchCamera.UnRegister(Robot);
    }

    private void Switch(InputAction.CallbackContext callbackContext)
    {
        if (SwitchCamera.IsActiveCam(Ellen) && IsOperativeEllen)
        {
            EllenController.enabled = false;
            RobotController.enabled = true;
            SwitchCamera.SwitchCam(Robot);
        }
        else if (SwitchCamera.IsActiveCam(Robot))
        {
            EllenController.enabled = true;
            RobotController.enabled = false;
            SwitchCamera.SwitchCam(Ellen);
        }
        else
        {
            Debug.Log("Non riesco a cambiare telecamera con Ellen o il robot");
        }
     }
}
