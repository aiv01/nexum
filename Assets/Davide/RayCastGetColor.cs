using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor;
using UnityEngine.InputSystem;
[DisallowMultipleComponent]
public class RayCastGetColor : MonoBehaviour
{
    [SerializeField]
    BulletMgr btMgr;

    InputPlayer playerController_;

    InputAction aim;
    InputAction shoot;
    private void Awake()
    {
        playerController_ = new InputPlayer();
    }
    private void Start()
    {
        btMgr = GameObject.Find("BulletMgr").GetComponent<BulletMgr>();
        aim = playerController_.Player.Aim;
        shoot = playerController_.Player.Shoot;

        aim.Enable();
        shoot.Enable();
    }

    private void Update()
    {
        if (aim.IsPressed())
        {
            Debug.Log("Aimando");
            if (shoot.triggered)
            {
                LanciaRaggio();
            }
        }
            
    }
    public void LanciaRaggio()
    {
        //RaycastHit hitfo;
        //Physics.Raycast(transform.position, transform.forward, out hitfo);

        //Debug.Log(hitfo.collider.name);
        //Debug.Log(hitfo.textureCoord);
        var b = btMgr.GetBullet();
        b.transform.position = transform.position;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);
        
    }
}
