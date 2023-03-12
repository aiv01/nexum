using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class ChangeMaterial : MonoBehaviour
{
    [SerializeField]
    Material newMaterial;

    Renderer myRenderer;
    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    public void Change()
    {
        myRenderer.sharedMaterial = newMaterial;
    }
}
