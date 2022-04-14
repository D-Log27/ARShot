using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

/// <summary>
/// Title Manager
/// </summary>
public class TitleManager : MonoBehaviour
{
    private static TitleManager _instance;
    TitleManager() { }

    public static TitleManager GetInstance()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        return _instance;
    }
    
    public Transform startBtn;
    public Transform optionBtn;
    public Transform exitBtn;
    public Transform loading;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        startBtn.GetComponent<Button>().onClick.AddListener(() => OnClickStartButton());
        optionBtn.GetComponent<Button>().onClick.AddListener(() => OnClickOptionButton());
        exitBtn.GetComponent<Button>().onClick.AddListener(() => OnClickExitButton());
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
        SceneManager.LoadScene("Option_AL");
    }

    /// <summary>
    /// Title -> Exit Click
    /// </summary>
    public void OnClickExitButton()
    {
        print("### exit OnClick Check");
        Application.Quit();
    }

        float threeSec = 3;
    /// <summary>
    /// Title -> Start Click
    /// </summary>
    public void OnClickStartButton()
    {
        TitleLoadingImage(true);
        bool isSuccess = PhotonManager.GetInstance().ConnectingRoom();
        if (isSuccess)
        {
            TitleLoadingImage(false);
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