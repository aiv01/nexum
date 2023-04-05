using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class PauseMenuMgr : MonoBehaviour
{
    [SerializeField]
    Canvas Menu;


    [SerializeField]
    InputPlayer gameInput;


    [SerializeField]
    UnityEvent Enable;
    [SerializeField]
    UnityEvent Disable;

    [SerializeField]
    int mainMenuSceneID;

    private void OnEnable()
    {
        Menu.gameObject.SetActive(true);
        Enable.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;

    }
    private void OnDisable()
    {
        Menu.gameObject.SetActive(false);
        Disable.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneID);
    }

    public void ReloadSCene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
