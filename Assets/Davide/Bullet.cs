using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    VisualEffect effect;

    [SerializeField]
    float n = 10;
    [SerializeField]
    float speed = 5f;

    Rigidbody MyRB;
    Collider myC;
    private void Awake()
    {
        MyRB = GetComponent<Rigidbody>();
        myC = GetComponentInChildren<Collider>();
        MyRB.velocity = transform.forward * speed;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colpito");
        var vfx =Instantiate<VisualEffect>(effect);
        var cp = collision.GetContact(0);

        RaycastHit hitfo;

        if (Physics.Raycast(myC.bounds.center, -cp.normal, out hitfo, 1))
        {
            Debug.Log(hitfo.textureCoord);

            Texture tex = collision.gameObject.GetComponent<Renderer>().material.mainTexture;
            
            var rt = RenderTexture.GetTemporary(tex.width, tex.height);

            Graphics.Blit(tex, rt, new Material(Shader.Find("Unlit/Texture")));

            var ics = hitfo.textureCoord[0] * tex.width;
            var ipsilon = hitfo.textureCoord[1] * tex.height;


            Texture2D finalTex = new Texture2D(rt.width, rt.height);

            RenderTexture.active = rt;

            finalTex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            var c = finalTex.GetPixel((int)ics, (int)ipsilon) * n;
            
            vfx.SetVector4("SurfaceColor", new Vector4(c.r, c.g, c.b, 1));
        }
        vfx.transform.position = cp.point;
        vfx.transform.forward = cp.normal;

        Destroy(gameObject);
    }
}
