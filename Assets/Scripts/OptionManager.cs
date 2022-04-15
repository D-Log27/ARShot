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
    //OptionManagerDTO 싱글톤
    public static OptionManager instance;

    /// <summary>
    /// 플레이어 ID
    /// </summary>
    public string PlayerID { get; set; }

    /*
     * Audio
     * Music: 진짜 노래
     * Ambient: 배경음(시대, 환경 등등을 알려줌)
     * SFX: 특수효과
     */

    /// <summary>
    /// 브금 볼륨 크기
    /// </summary>
    public int BGMVolume { get; set; }

    /// <summary>
    /// 특수효과 볼륨
    /// </summary>
    public int SFXVolume { get; set; }

    /// <summary>
    /// 플레이어의 현재 Scene 정보
    /// </summary>
    public string PlayerScene { get; set; }

    /// <summary>
    /// 화면 밝기
    /// </summary>
    public float Brightness { get; set; }


    //옵션 캔버스그룹
    public CanvasGroup optionCGrp;

    //유저명
    public TMP_Text txt_UserID;

    //사운드 텍스트 표시
    public TMP_Text txt_bgmValue;
    public TMP_Text txt_sfxValue;

    //사운드 슬라이더
    public Slider bgmSlider;
    public Slider sfxSlider;


    public float bgmVolume;
    public float sfxVolume;

    public Transform backBtn;


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

    private void Start()
    {
        backBtn.GetComponent<Button>().onClick.AddListener(() => OnClickBackButton());
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        //txt_bgmValue.text = Convert.ToString(bgmVolume.value) + "%";
        //txt_sfxValue.text = Convert.ToString(sfxVolume.value) + "%";
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Option_AL")
        {
            // Tag Canvas UI를 연결
            bgmSlider = GameObject.FindGameObjectWithTag("UI_OPT_BGM_SLIDER").GetComponent<Slider>();
            txt_bgmValue = GameObject.FindGameObjectWithTag("UI_OPT_BGM_VALUE").GetComponent<TMP_Text>();

            // Slider Event Connect
            bgmSlider.onValueChanged.AddListener(OnChangeBGMVolume);
            
            // Tag Canvas UI를 연결
            sfxSlider = GameObject.FindGameObjectWithTag("UI_OPT_SFX_SLIDER").GetComponent<Slider>();
            txt_sfxValue = GameObject.FindGameObjectWithTag("UI_OPT_SFX_VALUE").GetComponent<TMP_Text>();

            // Slider Event Connect
            sfxSlider.onValueChanged.AddListener(OnChangeSFXVolume);
        }
    }

    void OnChangeBGMVolume(float value)
    {
        bgmVolume = value;
        txt_bgmValue.text = $"{bgmVolume} %";
    }
    
    void OnChangeSFXVolume(float value)
    {
        sfxVolume = value;
        txt_sfxValue.text = $"{sfxVolume} %";
    }

    public void OnClickBackButton()
    {
    }
}
