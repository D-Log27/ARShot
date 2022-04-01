using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ammo Model
/// </summary>
public class AmmoDTO : MonoBehaviour
{
    /// <summary>
    /// ���� ź��
    /// </summary>
    public int currentAmmoCnt { get; set; }
    /// <summary>
    /// źâ �� �ִ� ź�� ��
    /// </summary>
    public int ammoSize { get; set; }

    public AmmoDTO(int currentAmmoCnt, int ammoSize)
    {
        this.currentAmmoCnt = currentAmmoCnt;
        this.ammoSize = ammoSize;
    }
}
