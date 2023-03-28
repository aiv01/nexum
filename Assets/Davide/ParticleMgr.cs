using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;



[DisallowMultipleComponent]
public class ParticleMgr : MonoBehaviour
{
    [SerializeField]
    VisualEffect bulletHitParticle;
    [SerializeField]
    int bulletParticleAmount = 2;

    VisualEffect[] bulletParticles;
    private void Start()
    {
        bulletParticles = InstantiateParticleArray(bulletHitParticle, bulletParticleAmount);
    }

    public VisualEffect GetBulletParticle(float timeBeforeReset)
    {
        for (int i = 0; i< bulletParticleAmount; i++)
        {
            if (bulletParticles[i].enabled == false)
            {
                bulletParticles[i].enabled = true;
                IEnumerator coor = disableAfterTime(bulletParticles[i], timeBeforeReset);
                StartCoroutine(coor);
                return bulletParticles[i];
            }
        }

        return null;
    }


    IEnumerator disableAfterTime(VisualEffect particle, float time)
    {
        yield return new WaitForSeconds(time + Random.Range(.01f, .05f));

        particle.enabled = false;
    }

    private VisualEffect[] InstantiateParticleArray(VisualEffect vfx, int amount)
    {
        VisualEffect[] particles = new VisualEffect[amount];

        for (int i = 0; i < amount; i++)
        {
            particles[i] = Instantiate<VisualEffect>(vfx);
            particles[i].enabled = false;
            particles[i].transform.SetParent(this.transform);
        }
        return particles;
    }
}
