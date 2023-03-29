using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class BulletMgr : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;
    [SerializeField]
    int numBullets = 10;
    
    Bullet[] bullets;

    private void Start()
    {
        bullets = new Bullet[numBullets];
        for (int i  = 0;  i< numBullets; i++)
        {
            bullets[i] = Instantiate<Bullet>(bulletPrefab);
            bullets[i].transform.parent = this.transform;
            bullets[i].gameObject.SetActive(false);
        }
    }

    public Bullet GetBullet()
    {
        foreach(var b in bullets)
        {
            if (!b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(true);
                return b;
            }
        }

        return null;
    }
}
