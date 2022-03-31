using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표2 : 7초 뒤에 [briefingMan]와 <3DText>가 사라지게 하고 싶다.
//목표3 : 마커 위에 UI버튼이 나오게 하고 싶다.

public class Intro_BriefingObjects : MonoBehaviour
{

    public GameObject brefingObjects;
    float currentTime;
    public float createTime = 7f;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            brefingObjects.SetActive(false);
        }
    }
}
