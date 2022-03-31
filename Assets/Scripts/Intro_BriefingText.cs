using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : [briefingMan]이 생성되고 2초 뒤에 [briefingText]를 생성하고 싶다.
//목표2 : [briefingText]가 생성되고 5초 뒤에 [briefingMan]와 <3DText>가 사라지게 하고 싶다.
//목표3 : 마커 위에 UI버튼이 나오게 하고 싶다.

public class Intro_BriefingText : MonoBehaviour
{
    // 처음에 [briefingText]가 꺼진 상태로 시작
    // 2초 뒤 활성화

   
    public GameObject briefingText;

    float currentTime;
    public float createTime = 2f;
    //public GameObject defenceObjectFactory;

    void Update()
    {
        briefingText.SetActive(false);

        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            briefingText.SetActive(true);
          
        }


        //1-1) 처음 시작할 때 Text를 비활성화해서 안보이게하고 싶다.
        //1-2) 1.5초 뒤에 활성화해서 보이게하고 싶다.


        //currentTime += Time.deltaTime;
        //if (currentTime > createTime)
        //{
        //    currentTime = 0;
        //    Destroy(gameObject);
        //    //gameObject.SetActive(false);

        //    GameObject defenceObject = Instantiate(defenceObjectFactory);
        //    defenceObject.transform.position = transform.position;

    }
}
