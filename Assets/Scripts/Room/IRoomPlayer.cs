using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이 가능한 클래스 타입
public enum PlayerClassType { Tanker, Dealer, Healer, Supporter }

// 플레이어 상태
public enum PlayerReadyType { SELECTING, READY }


public interface IRoomPlayer
{
    
    /// <summary>
    /// Get player class
    /// </summary>
    /// <returns>PlayerClassType</returns>
    PlayerClassType GetClassType();
    
    /// <summary>
    /// Get player Ready Type
    /// </summary>
    /// <returns>PlayerReadyType</returns>
    PlayerReadyType GetReadyType();
}
