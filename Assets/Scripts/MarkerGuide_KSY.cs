using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1.������ "VIP"�±׸� ���� ������Ʈ�� ã�Ƽ�
// 2. ���� ������ ��Ŀ������ ����.

public class MarkerGuide_KSY : MonoBehaviour //
{
    private GameObject tagVIP;
    public GameObject markerLine;
    //public GameObject tagTest; //delete!!
    //public GameObject tagTestFactory; //delete!! 

    private void Update()
    {
        tagVIP = GameObject.FindWithTag("VIP");
        this.gameObject.SetActive(tagVIP == null); //������ true�ϱ� �Ҵ�???
       

        ////tagTest = GameObject.FindWithTag("VIP"); //delete!!
 
        ////Invoke("TagTest", 5.0f); //delete!!

        //if (tagVIP != null)
        //{
        //    markerLine.gameObject.SetActive(false);
        //}
    }

    //void TagTest() //delete ~ 
    //{
    //    tagTest.gameObject.SetActive(true);
    //    if (tagVIP != null)
    //    {
    //        markerLine.gameObject.SetActive(false);
    //    }

    //} // ~ delete
}
