using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusDTO : MonoBehaviour
{
    //PlayerStatusDTO 싱글톤
    public static PlayerStatusDTO instance;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 플레이어 HP
    /// </summary>
    public float playerHP { get; set; }

    /// <summary>
    /// 플레이어가 현재 Skill 사용 중인지 여부
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
