using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1.씬에서 "VIP"태그를 가진 오브젝트를 찾아서
// 2. 만약 있으면 마커라인을 끈다.

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
