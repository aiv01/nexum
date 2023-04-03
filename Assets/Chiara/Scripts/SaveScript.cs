using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[DisallowMultipleComponent]
public class SaveScript : MonoBehaviour
{
    static bool singleton = false;

    [SerializeField]
    private string[] scenes;
    [SerializeField]
    private string savePath = "SaveFile.Nexum";
    [SerializeField]
    private int totalLevels = 3;  //!!!level numbers start from 1 not 0!!!

    private int saveFileNumber;

    public int SaveFileNumber {
        get  { return saveFileNumber; }
        set { saveFileNumber = value < 0 ? 0 : value > 3 ? 3 : value; }
    
    }

    private int currSceneIdx = 0;
    private string[] saveValues;

    private void Awake()
    {
        if (singleton)
            Destroy(this);

        singleton = true;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!File.Exists(savePath))
        {
            saveValues = new string[3];
            for(int i = 0; i < 3; i++)
            {
                saveValues[i] = "0";
            }
            File.WriteAllLines(savePath, saveValues);
        }
        saveValues = File.ReadAllLines(savePath);
        GetStartSceneIdx();
        //Save();
        //Load();
    }

    //save file format: int file exists (0 = false), bool for each level (true if completed)
    public void Save()
    {
        //save file alredy has three lines
        if (!SaveFileExists(saveFileNumber)) //creates new saveFile (parser returns true)
        {
            saveValues[saveFileNumber] = "1,"; //file exists
            for (int i = 1; i <= totalLevels; i++)
            {
                saveValues[saveFileNumber] += false.ToString() + ',';
            }
        }
        else
        {
            saveValues[saveFileNumber] = "1,"; //file exists
            int i = 1;
            for(; i <= currSceneIdx; i++)
            {
                saveValues[saveFileNumber] += true.ToString() + ',';
            }
            for(; i <= totalLevels; i++)
            {
                saveValues[saveFileNumber] += false.ToString() + ',';
            }
        }

        File.WriteAllLines(savePath, saveValues);

        Debug.Log("Saved scene: " + currSceneIdx);
    }
    private void GetStartSceneIdx() //to get starting scene
    {
        string[] currSaveFileValues = saveValues[saveFileNumber].Split(',');

        for (int i = 1; i < currSaveFileValues.Length; i++)
        {
            if (currSaveFileValues[i] == "False")
            {
                currSceneIdx = i - 1;
                break;
            }
        }
    }
    public void Load()
    {
        //SceneManager.LoadScene(scenes[currSceneIdx]);
        //Debug.Log("Loaded scene: " + currSceneIdx);
    }
    public void ResetSaveFile() //call ONLY after SetSaveFileNumber()
    {
        saveValues[saveFileNumber] = "0"; //file does not exist yet
        File.WriteAllLines(savePath, saveValues);
    }

    public void LevelUp() //call when switching to next level
    {
        currSceneIdx += currSceneIdx < totalLevels ? 1 : 0; //avoid overflow
    //    Save();
    //    Load();
    }

    // Divenuto una proprietà
    //public void SelectSaveFileByNumber(int n)
    //{
    //    saveFileNumber = n;
    //}
    public bool SaveFileExists(int n)
    {
        int num;
        int.TryParse(saveValues[n].Split(',')[0], out num);
        if (num == 0)
            return false;
        else
            return true;
    }

    public bool WasLevelCompleted(int saveFile, int level) //!!!level numbers start from 1!!!
    {
        if (!SaveFileExists(saveFile)) return false;
        string[] values = saveValues[saveFile].Split(',');
        Debug.Log(values);
        bool completed = false;
        bool.TryParse(values[level], out completed);
        return completed;
    }

}
