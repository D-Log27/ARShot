using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//TODO:
//FIXME:

public class ARHoloAtv_Mgr: MonoBehaviour
{
    public GameObject Vi_01;
    public GameObject HoloEarth;
    public GameObject HoloSdLg;
    public GameObject HoloCEft;
    public static ARHoloAtv_Mgr Ins; //➡️코루틴

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

    //➡️IEnumerator: Coroutine을 선언할 때 사용하는 반환형은 void가 아닌 IEnumerator 사용
    IEnumerator LateSwHoloEarth(bool Bl,float Tm)
    {
        if (Bl)
        {
            //➡️yield return: Coroutine에서 동작하는 제어권을 유니티에 다시 돌려줌
            //yield return 뒤에 명시한 시간만큼 코드 동작을 중지하고 제어권을 유니티에 돌려줌
            //명시한 시간이 끝나면 다시 Coroutine이 동작한다.
            yield return new WaitForSeconds(Tm); //➡️WaitForSeconds: 생성자의 매개변수로 넣어준 시간만큼
            HoloEarth.SetActive(Bl);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            HoloEarth.SetActive(Bl);
        }     
    }


    //
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
