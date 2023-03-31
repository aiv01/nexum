using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[DisallowMultipleComponent]
public class SaveScript : MonoBehaviour
{
    [SerializeField]
    private string[] scenes;
    [SerializeField]
    private string savePath = "Assets/SaveFile.Nexum";

    private int currSceneIdx = 0;
    private string[] saveValues;

    public int saveFileNumber;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        saveValues = File.ReadAllLines(savePath);
        Save();
        Load();
    }

    //save file format: int Level
    public void Save()
    {
        //save file alredy has three lines

        saveValues[saveFileNumber] = currSceneIdx.ToString();
        File.WriteAllLines(savePath, saveValues);

        Debug.Log("Saved scene: " + currSceneIdx);
    }
    public void Load()
    {
        string savedSceneIdx = saveValues[saveFileNumber].Split(',')[0]; //take the first value (level)
        currSceneIdx = int.Parse(savedSceneIdx);
        //SceneManager.LoadScene(scenes[sceneIdx]);
        Debug.Log("Loaded scene: " + currSceneIdx);
    }
    public void ResetSaveFile() //call ONLY after SetSaveFileNumber()
    {
        currSceneIdx = 0;
        Save();
    }
    public void LevelUp() //call when switching to next level
    {
        currSceneIdx++;
        Save();
        Load();
    }
    public void SetSaveFileNumber(int n)
    {
        saveFileNumber = n;
    }
}
