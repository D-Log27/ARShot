using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionManager : MonoBehaviour
{
    public TMP_Text userID;

    public void OnClickIDChange()
    {
        userID.text = "Anthony Lee";
        //TouchScreenKeyboard.Open()
    }

    public void OnClickBackToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_AL");
    }
}
