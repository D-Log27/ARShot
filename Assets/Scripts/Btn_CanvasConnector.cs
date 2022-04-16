using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_CanvasConnector : MonoBehaviour
{
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Title_AL":
                break;

            case "Room_AL":
                break;

            case "InGame_AL":
                break;

            default:
                break;
        }
    }
}
