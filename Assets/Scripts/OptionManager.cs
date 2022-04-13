using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//�ɼ��� ���� ���� �ƴ϶� ������ UI ���� ������ ���� ���ӿ�����Ʈ
//��𼭵� ����Ǿ�� �ϰ�, �÷��̾�� �������� ������ �����ؾ� ��
public class OptionManager : MonoBehaviour
{
    //����ID
    public TMP_Text userID;

    //���� �ؽ�Ʈ ǥ��
    public TMP_Text txt_bgmValue;
    public TMP_Text txt_sfxValue;

    //���� �����̴�
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
    /// �ɼ�â���� ���̵� ������ ����� Ű���尡 ������ �Է¿� ���� ����ID ����
    /// </summary>
    public void OnClickIDChange()
    {
        //��ġ�� Ű���� �� �Է�â ��
        //�Է�â�� ��ĭ
        //Ȯ���ϸ� �ٲٰڴ��� Ȯ��â ��
        //Ȯ�� �� �ص� Ȯ��â ��
        //�� �ٲٸ� ���� �̸� ����
        userID.text = "Anthony Lee";
        //TouchScreenKeyboard.Open()
    }

    /// <summary>
    /// Title�� �ε�
    /// </summary>
    public void OnClickBackToTitle()
    {
        SceneManager.LoadScene("Title_AL");
    }
}
