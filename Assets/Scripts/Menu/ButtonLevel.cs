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
    TMPro.TextMeshProUGUI myText;
    private void OnEnable()
    {
        myButton.interactable = true;
        myLock.color = new Color(0f,0f,0f,0f);
        myText.text = myName;
        Debug.Log("Enable");
    }

    private void OnDisable()
    {
        myButton.interactable = false;
        myLock.color = new Color(1f, 1f, 1f, 1f);
        myText.text = "Locked";
        Debug.Log("Disable");
    }
}
