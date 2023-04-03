using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[DisallowMultipleComponent]
public class ButtonLevel : MonoBehaviour
{
    [SerializeField]
    string myName = "Livello_";
    
    [SerializeField]
    Button myButton;
    [SerializeField]
    Image myLock;
    [SerializeField]
    Image myLevelImage;
    [SerializeField]
    TMPro.TextMeshProUGUI myText;

    public bool isUnlocked = true;
    private void OnEnable()
    {
        if (isUnlocked)
            LevelUnlocked();
        else
            LevelLocked();
    }

    private void LevelLocked()
    {
        myText.text = "Locked";
        myLock.enabled = true;
        myLevelImage.enabled = false;

        var bc = myButton.colors;

        bc.normalColor = new Color(.75f, .75f, .75f, 1f);
        bc.selectedColor = new Color(1f, 0f, 0f, 1f);

        myButton.colors = bc;
    }

    private void LevelUnlocked()
    {
        myText.text = myName;
        myLevelImage.enabled = true;
        myLock.enabled = false;
    }
    //private void OnDisable()
    //{
    //    myLock.color = new Color(1f, 1f, 1f, 1f);
    //    myText.text = "Locked";
    //    Debug.Log("Disable");
    //}
}
