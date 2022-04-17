using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 터치 연결은 나중에! 유니티 내에서 순서 먼저 지금은 순서부터 구현하기!
public class HologramTornado_test : MonoBehaviour
{
    public GameObject sqCrc_01;
    public GameObject sqCrc_02;
    public GameObject sqCrc_03;
    public GameObject sqCrc_04;
    public GameObject sqCrc_05;
    public GameObject sqCrc_06;
    public GameObject model_Head;
    public GameObject aRHoloM_Test;

    void Start()
    {
        print(1);
        Invoke("SqCrc_01", 0.5f);
    }

    void SqCrc_01()
    {
        sqCrc_01.SetActive(true);
        Invoke("SqCrc_02", 0.5f);
    }

     void SqCrc_02()
    {
        sqCrc_02.SetActive(true);
        Invoke("SqCrc_03", 0.5f);
    }

    void SqCrc_03()
    {
         sqCrc_03.SetActive(true);
         Invoke("SqCrc_04", 0.5f);
    }

    void SqCrc_04()
    {
        sqCrc_04.SetActive(true);
        Invoke("SqCrc_05", 0.5f);
    }

    void SqCrc_05()
    {
        sqCrc_05.SetActive(true);
        Invoke("SqCrc_06", 0.5f);
    }

    void SqCrc_06()
    {
        sqCrc_06.SetActive(true);
        Invoke("Model_Head", 0.5f);
    }

    void Model_Head()
    {
        model_Head.SetActive(true);
        Invoke("ARHoloM_Test", 0.5f);
    }

    void ARHoloM_Test()
    {
        aRHoloM_Test.SetActive(true); 
        //return; 
    }
}