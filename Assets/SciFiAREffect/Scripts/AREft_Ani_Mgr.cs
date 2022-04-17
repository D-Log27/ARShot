using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AREft_Ani_Mgr : MonoBehaviour
{
    public Animator[] AniAry;
    public static AREft_Ani_Mgr Ins;
    private float tmCount = 0;
    private bool blAni = true;
    
    void Awake()
    {
        Ins = this;
    }

    public void StAni()
    {
        StartCoroutine(crc02());
    }

    public void ReAni()
    {
        foreach (Animator i in AniAry)
        {
            i.SetTrigger("Re");
        }
    }

    IEnumerator crc02()
    {

        for (int i = 0; i < AniAry.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if (blAni)
            {
                AniAry[i].SetTrigger("St");
            }         
        }
    }

}
