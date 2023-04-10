using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    private SaveScript save;
    public int nextLevelID;

    private void Start()
    {
        save = GameObject.Find("DontDestroyOnLoad").GetComponent<SaveScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            save.LevelUp();
            SceneManager.LoadScene(nextLevelID);
        }
    }
}
