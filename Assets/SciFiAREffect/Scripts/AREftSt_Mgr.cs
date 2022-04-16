using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene����� �� �����ؾ� �ϴ� ��

public class AREftSt_Mgr : MonoBehaviour
{
    private bool Bl_Btn = true;

    public void Btn()
    {
        if (Bl_Btn)//��ư�� ������ 
        {
            AREftSt();//Start ���� ����
        }
        else //���������� ����
        {
            AREftRe(); //Re���� ����
        }
        Bl_Btn = !Bl_Btn; // ���� A, �ڸ� B��� �� �� A�� ���̸� B�� ������ �ǰ� A�� �����̸� B�� ���� �ȴ�. 
    }

    public void AREftSt() //Start ��ư�� ������
    {
        ARHoloAtv_Mgr.Ins.SwHoloEarth(true,3f); //3�ʵڿ� �Ҵ�.
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
