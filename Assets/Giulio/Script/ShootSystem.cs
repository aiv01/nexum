
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
    [SerializeField] Vector3 noHitOffset;
    [SerializeField] float maxAutoAimDistance = 5;


    [SerializeField] Transform gun;
    [SerializeField] BulletMgr MyBM;
    
    AudioMgr globalAudioMgr;


    private void Start()
    {
        globalAudioMgr = GameObject.Find("AudioMgr").GetComponent<AudioMgr>();
    }
    private void Awake()
    {
        PlayerInput_ = new InputPlayer();
        myPC = GetComponent<PlayerController>();
        myAnim = GetComponent<Animator>();
        myCC = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
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

        SwitchCamera.UnRegister(AimCamOff);
    }

    public float IKMax = .5f;
    public float IKIncrement = .5f;
    private float IKStr = 0f;

    private bool IncrementIK = false;


    private void Update()
    {
        Input();


        if (IncrementIK)
        {
            if (IKStr < IKMax)
                IKStr += IKIncrement * Time.deltaTime;

            Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray_ = Camera.main.ScreenPointToRay(ScreenCenter);

            if (Physics.Raycast(ray_, out RaycastHit hit, maxAutoAimDistance, layerMask))
            {
                AimPos.position = Vector3.Lerp(AimPos.position, hit.point, Smooth * Time.deltaTime);
            }
            else
            {
                AimPos.position = Vector3.Lerp(AimPos.position, transform.position + Camera.main.transform.forward * maxAutoAimDistance + noHitOffset, Smooth * Time.deltaTime);
            }


            var k = transform.InverseTransformPoint(AimPos.position).normalized;
            if (k.x * k.x > .25f || k.z < 0)
            {
                transform.Rotate(transform.up * Mathf.Sign(k.x) * 5f);
                //transform.forward = (Vector3.ProjectOnPlane(aimDirection, transform.up));
            }

            gun.transform.LookAt(AimPos.position);

        }
        else if (IKStr >= 0)
            IKStr -= IKIncrement * Time.deltaTime;

        IKStr = Mathf.Clamp(IKStr, 0, IKMax);

    }

    private void ShootGun() { Shoot();
        globalAudioMgr.Shoot();
    }
    private void Shoot()
    {
        var b = MyBM.GetBullet();
        if (b == null) return;
        b.transform.position = gun.transform.position;
        b.transform.LookAt(AimPos.position);
        b.ChangeTarget(AimPos.position);
    }
    private void OnAnimatorIK(int layerIndex)
    {


        myAnim.SetLookAtWeight(IKStr);
        myAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKStr);
        myAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, IKStr);
        myAnim.SetLookAtPosition(AimPos.position);
        myAnim.SetIKPosition(AvatarIKGoal.LeftHand, AimPos.position);
        myAnim.SetIKPosition(AvatarIKGoal.RightHand, AimPos.position);
    }
    private void Input()
    {
        if (!myPC.isGround) { Debug.Log("NOT GRD"); return; }

        if (gunUp_.WasPressedThisFrame())
        {
            if (SwitchCamera.IsActiveCam(AimCamOff))
            {
                SwitchCamera.SwitchCam(AimCamOn);
                //transform.rotation = AimPos.transform.rotation;
                AimPos.gameObject.SetActive(true);
                myPC.isShoot = true;
                IncrementIK = true;
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
                AimPos.gameObject.SetActive(false);
            IncrementIK = false;
            myAnim.SetBool("isAiming", false);
        }
    }

    private void ActivateIK() { }
    private void OnTakeOutGun() {
    }
    private void PutAwayGun()
    {
        SwitchCamera.SwitchCam(AimCamOff);
        myPC.isShoot = false;
    }
}
