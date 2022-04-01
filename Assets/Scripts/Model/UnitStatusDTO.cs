using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 적 정보 모델
/// </summary>
public class UnitStatusDTO : MonoBehaviour
{
    /// <summary>
    /// 적 체력
    /// </summary>
    public int hp { get; set; }
    /// <summary>
    /// 적 쉴드
    /// </summary>
    public int shield { get; set; }
    /// <summary>
    /// 적공격 사정거리
    /// </summary>
    public int attackRange { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hp">체력</param>
    /// <param name="shield">쉴드</param>
    /// <param name="attackRange">공격범위</param>
    public UnitStatusDTO(int hp, int shield, int attackRange)
    {
        this.hp = hp;
        this.shield = shield;
        this.attackRange = attackRange;
    }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hp">체력</param>
    /// <param name="shield">쉴드</param>
    public UnitStatusDTO(int hp, int shield)
    {
        this.hp = hp;
        this.shield = shield;
    }
}
