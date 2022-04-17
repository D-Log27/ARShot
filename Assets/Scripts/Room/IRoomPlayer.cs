using Photon.Pun;
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

    /// <summary>
    /// Player select previous class
    /// </summary>
    public void previousClass();

    /// <summary>
    /// Player select next class
    /// </summary>
    public void NextClass();

    /// <summary>
    /// player ready
    /// </summary>
    public void Ready();

    /// <summary>
    /// player canceled ready
    /// </summary>
    public void ReadyCancel();

    /// <summary>
    /// start Countdown
    /// </summary>
    public void StartCountDown();

    /// <summary>
    /// Set PhotonView
    /// </summary>
    public void SetPhotonView(PhotonView photonView);
}
