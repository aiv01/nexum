using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float lifeSpan = 2f;


    Rigidbody MyRB;
    Collider myC;
    ParticleMgr particleMgr;

    public Vector3 target;
    
    private void Awake()
    {
        MyRB = GetComponent<Rigidbody>();
        myC = GetComponentInChildren<Collider>();
        particleMgr = GameObject.Find("ParticleMgr").GetComponent<ParticleMgr>();
        if (particleMgr == null)
            Debug.LogError("Manca ParticleMgr");
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(MaxLifespan));
    }


    private void Update()
    {
        //Vector3 moving = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //transform.position = moving;
    }
    public void ChangeTarget(Vector3 target)
    {
        //Debug.Log(transform.InverseTransformPoint(target)/*.normalized * speed*/);
        this.target = target;
        MyRB.velocity = ((target - transform.position).normalized * speed);
    }

    IEnumerator MaxLifespan()
    {
        yield return new WaitForSeconds(lifeSpan);

        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var vfx = particleMgr.GetBulletParticle(.5f);
        collision.gameObject.SendMessage("Hit", null, SendMessageOptions.DontRequireReceiver);
        if (vfx == null) { Debug.LogWarning("Manca Particella"); return; }
        var cp = collision.GetContact(0);


        /*if (collision.gameObject.GetComponent<Renderer>() == null) return;

            RaycastHit hitfo;

        if (Physics.Raycast(myC.bounds.center, -cp.normal, out hitfo, 1))
        {
            Texture tex = collision.gameObject.GetComponent<Renderer>().material.mainTexture;
            if (tex == null) return;
            var rt = RenderTexture.GetTemporary(tex.width, tex.height);

            Graphics.Blit(tex, rt, new Material(Shader.Find("Unlit/Texture")));

            var ics = hitfo.textureCoord[0] * tex.width;
            var ipsilon = hitfo.textureCoord[1] * tex.height;


            Texture2D finalTex = new Texture2D(rt.width, rt.height);

            RenderTexture.active = rt;

            finalTex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            var c = finalTex.GetPixel((int)ics, (int)ipsilon) * n;
            Debug.Log(c);
            vfx.SetVector4("SurfaceColor", new Vector4(c.r, c.g, c.b, 1));
        }*/
        vfx.transform.position = cp.point;
        vfx.transform.forward = cp.normal;

        gameObject.SetActive(false);
    }
}
