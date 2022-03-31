using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : [briefingMan]이 생성되고 2초 뒤에 [briefingText]를 생성하고 싶다.
//목표2 : [briefingText]가 생성되고 5초 뒤에 [briefingMan]와 <3DText>가 사라지게 하고 싶다.

public class Intro_BriefingText : MonoBehaviour
{
    // 처음에 [briefingText]가 꺼진 상태로 시작
    // 2초 뒤 활성화


    public GameObject briefingText;

    float currentTime;
    public float createTime = 2f;
    //public GameObject defenceObjectFactory;

    void Start()
    {
        briefingText.SetActive(false);
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            briefingText.SetActive(true);

        }
    }
}
