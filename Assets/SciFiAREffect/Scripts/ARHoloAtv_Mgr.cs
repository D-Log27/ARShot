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

    public void SwVi_01(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloVi_01(Bl, Tm));
    }

    public void SwHoloSdLg(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloHoloSdLg(Bl, Tm));
    }

    public void SwHoloCEft(bool Bl, float Tm)
    {
        StartCoroutine(LateSwHoloCEft(Bl, Tm));
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

    #endregion

}
