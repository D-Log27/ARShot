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

    public Transform gameOver;
    public CanvasGroup clearCGrp;
    public CanvasGroup failCGrp;

    public Button clear;
    public Button fail;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        skillGuideLine.color = Color.gray;
    }

    public void OnClickAttack()
    {
        print("Attack");
    }

    public void OnClickClassSkill()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            print("Skill");
        }
    }

    /// <summary>
    /// UI 보이기
    /// </summary>
    public void OnClickDrawerOpener()
    {
        drawerOpener.position = Vector3.MoveTowards(drawerOpener.position, drawerCloserPos, 1);
        print("전");
        uiBox.position = Vector3.MoveTowards(uiBox.position, new Vector3(uiBox.position.x, uiBoxPos.y + drawerOpenerPos.y - drawerCloserPos.y, uiBox.position.z), 1);
        print("후");
        CanvasGroupOnOff(cGrpDrawerCloser, true);
        CanvasGroupOnOff(cGrpDrawerOpener, false);
    }

    /// <summary>
    /// UI 숨기기
    /// </summary>
    public void OnClickDrawerCloser()
    {
        drawerCloser.position = Vector3.MoveTowards(drawerCloser.position, drawerOpenerPos, 1);
        uiBox.position = Vector3.MoveTowards(uiBox.position, new Vector3(uiBox.position.x, uiBoxPos.y + drawerCloserPos.y - drawerOpenerPos.y, uiBox.position.z), 1);
        CanvasGroupOnOff(cGrpDrawerOpener, true);
        CanvasGroupOnOff(cGrpDrawerCloser, false);
    }

    /// <summary>
    /// 캔버스그룹의 3가지 속성을 한번에 제어
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
        ScreenController.GetInstance().FinishBrief();
    }
}
