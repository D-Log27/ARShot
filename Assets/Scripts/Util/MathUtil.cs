using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SysRandom = System.Random;

/// <summary>
/// 수식 계산 Utility
/// </summary>
public class MathUtil : MonoBehaviour
{
    private static MathUtil Instance;

    MathUtil() { }

    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// Singleton Instance
    /// </summary>
    /// <returns></returns>
    public static MathUtil GetInstance()
    {
        if (Instance == null) Instance = new MathUtil();
        return Instance;
    }
    
    /// <summary>
    /// Int형 난수
    /// </summary>
    /// <returns></returns>
    public int RandomInt()
    {
        return new SysRandom().Next();
    }
}
