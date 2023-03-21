using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[DisallowMultipleComponent]
public class SceneSwitcher : MonoBehaviour
{
    public int[] scenes;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        for (int i = 1; i <= scenes.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
                SceneManager.LoadScene(i - 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
