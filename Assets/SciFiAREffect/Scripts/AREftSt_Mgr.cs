using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AREftSt_Mgr : MonoBehaviour
{
    private bool GetButtonDown = true;

    private void Update() 
    {
        Click();
    }

    public void Click()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AREftSt();
        }
        



        // else
        // {
        //     return;
        //     //AREftRe();
        // }
        //GetButtonDown = !GetButtonDown;
        //GetButtonDown = false;
        //return;
    }

    //  private bool Bl_Btn = true;

    // public void Btn()
    // {
    //     if (Bl_Btn)//버튼을 누르면 
    //     {
    //         AREftSt();//Start 로직 실행
    //     }
    //     else //누르지않은 상태
    //     {
    //         AREftRe(); //Re로직 실행
    //     }
    //     Bl_Btn = !Bl_Btn; // 앞을 A, 뒤를 B라고 할 때 A가 참이면 B가 거짓이 되고 A가 거짓이면 B가 참이 된다. 
    // }

    public void AREftSt()
    {
        ARHoloAtv_Mgr.Ins.SwHoloEarth(true,3f);
        ARHoloAtv_Mgr.Ins.SwHoloSdLg(true,2.7f);
        ARHoloAtv_Mgr.Ins.SwVi_01(true,3.5f);
        ARHoloAtv_Mgr.Ins.SwHoloCEft(true,0.5f);
        AREft_Ani_Mgr.Ins.StAni();



    }

    public void AREftRe()
    {
        ARHoloAtv_Mgr.Ins.SwHoloEarth(false, 0.1f);
        ARHoloAtv_Mgr.Ins.SwHoloSdLg(false, 0.1f);
        ARHoloAtv_Mgr.Ins.SwVi_01(false, 0.1f);
        ARHoloAtv_Mgr.Ins.SwHoloCEft(false, 0.1f);
        AREft_Ani_Mgr.Ins.ReAni();
    }

    // public void BtnBack()
    // {
    //     SceneManager.LoadScene(1);
    // }
}

