using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryCatchTest_KSY : MonoBehaviour
{
    void Update()
    {
        
        //float currentTime;
        //float creatTime = 3;

        //currentTime += Time.deltaTime;
        //if (currentTime <c

        try
        {
            string bss = null;
            Debug.Log(bss.Length);
        } catch (Exception e)
        {
            Debug.Log("S2B");
        }

    }

}
