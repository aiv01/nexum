using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[DisallowMultipleComponent]
public class LevelSelectMgr : MonoBehaviour
{
    [SerializeField]
    int numLevel;
    [SerializeField]
    bool[] levelUnlocked;

    [SerializeField]
    ButtonLevel[] Levels;
    private void Start()
    {
        for (int i = 0; i < numLevel; i++)
        {
            Levels[i].enabled = levelUnlocked[i];
        }
    }
}
