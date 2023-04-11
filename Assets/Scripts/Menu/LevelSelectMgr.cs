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
    ButtonLevel[] Levels;

    SaveScript save;
    private void Awake()
    {
        save = GameObject.Find("DontDestroyOnLoad").GetComponent<SaveScript>();
    }
    private void Start()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].enabled = false;

            if (i > 0 && !save.WasLevelCompleted(save.SaveFileNumber, i))            
                Levels[i].myState = LevelState.Unlocked;
            if (save.WasLevelCompleted(save.SaveFileNumber, i))
                Levels[i].myState = LevelState.Completed;
            //Debug.Log(save.WasLevelCompleted(save.SaveFileNumber, i + 1));
            Levels[i].enabled = true;
        }
    }

    public void LoadLevel(int level_id)
    {
        SceneManager.LoadScene(level_id);
    }

    public void DeleteSaveFile()
    {
        save.ResetSaveFile();
        SceneManager.LoadScene(0);
    }
}
