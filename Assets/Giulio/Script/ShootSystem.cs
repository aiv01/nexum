
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using ValvolaTest;
using static Cinemachine.CinemachineFreeLook;

public class ShootSystem : MonoBehaviour
{
    private CharacterController myCC;
    private PlayerController myPC;
    private Animator myAnim;

    //[SerializeField] private CharacterController EllenController;
    [SerializeField] private CinemachineVirtualCamera AimCamOn;
    [SerializeField] private CinemachineVirtualCamera AimCamOff;

    private InputPlayer PlayerInput_;
    private InputAction gunUp_;
    private InputAction gunShoot_;

    [SerializeField] Transform AimPos;
    [SerializeField] float Smooth;
    [SerializeField] float FOV;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera CamAim;
    [SerializeField] float maxAutoAimDistance = 5;


    [SerializeField] Transform gun;
    [SerializeField] BulletMgr MyBM;
    private void Awake()
    {
        PlayerInput_ = new InputPlayer();
        myPC = GetComponent<PlayerController>();
        myAnim = GetComponent<Animator>();
        myCC = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        SwitchCamera.Register(AimCamOn);
        SwitchCamera.Register(AimCamOff);

        gunUp_ = PlayerInput_.Player.GunAim;
        gunUp_.Enable();
        gunShoot_ = PlayerInput_.Player.GunShoot;
        gunShoot_.Enable();

    }

    private void OnDisable()
    {
        gunUp_.Disable();
        gunShoot_.Disable();

        SwitchCamera.UnRegister(AimCamOn);
        SwitchCamera.UnRegister(AimCamOff);
    }

    public float IKMax = .5f;
    public float IKIncrement = .5f;
    private float IKStr = 0f;

    private void Update()
    {
        Input();


        if (myPC.isShoot)
        {
            if (IKStr < IKMax)
                IKStr += IKIncrement * Time.deltaTime;

            Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray_ = CamAim.ScreenPointToRay(ScreenCenter);

            if (Physics.Raycast(ray_, out RaycastHit hit, maxAutoAimDistance, layerMask))
            {
                AimPos.position = Vector3.Lerp(AimPos.position, hit.point, Smooth * Time.deltaTime);
            }
            else
            {
                AimPos.position = Vector3.Lerp(AimPos.position, transform.position + Camera.main.transform.forward * maxAutoAimDistance, Smooth * Time.deltaTime);
            }

            var k = transform.TransformPoint(AimPos.position);
            //if (k.x * k.x > 25f)
                //k.x = 5f + Mathf.Sign(k.x);
            Debug.Log(k);

            //AimPos.position = transform.InverseTransformPoint(k);

            gun.transform.LookAt(AimPos.position);

        }
        else if (IKStr >= 0)
            IKStr -= IKIncrement * Time.deltaTime;

        IKStr = Mathf.Clamp(IKStr, 0, IKMax);

    }

    private void ShootGun() { Shoot(); }
    private void Shoot()
    {
        var b = MyBM.GetBullet();
        if (b == null) return;
        b.transform.position = gun.transform.position;
        b.transform.LookAt(AimPos.position);
        b.target = AimPos.position;
    }
    private void OnAnimatorIK(int layerIndex)
    {
        //return;
        //if (layerIndex != 1) return;

        //if (IKStr <= 0f)
        //{
        //    myAnim.SetLookAtWeight(0);
        //    myAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        //    myAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        //    return;
        //}


        myAnim.SetLookAtWeight(IKStr);
        myAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKStr);
        myAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, IKStr);
        myAnim.SetLookAtPosition(AimPos.position);
        myAnim.SetIKPosition(AvatarIKGoal.LeftHand, AimPos.position);
        myAnim.SetIKPosition(AvatarIKGoal.RightHand, AimPos.position);
    }
    private void Input()
    {
        if (gunUp_.WasPressedThisFrame())
        {

            if (SwitchCamera.IsActiveCam(AimCamOff))
            {
                SwitchCamera.SwitchCam(AimCamOn);
                transform.rotation = AimPos.transform.rotation;
                myPC.isShoot = true;
                IKStr = 0.01f;
                myAnim.SetBool("isAiming", true);


            }

        }
        if (!myPC.isShoot) return;

        if (gunShoot_.triggered) // shoot
        {
            myAnim.SetTrigger("isShoting");
        }

        if (gunUp_.WasReleasedThisFrame() && SwitchCamera.IsActiveCam(AimCamOn))
        {
            myPC.isShoot = false;
            myAnim.SetBool("isAiming", false);
            SwitchCamera.SwitchCam(AimCamOff);
        }
    }


    private void OnTakeOutGun() {
    }
    private void PutAwayGun()
    {
    }
}
