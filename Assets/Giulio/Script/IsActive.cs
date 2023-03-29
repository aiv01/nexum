using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class IsActive : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ellen;
    [SerializeField] private CinemachineVirtualCamera Robot;
    [SerializeField] private CharacterController EllenController;
    [SerializeField] private CharacterController RobotController;
    [SerializeField] private PlayerController RbPlController;
    [SerializeField] private PlayerController EllenPLController;
    [SerializeField] private Animator EllenAnim;
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
            if(EllenController.isGrounded)
            {
              EllenPLController.enabled = false;
              EllenAnim.SetFloat("x", 0);
              EllenAnim.SetFloat("y", 0);
              RbPlController.enabled = true;
              SwitchCamera.SwitchCam(Robot);
            }    
           
        }
        else if (SwitchCamera.IsActiveCam(Robot))
        {
             RbPlController.enabled = false;
             RobotAnim.SetFloat("x", 0);
             RobotAnim.SetFloat("y", 0);
             EllenPLController.enabled = true;
             SwitchCamera.SwitchCam(Ellen);
           
        }
 
     }
}
