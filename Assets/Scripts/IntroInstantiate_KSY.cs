using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroInstantiate_KSY : MonoBehaviour
{
    private GameObject vip;
    public GameObject hollogram;
    //public Transform introObject;
    
    void Start()
    {

        // 씬에 생성된 Intro 게임오브젝트 검색 및 추출
        vip  = GameObject.FindWithTag("VIP");
        
        GameObject nextIntro = Instantiate(hollogram, transform);
        //nextIntro.transform.position = introObject.position;
    }
}
