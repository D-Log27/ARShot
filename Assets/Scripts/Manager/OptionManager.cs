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
    #region ���� ����
    //OptionManager �̱���
    public static OptionManager instance;

    /*
     * Audio
     * Music: ��¥ �뷡
     * Ambient: �����(�ô�, ȯ�� ����� �˷���)
     * SFX: Ư��ȿ��
     */

    //Option�� ���� ��ư�� ���� �θ�
    public CanvasGroup cGrpConnector;

    //������ �Է�â
    public TMP_InputField inputField;

    //������
    public TMP_Text txt_UserID;

    //�ɼ� ĵ�����׷�
    public CanvasGroup optionCGrp;

    //���� �ؽ�Ʈ ǥ��
    public TMP_Text txt_bgmValue;
    public TMP_Text txt_sfxValue;

    //���� �����̴�
    public Slider bgmSlider;
    public Slider sfxSlider;

    #endregion
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
