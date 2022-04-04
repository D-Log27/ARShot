using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //1. �� ���� �̵��� �ϱ� ���� �ʿ��� SceneManagement �ֱ�

// ��ǥ : StartButton�� ������ InPlayScene���� ����ǰ� �ϰ� �ʹ�.

public class GameSceneManager_KSY : MonoBehaviour
{
    public void InGame()   //2. Start�� Update�� ������� �����Ƿ� ����� InGame�̶�� �̸��� �Լ��� ������. Button�ʿ��� ����ؾ� �Ǳ� ������ Public���� ����
    {

        SceneManager.LoadScene("InGame_KSY", LoadSceneMode.Single); //3. <SceneManagerŬ����>���� <LoadScene�Լ�>�� ȣ���ϰ�, �Ķ���ͷ� �ҷ��� �� �̸�(�״�� ����� ��)�� �� ���� ��� �ҷ� �� ����(LoadSceneMode.) ����(Additive �Ǵ� Single)�Ѵ�. 
    }

    public void GotaoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }



}
