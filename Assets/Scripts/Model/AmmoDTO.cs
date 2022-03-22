using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ammo Model
/// </summary>
public class AmmoDTO : MonoBehaviour
{
    /// <summary>
    /// ÇöÀç Åº¾Ë
    /// </summary>
    public int currentAmmoCnt { get; set; }
    /// <summary>
    /// ÅºÃ¢ ´ç ÃÖ´ë Åº¾Ë ¼ö
    /// </summary>
    public int ammoSize { get; set; }

    public AmmoDTO(int currentAmmoCnt, int ammoSize)
    {
        this.currentAmmoCnt = currentAmmoCnt;
        this.ammoSize = ammoSize;
    }
}
