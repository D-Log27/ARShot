using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//옵션은 개별 씬이 아니라 나머지 UI 전부 가리는 개별 게임오브젝트
//플레이어마다 개별적인 설정이 가능해야 함
public class OptionManager : MonoBehaviour
{
    #region 변수 영역
    //OptionManager 싱글톤
    public static OptionManager instance;

    /*
     * Audio
     * Music: 진짜 노래
     * Ambient: 배경음(시대, 환경 등등을 알려줌)
     * SFX: 특수효과
     */

    //Option씬 연결 버튼들 모은 부모
    public CanvasGroup cGrpConnector;

    //유저명 입력창
    public TMP_InputField inputField;

    //유저명
    public TMP_Text txt_UserID;

    //옵션 캔버스그룹
    public CanvasGroup optionCGrp;

    //사운드 텍스트 표시
    public TMP_Text txt_bgmValue;
    public TMP_Text txt_sfxValue;

    //사운드 슬라이더
    public Slider bgmSlider;
    public Slider sfxSlider;

    #endregion
    private void Awake()
    {
        //instance가 할당되지 않은 경우
        if (instance == null)
        {
            instance = this;
        }

        //instance에 할당된 클래스의 인스턴스가 다를 경우 새로 생성된 클래스를 의미함
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //다른 씬으로 넘어가더라도 삭제하지 않고 유지함
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        txt_bgmValue.text = Convert.ToString(bgmSlider.value) + "%";
        txt_sfxValue.text = Convert.ToString(sfxSlider.value) + "%";
    }

    public void OnClickOptionButton()
    {
        optionCGrp.alpha = 1;
        optionCGrp.interactable = optionCGrp.blocksRaycasts = true;
        cGrpConnector.alpha = 0;
        cGrpConnector.interactable = cGrpConnector.blocksRaycasts = false;
    }

    public void OnClickBackButton()
    {
        optionCGrp.alpha = 0;
        optionCGrp.interactable = optionCGrp.blocksRaycasts = false;
        cGrpConnector.alpha = 1;
        cGrpConnector.interactable = cGrpConnector.blocksRaycasts = true;
    }
}
