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

    SaveScript save;

    private bool[] Files;
    private void Start()
    {
        save = GameObject.Find("DontDestroyOnLoad").GetComponent<SaveScript>();
        Files = new bool[Files_Btn.Length];
        ColorBlock cb = Files_Btn[0].colors;
        for(int i = 0;  i <3; i++)
        {
            Files[i] = save.SaveFileExists(i);

            if (!save.SaveFileExists(i))
            {

                cb.normalColor = new Color(.75f, .75f, .75f, .75f);
                Files_Btn[i].colors = cb;
            }
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

    public void ExitGame()
    {
        Application.Quit();
    }

    //private void NewGame()
    //{
    //    StaticValues.currentFile = firstAviableSlot;
    //    Debug.Log(firstAviableSlot);
    //    SceneManager.LoadScene(newGameScene);
    //}

    public void SwitchToLevelSceneWithID(int btn_id)
    {
        save.SaveFileNumber = btn_id;
        if (!Files[btn_id])
            save.Save();

        //save.Load();
        Debug.Log("SWDQDAD");
        SceneManager.LoadScene(LevelSelectionScene);
    }

}
