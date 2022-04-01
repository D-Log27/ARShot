using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �� ���� ��
/// </summary>
public class UnitStatusDTO : MonoBehaviour
{
    /// <summary>
    /// �� ü��
    /// </summary>
    public int hp { get; set; }
    /// <summary>
    /// �� ����
    /// </summary>
    public int shield { get; set; }
    /// <summary>
    /// ������ �����Ÿ�
    /// </summary>
    public int attackRange { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hp">ü��</param>
    /// <param name="shield">����</param>
    /// <param name="attackRange">���ݹ���</param>
    public UnitStatusDTO(int hp, int shield, int attackRange)
    {
        this.hp = hp;
        this.shield = shield;
        this.attackRange = attackRange;
    }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hp">ü��</param>
    /// <param name="shield">����</param>
    public UnitStatusDTO(int hp, int shield)
    {
        this.hp = hp;
        this.shield = shield;
    }
}
