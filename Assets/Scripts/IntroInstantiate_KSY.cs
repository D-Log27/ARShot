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

        // ���� ������ Intro ���ӿ�����Ʈ �˻� �� ����
        vip  = GameObject.FindWithTag("VIP");
        
        GameObject nextIntro = Instantiate(hollogram, transform);
        //nextIntro.transform.position = introObject.position;
    }
}
