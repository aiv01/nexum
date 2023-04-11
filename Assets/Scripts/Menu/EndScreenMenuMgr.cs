using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[DisallowMultipleComponent]
public class EndScreenMenuMgr : MonoBehaviour
{
    [SerializeField]
    int mainMenuID = 0;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuID);
    }
}
