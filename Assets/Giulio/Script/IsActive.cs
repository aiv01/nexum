using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class IsActive : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ellen;
    [SerializeField] private CinemachineVirtualCamera Robot;
    [SerializeField] private PlayerController EllenController;
    [SerializeField] private Animator EllenAnim;
    [SerializeField] private PlayerController RobotController;
    [SerializeField] private Animator RobotAnim;

    [SerializeField] private InputPlayer Key;

    private InputAction switchPlayer_;

    private void Awake()
    {
        Key = new InputPlayer();
    }

    private void OnEnable()
    {
        SwitchCamera.Register(Ellen);
        SwitchCamera.Register(Robot);
        SwitchCamera.SwitchCam(Ellen);

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
        if (SwitchCamera.IsActiveCam(Ellen))
        {
            EllenController.enabled = false;
            EllenAnim.SetFloat("x", 0);
            EllenAnim.SetFloat("y", 0);
            EllenAnim.SetBool("isGround", true);
            EllenAnim.SetBool("isJump", false);
            EllenAnim.SetBool("isFalling", false);
            RobotController.enabled = true;
            SwitchCamera.SwitchCam(Robot);
           
        }
        else if (SwitchCamera.IsActiveCam(Robot))
        {
            RobotController.enabled = false;
            RobotAnim.SetFloat("x", 0);
            RobotAnim.SetFloat("y", 0);
            EllenController.enabled = true;
            
            SwitchCamera.SwitchCam(Ellen);
        }
 
     }
}
