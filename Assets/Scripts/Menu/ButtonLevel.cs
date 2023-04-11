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
    Image myCheck;
    [SerializeField]
    TMPro.TextMeshProUGUI myText;

    public LevelState myState;
    private void OnEnable()
    {
        switch (myState)
        {
            case LevelState.Locked:
                LevelLocked();
                break;
            case LevelState.Unlocked:
                LevelUnlocked();
                break;
            case LevelState.Completed:
                LevelCompleted();
                break;
        }
    }

    private void LevelCompleted()
    {
        LevelUnlocked();
        myCheck.enabled = true;
    }

    private void LevelLocked()
    {
        myText.text = "Locked";
        myLock.enabled = true;
        myCheck.enabled = false;
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
}
public enum LevelState
{
    Locked = 0,
    Unlocked = 1,
    Completed = 2,
    Last = 3
}