using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ĳ���� ���� ��
/// </summary>
public class CharacterInfoDTO : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �̸�
    /// </summary>
    public string playerName { get; set; }
    /// <summary>
    /// ų ī��Ʈ
    /// </summary>
    public int killCnt { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    public int score { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="playerName">�÷��̾� �̸�</param>
    /// <param name="hp">ü��</param>
    /// <param name="shield">����</param>
    public CharacterInfoDTO(string playerName)
    {
        this.playerName = playerName;
        this.killCnt = 0;
        this.score = 0;
    }
}
