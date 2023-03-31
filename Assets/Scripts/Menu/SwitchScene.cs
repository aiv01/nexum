using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Button button_;
    [SerializeField] int ID;


    private void Start()
    {
        Button btn = button_.GetComponent<Button>();
        btn.onClick.AddListener(Load);
    }

    public void Load()
    {

        StartCoroutine(loadLevel(ID));
    }

    IEnumerator loadLevel(int id)
    {
        //animator.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(id);
    }
}
