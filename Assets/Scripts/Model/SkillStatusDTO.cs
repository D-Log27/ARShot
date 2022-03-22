using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬정보 모델
/// </summary>
public class SkillStatusDTO : MonoBehaviour
{
    /// <summary>
    /// 스킬사용가능시간
    /// </summary>
    public int skillActivableTime { get; set; }
    
    /// <summary>
    /// 스킬 사용가능 여부
    /// </summary>
    public bool isActive { get; set; }

    /// <summary>
    /// 스킬 쿨타임
    /// </summary>
    public int skillCoolTime { get; set; }

    /// <summary>
    /// 스킬 사용중 여부
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
