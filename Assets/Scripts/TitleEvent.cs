using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

/// <summary>
/// Title 화면 이벤트
/// </summary>
public class TitleEvent : MonoBehaviour
{
    Transform optionBtn;
    Transform exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        optionBtn = this.transform.Find("Option");
        exitBtn = this.transform.Find("Exit");
        optionBtn.GetComponent<Button>().onClick.AddListener(()=>OnClickOptionButton());
        exitBtn.GetComponent<Button>().onClick.AddListener(()=>OnClickExitButton());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Title -> Option Click
    /// </summary>
    public void OnClickOptionButton()
    {
        print("### option OnClick Check");
        SceneManager.LoadScene("Options");
    }

    /// <summary>
    /// Title -> Exit Click
    /// </summary>
    public void OnClickExitButton()
    {
        print("### exit OnClick Check");
        Application.Quit();
    }
}
