using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    SaveScript save;
    private void Start()
    {
        save = GameObject.Find("DontDestroyOnLoad").GetComponent<SaveScript>();
    }


    public int nextLevelID;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            save.LevelUp();
            SceneManager.LoadScene(nextLevelID);
        }
    }
}
