using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoardSceneTest_KSY : MonoBehaviour
{
    public void InGameScene()
    {
        SceneManager.LoadScene("InGameBriefing_KSY", LoadSceneMode.Single);
    }

    public void GotoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
