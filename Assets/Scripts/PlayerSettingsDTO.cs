using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsDTO : MonoBehaviour
{
    //PlayerSettingsDTO �̱���
    public static PlayerSettingsDTO instance;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// �÷��̾� ID
    /// </summary>
    public string playerID { get; set; }

    /*
     * Audio
     * Music: ��¥ �뷡
     * Ambient: �����(�ô�, ȯ�� ����� �˷���)
     * SFX: Ư��ȿ��
     */

    /// <summary>
    /// ��� ���� ũ��
    /// </summary>
    public int bgmVolume { get; set; }

    /// <summary>
    /// Ư��ȿ�� ����
    /// </summary>
    public int sfxVolume { get; set; }

    /// <summary>
    /// �÷��̾��� ���� Scene ����
    /// </summary>
    public string playerScene { get; set; }

    /// <summary>
    /// ȭ�� ���
    /// </summary>
    public float brightness { get; set; }
}
