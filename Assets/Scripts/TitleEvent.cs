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
    public Transform startBtn;
    public Transform optionBtn;
    public Transform exitBtn;
    public Transform loading;
    
    // Start is called before the first frame update
    void Start()
    {
        startBtn.GetComponent<Button>().onClick.AddListener(() => OnClickStartButton());
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

    /// <summary>
    /// Title -> Start Click
    /// </summary>
    public void OnClickStartButton()
    {
        TitleLoadingImage(true);
        bool isSuccess = PhotonManager.GetInstance().ConnectingRoom();
        if(isSuccess)
        {
            TitleLoadingImage(false);
            // TODO : load room scene
        }
    }

    /// <summary>
    /// Activate/Deactivate loading image
    /// </summary>
    /// <param name="flag"></param>
    public void TitleLoadingImage(bool flag)
    {
        loading.gameObject.SetActive(flag);
    }
}
