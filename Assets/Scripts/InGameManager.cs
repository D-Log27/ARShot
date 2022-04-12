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

    }

    public void OnClickClassSkill()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            print("Skill");
        }
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

    public void OnClickAttack()
    {
        print("Attack");
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
