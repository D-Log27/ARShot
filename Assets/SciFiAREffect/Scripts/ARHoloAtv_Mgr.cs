using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

// 
public class ARHoloAtv_Mgr: MonoBehaviour
{

    public GameObject Vi_01;
    public GameObject HoloEarth;
    public GameObject HoloSdLg;
    public GameObject HoloCEft;
    public GameObject image00;
    public GameObject image01;
    public GameObject image02;
    public GameObject image03;
    public GameObject aRHoloEft;
    public static ARHoloAtv_Mgr Ins;
  

    void Awake()
    {
        Ins = this;
    }

#region Active HoloGameObject

    public void SwHoloEarth(bool Bl,float Tm)
    {       
        StartCoroutine(LateSwHoloEarth(Bl, Tm)); //StartCoroutine: 코루틴 함수를 호출할 때
    }

     public void SwHoloSdLg(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloHoloSdLg(Bl, Tm));
    }

    public void SwVi_01(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloVi_01(Bl, Tm));
    }


    public void SwHoloCEft(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloCEft(Bl, Tm));
    }

    public void SwImage00(bool Bl, float Tm)
    {
        StartCoroutine(LateSwImage00(Bl, Tm));
    }

    #endregion


    #region 

    IEnumerator LateSwHoloEarth(bool Bl,float Tm) //IEnumerator: 코루틴을 만들때는 void대신 IEnumerator을 사용
    {
        if (Bl)
        {
            //yield return: 코루틴에서 동작하는 제어권을 유니티에 다시 돌려준다는 뜻
            yield return new WaitForSeconds(Tm); //이 지점에 도착하면 뒤에 명시한 시간만큼 코드 동작을 중지하고 제어권을 유니티에 반환한다. ⤴️어디로??(⬅️↩️)
            HoloEarth.SetActive(Bl);             //명시된 시간이 끝나면 이 다음줄 부터 다시 코드가 동작한다 
        }                                        
        else
        {
            yield return new WaitForEndOfFrame(); //한 프레임을 지나가는 과정이 다 끝난 뒤 제일 마지막
            HoloEarth.SetActive(Bl);
        }     
    }

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

    IEnumerator LateSwImage00(bool Bl, float Tm)
    {
        if (Bl)
        {
            yield return new WaitForSeconds(Tm);
            image00.SetActive(Bl);
            Invoke ("LateSwImage01", 3f);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            image00.SetActive(Bl);
        }
    }

    void LateSwImage01()
    {
        image00.SetActive(false);
        image01.SetActive(true);
        Invoke ("LateSwImage02", 3f);
    }

    void LateSwImage02()
    {
        image01.SetActive(false);
        image02.SetActive(true);
        Invoke ("LateSwImage03", 3f);
    }

     void LateSwImage03()
    {
        image02.SetActive(false);
        image03.SetActive(true);
        Invoke ("Late", 3f);
    }

    void Late()
    {
        image03.SetActive(false);
        aRHoloEft.SetActive(false);
        //차례대로 움직이기

    }


  #endregion

}
