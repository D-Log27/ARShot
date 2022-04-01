using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public Transform GameOver;

    public Button Attack;
    public Button Skill;
    public Button Alpha;
    public Button Title;
    public Button Restart;
    public Button EndGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickAlpha()
    {
        Alpha.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(true);
    }

    public void OnClickAttack()
    {

    }

    public void OnClickTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_AL");
    }
    public void OnClickRestart()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title_Test");
    }

    public void OnClickEndGame()
    {
        Application.Quit();
    }
}
