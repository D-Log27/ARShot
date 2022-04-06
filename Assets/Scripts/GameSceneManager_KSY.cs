using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //1. 씬 간에 이동을 하기 위해 필요한 SceneManagement 넣기

// 목표 : StartButton을 누르면 InPlayScene으로 연결되게 하고 싶다.

public class GameSceneManager_KSY : MonoBehaviour
{
    public void InGame()   //2. Start와 Update는 사용하지 않으므로 지우고 InGame이라는 이름의 함수를 만들음. Button쪽에서 사용해야 되기 때문에 Public으로 선언
    {

        SceneManager.LoadScene("InGame_KSY", LoadSceneMode.Single); //3. <SceneManager클래스>에서 <LoadScene함수>를 호출하고, 파라미터로 불러올 씬 이름(그대로 적어야 함)과 이 씬을 어떻게 불러 올 건지(LoadSceneMode.) 선택(Additive 또는 Single)한다. 
    }

    public void GotaoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }



}
