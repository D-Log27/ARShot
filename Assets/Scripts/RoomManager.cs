using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.UIElements;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // 플레이 가능한 클래스 타입
    enum PlayerClassType { Tanker, Dealer, Healer, Supporter }

    // 플레이어 상태
    enum PlayerReadyType { SELECTING, READY }
    
    // 플레이어 선택한 클래스
    private PlayerClassType _playerClass;
    
    // 플레이어 상태
    private PlayerReadyType _playerReadyType;

    private PhotonHashTable playerInfo;
    
    private PhotonView _photonView;
    
    //클래스선택 버튼의 부모 이름: 변수명 줄이기 위해 선언함
    string btnClassParentName;

    //뒤로 가기 버튼
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //방제목
    public TMP_Text roomID;

    //클래스 선택 버튼 및 캔버스그룹
    public Button[] btnPrevious;
    public Button[] btnNext;
    public CanvasGroup[] cGrpPrevious;
    public CanvasGroup[] cGrpNext;

    //InGame 로딩시간
    float sec;

    //현재 클릭한 버튼(Update에서 계속 바뀜)
    GameObject btn;

    //플레이어 준비중, 준비완료 패널
    public CanvasGroup[] playerGettingReady;
    public CanvasGroup[] playerReady;

    //게임 로드시 플레이어 패널
    public CanvasGroup[] playerReadyed;

    //클래스 선택
    public CanvasGroup[] playerClass;
    public CanvasGroup[] player_1Class;
    public CanvasGroup[] player_2Class;
    public CanvasGroup[] player_3Class;

    //값이 변하는 UI가 있는 캔버스 트랜스폼(현재 씬에서는 InGame 로딩 텍스트가 유일)
    public Transform canvasChanging;

    // FOR TEST
    public Transform player1_nickname;
    public Transform player2_nickname;
    
    // 방 접속자 정보
    private List<Player> userList;
    
    // Start is called before the first frame update
    void Start()
    {
        userList = new List<Player>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _photonView = GetComponent<PhotonView>();
        //초기 설정; unity play 전에 설정 잘 해 놓으면 사실 필요 없음
        canvasChanging.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            CanvasGroupOnOff(playerGettingReady[i], true);
            CanvasGroupOnOff(playerReady[i], false);
            CanvasGroupOnOff(playerReadyed[i], false);
        }

        StartRoom();
    }

    /// <summary>
    /// 입장시
    /// </summary>
    private void StartRoom()
    {
        print($"### Lobby : {PhotonNetwork.CurrentLobby.Name}");
        print($"### Room : {PhotonNetwork.CurrentRoom.Name}");
        print($"### User NickName : {PhotonNetwork.NickName}");
        
        Dictionary<int, Player> _players = PhotonNetwork.CurrentRoom.Players;
        print($"### players count in room : {_players.Count}");

        playerInfo = new PhotonHashTable();        
        // Hashtable<string, int> playerInfo = new Hashtable<string, int>();
        playerInfo.Add("class", PlayerClassType.Dealer);
        playerInfo.Add("status",PlayerReadyType.SELECTING);
        PhotonNetwork.LocalPlayer.CustomProperties = playerInfo;
        
        photonView.RPC("NicknameDisplay",RpcTarget.AllBufferedViaServer);
    }

    // Update is called once per frame
    void Update()
    {
        //InGame 로딩 시간 표시 및 다음 씬 연결 함수
        if (canvasChanging.gameObject.activeSelf)
        {
            sec -= Time.deltaTime;
            loadingGame.text = "Starting in " + Mathf.Ceil(sec) + " Seconds";
            if (sec < 0)
            {
                PhotonNetwork.LoadLevel("InGame_AL");
            }
        }
    }

    /// <summary>
    /// 버튼 클릭시 이전 클래스 보임
    /// </summary>
    public void OnClickPrevious()
    {
        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;

        if (btnClassParentName == "Player")
        {
            int nowClassIdx = (int)PhotonNetwork.LocalPlayer.CustomProperties["class"];
            switch (nowClassIdx)
            {
                case (int)PlayerClassType.Supporter :
                    print("### dealder <- supporter");
                    ChangeClass(PlayerClassType.Healer);
                    break;
                case (int)PlayerClassType.Healer:
                    ChangeClass(PlayerClassType.Tanker);
                    print("### Supporter <- Healer");
                    break;
                case (int)PlayerClassType.Tanker:
                    ChangeClass(PlayerClassType.Dealer);
                    print("### Healer <- Tanker");
                    break;
                case (int)PlayerClassType.Dealer :
                    ChangeClass(PlayerClassType.Supporter);
                    print("### dealder <- supporter");
                    break;
                
            }
            
            //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
            // btnPrevious[0].enabled = false;
        }
        
    }

    /// <summary>
    /// 버튼 클릭시 다음 클래스 보임
    /// </summary>
    public void OnClickNext()
    {
        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;

        if (btnClassParentName == "Player")
        {
            int nowClassIdx = (int)PhotonNetwork.LocalPlayer.CustomProperties["class"];
            switch (nowClassIdx)
            {
                case (int)PlayerClassType.Supporter :
                    ChangeClass(PlayerClassType.Dealer);
                    break;
                case (int)PlayerClassType.Healer:
                    ChangeClass(PlayerClassType.Supporter);
                    break;
                case (int)PlayerClassType.Tanker:
                    ChangeClass(PlayerClassType.Healer);
                    break;
                case (int)PlayerClassType.Dealer :
                    ChangeClass(PlayerClassType.Tanker);
                    break;
                
            }
            
        }
    }

    /// <summary>
    /// Ready 버튼 클릭시 준비 UI 띄우고 모든 플레이어 준비 완료시 게임 로드 UI 활성화
    /// </summary>
    public void OnClickReady()
    {
        // btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;
        //
        // if (btnClassParentName == "Player")
        // {
        //     //현재 누른 버튼의 부모의 CanvasGroupComponent: GettingReady Panel의 CanvasGroup
        //     CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        //
        //     //현재 누른 버튼의 부모의 부모의 2번째 자식의 CanvasGroupComponent: Ready Panel의 CanvasGroup
        //     CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        //
        //     CanvasGroupOnOff(cGrp1, false);
        //     CanvasGroupOnOff(cGrp2, true);
            GamePlayReady(true);
        // }
        
    }

    /// <summary>
    /// 준비 취소시 초기 UI 가시화
    /// </summary>
    public void OnClickReadyCancel()
    {
        // CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        // CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        // CanvasGroupOnOff(cGrp1, false);
        // CanvasGroupOnOff(cGrp2, true);
        GamePlayReady(false);
    }

    

    /// <summary>
    /// CanvasGroup 설정 변경 간소화
    /// </summary>
    bool CanvasGroupOnOff(CanvasGroup cGrp, bool isOn)
    {
        if (isOn)
        {
            cGrp.alpha = 1;
            cGrp.interactable = true;
            cGrp.blocksRaycasts = true;
            return true;
        }
        else
        {
            cGrp.alpha = 0;
            cGrp.interactable = false;
            cGrp.blocksRaycasts = false;
            return false;
        }
    }
    string On = "On"; string Off = "Off";

    /// <summary>
    /// 플레이어 방 입장시
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print($"### Lobby : {PhotonNetwork.CurrentLobby.Name}");
        print($"### Room : {PhotonNetwork.CurrentRoom.Name}");
        print($"### User NickName : {PhotonNetwork.NickName}");
        
        print($"### New Player Enter Room : {newPlayer.NickName}");
        Dictionary<int, Player> _players = PhotonNetwork.CurrentRoom.Players;
        print($"### players count in room : {_players.Count}");

        NicknameDisplay();

    }
    
    /// <summary>
    /// TEST용 닉네임 표시기. 
    /// </summary>
    [PunRPC]
    private void NicknameDisplay()
    {
        // TODO : 클래스 표시 없음(플레이어 없음) -> 클래스 표시
        
        // TODO : 방장이 본인이라면 본인을 첫번째 표시
        
        // TODO : 방장이 아니라면 방장을 첫번째 표시
        Dictionary<int, Player> _players = PhotonNetwork.CurrentRoom.Players;
        print($"### players count in room : {_players.Count}");
        print($"### masterClientId : {PhotonNetwork.CurrentRoom.masterClientId}");

        foreach (var _player in _players)
        {
            
            print($" ### Player nickname check : {_player.Value.NickName}");
            if (PhotonNetwork.NickName.Equals(_player.Value.NickName))
            {
                // TODO : 플레이어 자신은 가장 왼쪽에 표기
                print("### This is ME!");
                player1_nickname.GetComponent<Text>().text = _player.Value.NickName;
            }
            else
            {
                // TODO : 다른 플레이어는 2~4번째에 차례대로 표시
                print($"### This is not me, he/she is {_player.Value.NickName}");
                player2_nickname.GetComponent<Text>().text = _player.Value.NickName;
            }
            
            
        }
    }

    /// <summary>
    /// 방 나가기
    /// </summary>                                                                                      
    public void OnClickBackToTitle()
    {
        //PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene("Title_Test");
        // SceneManager.LoadScene("Title_AL");
        
        PhotonNetwork.LeaveRoom();
        
    }

    /// <summary>
    /// 다른유저 방 나간 후 callback
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("### other player room left");
        // TODO : 참여자 re-sorting
        NicknameDisplay();
    }

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

            // TODO : 참여자 정보 screen 초기화

        }
    }
    
    /// <summary>
    /// 플레이어 클래스 변경
    /// </summary>
    private void ChangeClass(PlayerClassType classType)
    {
        Player thisPlayer = PhotonNetwork.LocalPlayer;
        PhotonHashTable temp = thisPlayer.CustomProperties;
        temp["class"] = classType;
        PhotonNetwork.LocalPlayer.CustomProperties = temp;
        
        _photonView.RPC("ChangePlayersStatus", RpcTarget.AllBufferedViaServer, new System.Object[] {thisPlayer,temp});
    }
    
    

    /// <summary>
    /// 플레이어 준비상태 변경
    /// </summary>
    /// <param name="isReady"></param>
    private void GamePlayReady(bool isReady)
    {
        Player thisPlayer = PhotonNetwork.LocalPlayer;
        PhotonHashTable temp = thisPlayer.CustomProperties;
        if (isReady) temp["status"] = PlayerReadyType.READY;
        else temp["status"] = PlayerReadyType.SELECTING;
        
        
        

        
        _photonView.RPC("ChangePlayersStatus", RpcTarget.AllBufferedViaServer, new System.Object[] {thisPlayer,temp});
    }

    /// <summary>
    /// 플레이어 상태 RPC 연동
    /// </summary>
    /// <param name="player"></param>
    /// <param name="hash"></param>
    [PunRPC]
    private void ChangePlayersStatus(Player player, PhotonHashTable hash)
    {
        
        // 플레이어 상태를 모두에게 적용
        foreach (Player _player in PhotonNetwork.PlayerList)
        {
            if (player.Equals(_player)) _player.CustomProperties = hash;
        }
        // TODO : 해당하는 플레이어의 상태를 화면에 출력
        ChangeScreen();
    }

    private void ChangeScreen()
    {
        int isAllReady = 1;
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            // class canvas 초기화
            foreach (CanvasGroup canvas in playerClass)
            {
                CanvasGroupOnOff(canvas, false);
            }
            
            // TODO : 상태에 따라 클래스 표시 변경
            
            switch (player.Value.CustomProperties["class"])
            {
                case (int)PlayerClassType.Dealer:
                    CanvasGroupOnOff(playerClass[0], true);
                    break;
                case (int)PlayerClassType.Tanker:
                    CanvasGroupOnOff(playerClass[1], true);
                    break;
                case (int)PlayerClassType.Healer:
                    CanvasGroupOnOff(playerClass[2], true);
                    break;
                case (int)PlayerClassType.Supporter:
                    CanvasGroupOnOff(playerClass[3], true);
                    break;
            }
            
            // TODO : 상태 따라 Ready 표시 변경
            switch (player.Value.CustomProperties["status"])
            {
                case PlayerReadyType.READY:
                    btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;

                    if (btnClassParentName == "Player")
                    {
                        //현재 누른 버튼의 부모의 CanvasGroupComponent: GettingReady Panel의 CanvasGroup
                        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

                        //현재 누른 버튼의 부모의 부모의 2번째 자식의 CanvasGroupComponent: Ready Panel의 CanvasGroup
                        CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();

                        CanvasGroupOnOff(cGrp1, false);
                        CanvasGroupOnOff(cGrp2, true);
                    }
                    break;
                case PlayerReadyType.SELECTING:
                    btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;

                    if (btnClassParentName == "Player")
                    {
                        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
                        CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
                        CanvasGroupOnOff(cGrp1, false);
                        CanvasGroupOnOff(cGrp2, true);
                    }                         

                    break;
            }
            
    
            // CanvasGroupOnOff(playerReady[i], false);
            // CanvasGroupOnOff(playerReadyed[i], true);
            
            // 한명이라도 준비상태가 있다면 카운트다운을 하지 않는다

            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY)) isAllReady *= 1;
            else isAllReady *= 0;
        }

        // TODO : 전부 준비상태라면 -> 카운트다운 시작
        if (isAllReady == 1) StartCountDown();
        else return;
    }


    /// <summary>
    /// 게임 시작 카운트다운
    /// </summary>
    private void StartCountDown()
    {
        // for (int i = 0; i < 4; i++)
        // {
        //     CanvasGroupOnOff(playerReady[i], false);
        //     CanvasGroupOnOff(playerReadyed[i], true);
        // }

        loadingGame.text = "Starting in 5 Seconds";
        sec = 5;
        canvasChanging.gameObject.SetActive(true);
        btnBackToTitle.gameObject.SetActive(false);
        
    }
}
