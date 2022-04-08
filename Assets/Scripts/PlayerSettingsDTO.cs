using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsDTO : MonoBehaviour
{
    //PlayerSettingsDTO 싱글톤
    public static PlayerSettingsDTO instance;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 플레이어 ID
    /// </summary>
    public string playerID { get; set; }

    /*
     * Audio
     * Music: 진짜 노래
     * Ambient: 배경음(시대, 환경 등등을 알려줌)
     * SFX: 특수효과
     */

    /// <summary>
    /// 브금 볼륨 크기
    /// </summary>
    public int bgmVolume { get; set; }

    /// <summary>
    /// 특수효과 볼륨
    /// </summary>
    public int sfxVolume { get; set; }

    /// <summary>
    /// 플레이어의 현재 Scene 정보
    /// </summary>
    public string playerScene { get; set; }

    /// <summary>
    /// 화면 밝기
    /// </summary>
    public float brightness { get; set; }
}
