using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
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

    public GameObject _userInstance;
    
    //뒤로 가기 버튼
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //InGame 로딩시간
    float sec;

    //값이 변하는 UI가 있는 캔버스 트랜스폼(현재 씬에서는 InGame 로딩 텍스트가 유일)
    public Transform canvasChanging;
  
    private bool isGameSceneLoaded;

    public int[] photonViewIdArr = {1001, 2001, 3001, 4001};

    // 유저 닉네임 저장용 collection
    // actorNumber / NickName
    public Dictionary<int, string> nickNameDic;

    // Start is called before the first frame update
    void Start()
    {
        _photonView = this.GetComponent<PhotonView>();
        _instance = this;
        isGameSceneLoaded = false;
        btnBackToTitle.onClick.AddListener(()=>LeaveRoom());
        GameObject userInstance = PhotonNetwork.Instantiate("UserInstance", Vector3.zero, Quaternion.identity,0);
        print($"### PhotonNetwork.CurrentRoom.Players.Count : {PhotonNetwork.CurrentRoom.Players.Count}");
        userInstance.transform.parent = RoomPlayers[PhotonNetwork.CurrentRoom.Players.Count - 1];
        // userInstance.name = "User_" + userInstance.GetPhotonView().ViewID;
        userInstance.transform.localPosition = Vector3.zero;
        
    }

   
    #region GAME_START

    public void ReadyRPC()
    {
        print("### READYRPC CALLED");
        int result = 1;
        // 다른 플레이어들의 ready 상태 확인
        foreach (Transform player in RoomPlayers)
        {
            if (player.GetComponentInChildren<IRoomPlayer>() == null) break;
            int isReadyed = player.GetComponentInChildren<IRoomPlayer>().GetReadyType() == PlayerReadyType.READY ? 1 : 0;
            print($"!!! ready result : {player.GetComponentInChildren<IRoomPlayer>().GetReadyType()} / int :{isReadyed}");
            result *= isReadyed;
        }

        print($"!!! Finally result : {result}");
        if (result == 1)
        {
            //RoomPlayers[0].GetComponentInChildren<RoomPlayer>().StartCountDown();
            // this.GetComponent<PhotonView>().RPC("StartCountDown",RpcTarget.AllBufferedViaServer);
            // foreach (Transform player in RoomPlayers)
            // {
            //     player.GetComponent<IRoomPlayer>().StartCountDown();
            // }
            _photonView.RPC("StartCountDown",RpcTarget.AllBufferedViaServer);
            //StartCountDown();
        }
        else return;
    }

    [PunRPC]
    private void StartCountDown()
    {
        print("### ROOM MANAGER StartCountDown CALLED ");
        PhotonNetwork.CurrentRoom.IsOpen = false;
        // 여기까지 OK
        
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
            print("### WE WILL BE GOTO INGAME_AL");
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

        }
    }
    
    #endregion ROOM_OUT

    #region IN_ROOM
    

    #endregion
    


}
