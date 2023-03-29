using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class GunOutInShoot : MonoBehaviour
{
    [SerializeField]
    Renderer gun;

    private void TakeOutGun()
    {
        gun.enabled = true;
        Debug.Log("Out");
    }

    private void PutAwayGun()
    {
        gun.enabled = false;
        Debug.Log("Away");

    }
    private void ShootGun()
    {
        Debug.Log("Shoot");
    }
}
