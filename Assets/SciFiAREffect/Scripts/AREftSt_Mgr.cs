using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AREftSt_Mgr : MonoBehaviour
{

    private bool Bl_Btn = true;


    public void Btn()
    {
        if (Bl_Btn)
        {
            AREftSt();
        }
        else
        {
            AREftRe();
        }
        Bl_Btn = !Bl_Btn;
    }

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

    public void BtnBack()
    {
        SceneManager.LoadScene(1);
    }

}
