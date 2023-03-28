using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor;
[DisallowMultipleComponent]
public class RayCastGetColor : MonoBehaviour
{
    [SerializeField]
    Bullet bullet;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LanciaRaggio();
        }
    }
    public void LanciaRaggio()
    {
        //RaycastHit hitfo;
        //Physics.Raycast(transform.position, transform.forward, out hitfo);

        //Debug.Log(hitfo.collider.name);
        //Debug.Log(hitfo.textureCoord);
        var b = Instantiate<Bullet>(bullet);
        b.transform.position = transform.position;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);
        
    }
}
