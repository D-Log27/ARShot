using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 캐릭터 정보 모델
/// </summary>
public class CharacterInfoDTO : MonoBehaviour
{
    /// <summary>
    /// 플레이어 이름
    /// </summary>
    public string playerName { get; set; }
    /// <summary>
    /// 킬 카운트
    /// </summary>
    public int killCnt { get; set; }
    /// <summary>
    /// 점수
    /// </summary>
    public int score { get; set; }
    /// <summary>
    /// 플레이어 체력
    /// </summary>
    public int hp { get; set; }
    /// <summary>
    /// 플레이어 쉴드
    /// </summary>
    public int shield { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="playerName">플레이어 이름</param>
    /// <param name="hp">체력</param>
    /// <param name="shield">쉴드</param>
    public CharacterInfoDTO(string playerName, int hp, int shield)
    {
        this.playerName = playerName;
        this.killCnt = 0;
        this.score = 0;
        this.hp = hp;
        this.shield = shield;
    }
}
