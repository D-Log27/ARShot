using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private static RoomManager _instance;

    RoomManager() { }

    public static RoomManager GetInstance()
    {
        if (_instance == null) _instance = new RoomManager();
        return _instance;
    }

    public Transform[] RoomPlayers;

    private PhotonView _photonView;

    //뒤로 가기 버튼
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //InGame 로딩시간
    float sec;

    //값이 변하는 UI가 있는 캔버스 트랜스폼(현재 씬에서는 InGame 로딩 텍스트가 유일)
    public Transform canvasChanging;
  
    private bool isGameSceneLoaded;
    
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        isGameSceneLoaded = false;
        btnBackToTitle.onClick.AddListener(()=>LeaveRoom());
    }

    #region GAME_START
    
    [PunRPC]
    public void MonitoringReadyState()
    {
        bool result = true;
        // TODO : 다른 플레이어들의 ready 상태 확인
        foreach (Transform player in RoomPlayers)
        {
            bool isReadyed = player.GetComponent<IRoomPlayer>().GetReadyType() == PlayerReadyType.READY ? true : false;
            result &= isReadyed;
        }

        if (result) StartCountDown();
        else return;
    }
    
    /// <summary>
    /// 게임 시작 카운트다운
    /// </summary>
    private void StartCountDown()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        loadingGame.text = "Starting in 5 Seconds";
        sec = 5;
        canvasChanging.gameObject.SetActive(true);
        btnBackToTitle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //InGame 로딩 시간 표시 및 다음 씬 연결 함수
        if (canvasChanging.gameObject.activeSelf )
        {
            sec -= Time.deltaTime;
            float count = Mathf.Ceil(sec);
            if(count >= 0) loadingGame.text = "Starting in " + Mathf.Ceil(sec) + " Seconds";

            if (count < 0 && !isGameSceneLoaded)
            {
                PhotonNetwork.LoadLevel("InGame_AL");
                isGameSceneLoaded = true;
            }
        }
    }
    
    #endregion GAME_START

    #region ROOM_OUT
    
    private void LeaveRoom()
    {
        // TODO : RoomPlayer script Initialize;
        
        PhotonNetwork.LeaveRoom();
    }
    
    /// <summary>
    /// 다른유저 방 나간 후 callback
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // TODO : Screen Initialize
        _photonView.RPC("NicknameDisplay",RpcTarget.AllBufferedViaServer);
    }

    /// <summary>
    /// 방 퇴장후 callback
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Title_AL");
    }

    /// <summary>
    /// 방장 변경
    /// </summary>
    /// <param name="newMasterClient"> 새로운 방장정보</param>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (photonView.Owner.ActorNumber == newMasterClient.ActorNumber)
        {
            // TODO : 참여자 re-sorting

            _photonView.RPC("NicknameDisplay",RpcTarget.AllBufferedViaServer);

        }
    }
    
    #endregion ROOM_OUT
    
    


}
