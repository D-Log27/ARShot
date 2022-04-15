using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//�ɼ��� ���� ���� �ƴ϶� ������ UI ���� ������ ���� ���ӿ�����Ʈ
//�÷��̾�� �������� ������ �����ؾ� ��
public class OptionManager : MonoBehaviour
{
    //OptionManagerDTO �̱���
    public static OptionManager instance;

    /// <summary>
    /// �÷��̾� ID
    /// </summary>
    public string PlayerID { get; set; }

    /*
     * Audio
     * Music: ��¥ �뷡
     * Ambient: �����(�ô�, ȯ�� ����� �˷���)
     * SFX: Ư��ȿ��
     */

    /// <summary>
    /// ��� ���� ũ��
    /// </summary>
    public int BGMVolume { get; set; }

    /// <summary>
    /// Ư��ȿ�� ����
    /// </summary>
    public int SFXVolume { get; set; }

    /// <summary>
    /// �÷��̾��� ���� Scene ����
    /// </summary>
    public string PlayerScene { get; set; }

    /// <summary>
    /// ȭ�� ���
    /// </summary>
    public float Brightness { get; set; }


    //�ɼ� ĵ�����׷�
    public CanvasGroup optionCGrp;

    //������
    public TMP_Text txt_UserID;

    //���� �ؽ�Ʈ ǥ��
    public TMP_Text txt_bgmValue;
    public TMP_Text txt_sfxValue;

    //���� �����̴�
    public Slider bgmSlider;
    public Slider sfxSlider;


    public float bgmVolume;
    public float sfxVolume;

    public Transform backBtn;


    private void Awake()
    {
        //instance�� �Ҵ���� ���� ���
        if (instance == null)
        {
            instance = this;
        }

        //instance�� �Ҵ�� Ŭ������ �ν��Ͻ��� �ٸ� ��� ���� ������ Ŭ������ �ǹ���
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //�ٸ� ������ �Ѿ���� �������� �ʰ� ������
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
            // Tag Canvas UI�� ����
            bgmSlider = GameObject.FindGameObjectWithTag("UI_OPT_BGM_SLIDER").GetComponent<Slider>();
            txt_bgmValue = GameObject.FindGameObjectWithTag("UI_OPT_BGM_VALUE").GetComponent<TMP_Text>();

            // Slider Event Connect
            bgmSlider.onValueChanged.AddListener(OnChangeBGMVolume);
            
            // Tag Canvas UI�� ����
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
