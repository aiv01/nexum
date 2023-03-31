
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using ValvolaTest;
using static Cinemachine.CinemachineFreeLook;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private CharacterController EllenController;
    [SerializeField] private CinemachineVirtualCamera AimCamOn;
    [SerializeField] private CinemachineVirtualCamera AimCamOff;

    private InputPlayer PlayerController_;
    private InputAction gunUp_;

    [SerializeField] Transform AimPos;
    [SerializeField] float Smooth;
    [SerializeField] float FOV;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera CamAim;

    bool True_;

    private void Awake()
    {
        PlayerController_ = new InputPlayer();
    }

    private void OnEnable()
    {
        SwitchCamera.Register(AimCamOn);
        SwitchCamera.Register(AimCamOff);

        gunUp_ = PlayerController_.Player.GunAim;
        gunUp_.Enable();

    }

    private void OnDisable()
    {
        gunUp_.Disable();

        SwitchCamera.UnRegister(AimCamOn);
        SwitchCamera.UnRegister(AimCamOff);
    }

    private void Update()
    {
        Input();

        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray_ = CamAim.ScreenPointToRay(ScreenCenter);
        
        if (Physics.Raycast(ray_,out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            AimPos.position = Vector3.Lerp(AimPos.position, hit.point, Smooth * Time.deltaTime);
        }
    }

    private void Input()
    {
        if(gunUp_.WasPressedThisFrame())
        {

            if (SwitchCamera.IsActiveCam(AimCamOff))
            {
                SwitchCamera.SwitchCam(AimCamOn);
                transform.rotation = AimPos.transform.rotation;
            }

        }
        else if (gunUp_.WasReleasedThisFrame() && SwitchCamera.IsActiveCam(AimCamOn))
        {

            SwitchCamera.SwitchCam(AimCamOff);
        }
    }
}
