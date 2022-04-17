using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager Instance;

    InGameManager() { }

    public static InGameManager GetInstance()
    {
        return Instance;
    }

    public Canvas gameUI;
    
    public Transform gameOver;
    public CanvasGroup clearCGrp;
    public CanvasGroup failCGrp;

    public Button clear;
    public Button fail;

    public bool isBriefEnd;

    // Start is called before the first frame update
    void Start()
    {
        isBriefEnd = false;
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (isBriefEnd)
        {
            BriefEnd();
        }
    }

    public void OnClickAttack()
    {
        print("Attack");
    }

    /// <summary>
    /// ĵ�����׷��� 3���� �Ӽ��� �ѹ��� ����
    /// </summary>
    /// <param name="cGrp"></param>
    /// <param name="isOn"></param>
    void CanvasGroupOnOff(CanvasGroup cGrp, bool isOn)
    {
        cGrp.alpha = Convert.ToSingle(isOn);
        cGrp.interactable = cGrp.blocksRaycasts = isOn;
    }

    public void OnClickOption()
    {
        SceneManager.LoadScene("Option_AL");
    }

    public void OnClickClear()
    {
        clear.gameObject.SetActive(false);
        fail.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
        clearCGrp.alpha = 1;
        failCGrp.alpha = 0;
    }

    public void OnClickFail()
    {
        clear.gameObject.SetActive(false);
        fail.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
        clearCGrp.alpha = 0;
        failCGrp.alpha = 1;
    }

    public void OnClickTitle()
    {
        SceneManager.LoadScene("Title_AL");
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("InGame_AL");
    }

    public void OnClickEndGame()
    {
        Application.Quit();
    }

    public void BriefEnd()
    {
        // ScreenController.GetInstance().FinishBrief();
        // TODO : Game UI Smoothly ON(Alpha 값 0부터 1까지 조정)
        gameUI.enabled = true;
        // TODO : 총기 연동
    }
}
