using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//옵션은 개별 씬이 아니라 나머지 UI 전부 가리는 개별 게임오브젝트
//어디서든 적용되어야 하고, 플레이어마다 개별적인 설정이 가능해야 함
public class OptionManager : MonoBehaviour
{
    //유저ID
    public TMP_Text userID;

    //사운드 텍스트 표시
    public TMP_Text txt_bgmValue;
    public TMP_Text txt_sfxValue;

    //사운드 슬라이더
    public Slider bgmVolume;
    public Slider sfxVolume;

    private void Update()
    {
        txt_bgmValue.text = Convert.ToString(bgmVolume.value) + "%";
        txt_sfxValue.text = Convert.ToString(sfxVolume.value) + "%";
    }
    void OnChangeBGMVolume()
    {

    }


    /// <summary>
    /// 옵션창에서 아이디를 누르면 모바일 키보드가 나오고 입력에 따라 유저ID 변경
    /// </summary>
    public void OnClickIDChange()
    {
        //터치시 키보드 및 입력창 뜸
        //입력창은 빈칸
        //확인하면 바꾸겠는지 확인창 뜸
        //확인 안 해도 확인창 뜸
        //안 바꾸면 원래 이름 유지
        userID.text = "Anthony Lee";
        //TouchScreenKeyboard.Open()
    }

    /// <summary>
    /// Title씬 로드
    /// </summary>
    public void OnClickBackToTitle()
    {
        SceneManager.LoadScene("Title_AL");
    }
}
