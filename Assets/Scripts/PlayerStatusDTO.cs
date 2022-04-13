using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusDTO : MonoBehaviour
{
    //PlayerStatusDTO �̱���
    public static PlayerStatusDTO instance;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// �÷��̾� HP
    /// </summary>
    public float playerHP { get; set; }

    /// <summary>
    /// �÷��̾ ���� Skill ��� ������ ����
    /// </summary>
    public bool isSkillActive { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public PlayerStatusDTO()
    {
        this.playerHP = 10;
        this.isSkillActive = false;
    }
}
