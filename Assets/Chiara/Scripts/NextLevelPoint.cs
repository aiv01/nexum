using UnityEngine;
using UnityEngine.Events;

public class NextLevelPoint : MonoBehaviour
{
    SaveScript save;
    private void Start()
    {
        save = GameObject.Find("DontDestroyOnLoad").GetComponent<SaveScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            save.LevelUp();
        }
    }
}
