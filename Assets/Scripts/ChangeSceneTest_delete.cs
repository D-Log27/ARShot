using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTest_delete : MonoBehaviour
{
    public void GotoChangeScene()
    {
        SceneManager.LoadScene("InGame_KSY", LoadSceneMode.Single);
    }

    public void SceneChange(string sceneChangeName)
    {
        SceneManager.LoadScene(sceneChangeName, LoadSceneMode.Single);
    }       
}
