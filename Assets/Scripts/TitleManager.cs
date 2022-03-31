using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

/// <summary>
/// Title 화면 이벤트
/// </summary>
public class TitleManager : MonoBehaviour
{
    public Transform startBtn;
    public Transform optionBtn;
    public Transform exitBtn;
    public Transform loading;

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("Options_AL");
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
        threeSec -= Time.deltaTime;
        //아래 함수에 테스트용으로 if만 입힘
        //if (threeSec < 0)
        //{
        //    bool isSuccess = true;//PhotonManager.GetInstance().ConnectingRoom();
        //    if (isSuccess)
        //    {
        //        TitleLoadingImage(false);
        //        SceneManager.LoadScene("Room_AL");
        //    }
        //}
        bool isSuccess = true;//PhotonManager.GetInstance().ConnectingRoom();
        if (isSuccess)
        {
            TitleLoadingImage(false);
            SceneManager.LoadScene("Room_AL");
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