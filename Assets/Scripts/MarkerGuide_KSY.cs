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

    private void Update()
    {
        tagVIP = GameObject.FindWithTag("VIP");
        //tagTest = GameObject.FindWithTag("VIP"); //delete!!
    }

    private void Update()
    {
        //Invoke("TagTest", 5.0f); //delete!!

        if (tagVIP != null)
        {
            markerLine.gameObject.SetActive(false);
        }
    }
}
