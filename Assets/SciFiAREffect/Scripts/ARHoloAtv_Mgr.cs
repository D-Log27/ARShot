using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ARHoloAtv_Mgr: MonoBehaviour
{

    public GameObject Vi_01;
    public GameObject HoloEarth;
    public GameObject HoloSdLg;
    public GameObject HoloCEft;
    public static ARHoloAtv_Mgr Ins;

    void Awake()
    {
        Ins = this;
    }


#region Active HoloGameObject

    //显示全息地球
    public void SwHoloEarth(bool Bl,float Tm)
    {       
        StartCoroutine(LateSwHoloEarth(Bl, Tm));
    }

    //显示全息视频1（有任务）
    public void SwVi_01(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloVi_01(Bl, Tm));
    }

    //显示全息神盾局标志
    public void SwHoloSdLg(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloHoloSdLg(Bl, Tm));
    }

    //显示中间的粒子喷射特效
    public void SwHoloCEft(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloCEft(Bl, Tm));
    }

    #endregion


    #region 具体执行

    //全息地球
    IEnumerator LateSwHoloEarth(bool Bl,float Tm)
    {
        if (Bl)
        {
            yield return new WaitForSeconds(Tm);
            HoloEarth.SetActive(Bl);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            HoloEarth.SetActive(Bl);
        }     
    }


    //有任务视频
    IEnumerator LateSwHoloVi_01(bool Bl, float Tm)
    {
        
        if (Bl)
        {
            yield return new WaitForSeconds(Tm);
            Vi_01.SetActive(Bl);
            //Vi_01.transform.Find("Vi").GetComponent<VideoPlayer>().Play();
        }
        else
        {
            yield return new WaitForEndOfFrame();
            //Vi_01.transform.Find("Vi").GetComponent<VideoPlayer>().Stop();
            Vi_01.SetActive(Bl);
        }      
    }

    //全息Logo
    IEnumerator LateSwHoloHoloSdLg(bool Bl, float Tm)
    {
        if (Bl)
        {
            yield return new WaitForSeconds(Tm);
            HoloSdLg.SetActive(Bl);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            HoloSdLg.SetActive(Bl);
        }
    }

    //喷射中心的粒子特效
    IEnumerator LateSwHoloCEft(bool Bl, float Tm)
    {
        if (Bl)
        {
            yield return new WaitForSeconds(Tm);
            HoloCEft.SetActive(Bl);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            HoloCEft.SetActive(Bl);
        }
    }

    #endregion

}
