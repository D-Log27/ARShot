using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerGuide_KSY : MonoBehaviour
{
    private GameObject tagVIP;
    public GameObject markerLine;

    private void Update()
    {
        tagVIP = GameObject.FindWithTag("VIP");

        if (tagVIP != null)
        {
            markerLine.gameObject.SetActive(false);
        }
    }
}
