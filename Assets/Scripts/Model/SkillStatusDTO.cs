using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų���� ��
/// </summary>
public class SkillStatusDTO : MonoBehaviour
{
    /// <summary>
    /// ��ų��밡�ɽð�
    /// </summary>
    public int skillActivableTime { get; set; }
    
    /// <summary>
    /// ��ų ��밡�� ����
    /// </summary>
    public bool isActive { get; set; }

    /// <summary>
    /// ��ų ��Ÿ��
    /// </summary>
    public int skillCoolTime { get; set; }

    /// <summary>
    /// ��ų ����� ����
    /// </summary>
    public bool isActivable { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public SkillStatusDTO()
    {
        this.skillActivableTime = 10;
        this.isActive = true;
        this.skillCoolTime = 0;
        this.isActivable = false;
    }
}
