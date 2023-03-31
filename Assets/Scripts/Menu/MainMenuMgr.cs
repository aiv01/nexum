using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class MainMenuMgr : MonoBehaviour
{
    [SerializeField]
    Button New_Game_Btn;

    public bool[] Files;
    [SerializeField]
    Button[] Files_Btn;

    [SerializeField]
    Button Exit_Menu;


    [Header("Scene")]
    [SerializeField]
    int LevelSelectionScene;
    [SerializeField]
    int newGameScene;

    int firstAviableSlot;

    private void Start()
    {
        for(int i = 0;  i <3; i++)
        {
            Files_Btn[i].interactable = Exist_File(i);
        }

        firstAviableSlot = GetFirstAvaiableSlot();
        if (firstAviableSlot > 3)
            New_Game_Btn.interactable = false;

        Exit_Menu.onClick.AddListener(ExitGame);

        Debug.Log(firstAviableSlot + 1);
    }

    private int GetFirstAvaiableSlot()
    {
        for(int i = 0; i< Files.Length; i++)
        {
            if (!Files[i]) return i;
        }
        return Files.Length;
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void NewGame()
    {
        StaticValues.currentFile = firstAviableSlot;
        SceneManager.LoadScene(newGameScene);
    }

    public void SwitchToLevelScene(int btn_id)
    {
        StaticValues.currentFile = btn_id;
        SceneManager.LoadScene(LevelSelectionScene);
    }

    private bool Exist_File(int val)
    {
        return Files[val];
    }
}
