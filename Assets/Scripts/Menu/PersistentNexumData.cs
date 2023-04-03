using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class PersistentNexumData : MonoBehaviour
{
    public int currentSaveValue = -1;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
