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
    // 플레이 가능한 클래스 타입
    enum PlayerClassType { Tanker, Dealer, Healer, Supporter }

    private PlayerClassType myClass;

    // 플레이어 상태
    enum PlayerReadyType { SELECTING, READY }

    private PlayerReadyType myReady;
    
    private PhotonView _photonView;
    
    //클래스선택 버튼의 부모 이름: 변수명 줄이기 위해 선언함
    string btnClassParentName;

    //뒤로 가기 버튼
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //방제목
    public TMP_Text roomID;
    
    //InGame 로딩시간
    float sec;

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


    public Transform[] playerLabelSlots;
    

    private bool isGameSceneLoaded;
    
    // Start is called before the first frame update
    void Start()
    {
        myClass = PlayerClassType.Dealer;
        myReady = PlayerReadyType.SELECTING;
        // print($"### LOBBY INFO : {PhotonNetwork.CurrentLobby.Name}");
        // print($"### ROOM INFO : {PhotonNetwork.CurrentRoom.Name}");
        isGameSceneLoaded = false;
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
        _photonView.RPC("StartRoom",RpcTarget.AllBufferedViaServer);
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
        // StartRoom();
    }

    /// <summary>
    /// 입장시
    /// </summary>
    [PunRPC]
    private void StartRoom()
    {
        //sol 1
        // PhotonNetwork.LocalPlayer.CustomProperties["class"] = PlayerClassType.Dealer;
        // PhotonNetwork.LocalPlayer.CustomProperties["status"] = PlayerReadyType.SELECTING;
        //sol 2
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (PhotonNetwork.LocalPlayer.NickName.Equals(player.Value.NickName))
            {
                player.Value.CustomProperties["class"] = myClass;
                player.Value.CustomProperties["status"] = myReady;
            }
        }

        

        // print($"### Lobby : {PhotonNetwork.CurrentLobby.Name}");
        // print($"### Room : {PhotonNetwork.CurrentRoom.Name}");
        // print($"### User NickName : {PhotonNetwork.NickName}");

        photonView.RPC("NicknameDisplay",RpcTarget.AllBufferedViaServer);
    }
    
    /// <summary>
    /// 다른 플레이어 방 입장시
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //TODO :  플레이어 입장시 슬롯번호 갱신
        
        photonView.RPC("UpdateUsersInfo",RpcTarget.AllBufferedViaServer,new object[]{newPlayer});
    }

    /// <summary>
    /// 
    /// </summary>
    [PunRPC]
    private void UpdateUsersInfo(Player player)
    {
        foreach (var currentPlayer in PhotonNetwork.CurrentRoom.Players)
        {
            if (currentPlayer.Value.NickName.Equals(player.NickName))
            {
                // 방금 입장한 유저 정보 저장
                currentPlayer.Value.CustomProperties["class"] = PlayerClassType.Dealer;
                currentPlayer.Value.CustomProperties["status"] = PlayerReadyType.SELECTING;
            }
        }
        
        // TODO : 유저 정보
        NicknameDisplay();
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
    
    /// <summary>
    /// 클래스 변경 : 이전 버튼 클릭시
    /// </summary>
    public void OnClickPrevious()
    {
        int num = 0;
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                num = player.Key;
                break;
            }
        }
        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;
        // print($"### current User : {PhotonNetwork.LocalPlayer.NickName}");
        // print($"### key check : {num}");
        // print($"### btnClassParentName : {btnClassParentName}");
        print($"### btnClassParentName is Exist ? {btnClassParentName}");
        if (btnClassParentName != null && !btnClassParentName.Contains(num.ToString())) return;
        int nowClassIdx = (int)PhotonNetwork.LocalPlayer.CustomProperties["class"];
        // print($"### now class idx {nowClassIdx}");
        switch (nowClassIdx)
        {
            case (int)PlayerClassType.Supporter :
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Healer);
                myClass = PlayerClassType.Healer;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Healer});
                break;
            case (int)PlayerClassType.Healer:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Tanker);
                myClass = PlayerClassType.Tanker;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Tanker});
                break;
            case (int)PlayerClassType.Tanker:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Dealer);
                myClass = PlayerClassType.Dealer;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Dealer});
                break;
            case (int)PlayerClassType.Dealer :
                myClass = PlayerClassType.Supporter;
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Supporter);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Supporter});
                break;
        }
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
    }

    
    /// <summary>
    /// 클래스 변경 : 다음 버튼 클릭시 
    /// </summary>
    public void OnClickNext()
    {
        int num = 0;
        CanvasGroup[] canvasGroups = null;

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                num = player.Key;
                break;
            }
        }
        

        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;
        if (btnClassParentName != null && !btnClassParentName.Contains(num.ToString())) return;
        int nowClassIdx = (int)PhotonNetwork.LocalPlayer.CustomProperties["class"];
        // print($"### now class idx {nowClassIdx}");
        switch (nowClassIdx)
        {
            case (int)PlayerClassType.Supporter :
                //ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Dealer);
                myClass = PlayerClassType.Dealer;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Dealer});
                break;
            case (int)PlayerClassType.Healer:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Supporter);
                myClass = PlayerClassType.Supporter;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Supporter});
                break;
            case (int)PlayerClassType.Tanker:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Healer);
                myClass = PlayerClassType.Healer;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Healer});
                break;
            case (int)PlayerClassType.Dealer :
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Tanker);
                myClass = PlayerClassType.Tanker;
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Tanker});
                break;
                
        }
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
    }

    /// <summary>
    /// Ready 버튼 클릭시 준비 UI 띄우고 모든 플레이어 준비 완료시 게임 로드 UI 활성화
    /// </summary>
    public void OnClickReady()
    {
        int num = 0;

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                print($"### nickname {PhotonNetwork.LocalPlayer.NickName}");
                print($"### num : {player.Key}");
                num = player.Key;
                break;
            }
        }
        print($"### ");
        
        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;
        
        if (btnClassParentName != null && !btnClassParentName.Contains(num.ToString())) return;
        myReady = PlayerReadyType.READY;
        _photonView.RPC("GamePlayReady",RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,true});
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
    }

    /// <summary>
    /// 준비 취소시 초기 UI 가시화
    /// </summary>
    public void OnClickReadyCancel()
    {
        int num = 0;

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                num = player.Key;
                break;
            }
        }

        myReady = PlayerReadyType.SELECTING;
        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;
        if (btnClassParentName != null && !btnClassParentName.Contains(num.ToString())) return;
        _photonView.RPC("GamePlayReady",RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,false});
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
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

    /// <summary>
    /// TEST용 닉네임 표시기. 
    /// </summary>
    [PunRPC]
    private void NicknameDisplay()
    {
        // TODO : 클래스 표시 없음(플레이어 없음) -> 클래스 표시
        
        // TODO : 방장이 본인이라면 본인을 첫번째 표시
        
        // TODO : 방장이 아니라면 방장을 첫번째 표시

        foreach (var customRoomPlayer in PhotonNetwork.CurrentRoom.Players)
        {
            playerLabelSlots[customRoomPlayer.Key-1].GetComponent<Text>().text = customRoomPlayer.Value.NickName;
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
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        PhotonNetwork.LeaveRoom();
        
        
    }

    /// <summary>
    /// 다른유저 방 나간 후 callback
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("### other player room left");
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

            // TODO : 참여자 정보 screen 초기화

        }
    }
    
    /// <summary>
    /// 플레이어 클래스 프로퍼티 변경
    /// </summary>
    /// <param name="player">어떤 플레이어</param>
    /// <param name="classType">어떤 클래스</param>
    [PunRPC]
    private void ChangeClass(Player player, PlayerClassType classType)
    {

        foreach (var currentRoomPlayer in PhotonNetwork.CurrentRoom.Players)
        {
            if (currentRoomPlayer.Value.NickName.Equals(player.NickName))
            {
                currentRoomPlayer.Value.CustomProperties["class"] = classType;
                print($"### {currentRoomPlayer.Value.NickName} select {currentRoomPlayer.Value.CustomProperties["class"]}");
            }

            
        }
            
        ChangeScreen();
    }
    
    /// <summary>
    /// 플레이어 준비상태 프로퍼티 변경
    /// </summary>
    /// <param name="isReady"></param>
    [PunRPC]
    private void GamePlayReady(Player player, bool isReady)
    {
        if (isReady)
        {
            foreach (var currentRoomPlayer in PhotonNetwork.CurrentRoom.Players)
            {
                if(player.NickName.Equals(currentRoomPlayer.Value.NickName)) currentRoomPlayer.Value.CustomProperties["status"] = PlayerReadyType.READY;
            }
        }
        else
        {
            foreach (var currentRoomPlayer in PhotonNetwork.CurrentRoom.Players)
            {
                if(player.NickName.Equals(currentRoomPlayer.Value.NickName)) currentRoomPlayer.Value.CustomProperties["status"] = PlayerReadyType.SELECTING;
            }
        }
        
        ChangeScreen();
    }

    /// <summary>
    /// 화면 출력 동기화
    /// </summary>
    [PunRPC]
    private void ChangeScreen()
    {
        CanvasGroup[] userClassCanvas = null;
        int isAllReady = 1;
        
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            // print($"### player key check :{player.Key}");
            switch (player.Key)
            {
                case 1:
                    userClassCanvas = playerClass;
                    break;
                case 2:
                    userClassCanvas = player_1Class;
                    break;
                case 3:
                    userClassCanvas = player_2Class;
                    break;
                case 4:
                    userClassCanvas = player_3Class;
                    break;
            }

            if (userClassCanvas == null) throw new Exception($"### Key not valid for canvas, player.key:{player.Key}");
            
            // class canvas 초기화
            foreach (CanvasGroup canvas in userClassCanvas)
            {
                CanvasGroupOnOff(canvas, false);
            }
            
            // TODO : 상태에 따라 클래스 표시 변경
            
            switch (player.Value.CustomProperties["class"])
            {
                case PlayerClassType.Dealer:
                    CanvasGroupOnOff(userClassCanvas[0], true);
                    break;
                case PlayerClassType.Tanker:
                    CanvasGroupOnOff(userClassCanvas[1], true);
                    break;
                case PlayerClassType.Healer:
                    CanvasGroupOnOff(userClassCanvas[2], true);
                    break;
                case PlayerClassType.Supporter:
                    CanvasGroupOnOff(userClassCanvas[3], true);
                    break;
                default:
                    print($"### player nickname : {player.Value.NickName}");
                    print($"### player status : {player.Value.CustomProperties["status"]}");
                    SetClassCanvasByLocalVariable(userClassCanvas, myClass);
                    break;
            }

            print($"### User: {player.Value.NickName}");
            print($"### check status from customProperties : {player.Value.CustomProperties["status"]}");
            print($"### check status from variable : {myReady}");
            
            
            // Ready 표시 변경
            CanvasGroup gettingReady = playerGettingReady[player.Key-1];
            CanvasGroup ready = playerReady[player.Key-1];
            if (player.Value.CustomProperties["status"] == null)
            {
                if (player.Value.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
                {
                    switch (myReady)
                    {
                        case PlayerReadyType.READY :
                            CanvasGroupOnOff(gettingReady, false);
                            CanvasGroupOnOff(ready, true);
                            break;
                        case PlayerReadyType.SELECTING:
                            CanvasGroupOnOff(gettingReady, true);
                            CanvasGroupOnOff(ready, false);
                            break;
                    }
                }
            }
            else
            {
                switch (player.Value.CustomProperties["status"])
                {
                    case PlayerReadyType.READY :
                        CanvasGroupOnOff(gettingReady, false);
                        CanvasGroupOnOff(ready, true);
                        break;
                    case PlayerReadyType.SELECTING:
                        CanvasGroupOnOff(gettingReady, true);
                        CanvasGroupOnOff(ready, false);
                        break;
                }
            }
            
            // CanvasGroupOnOff(playerReady[i], false);
            // CanvasGroupOnOff(playerReadyed[i], true);
            
            // 카운트다운 확인
            // TODO : Exception 발생하므로 대비책 준비
            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY)) isAllReady *= 1;
            else isAllReady *= 0;
        }

        // 전부 준비상태라면 -> 카운트다운 시작
        if (isAllReady == 1) StartCountDown();
        else return;
    }

    /// <summary>
    ///  클래스 프로퍼티 미확인시 변수값으로 지정하여 클래스 화면 변경
    /// </summary>
    /// <param name="canvasGroups">유저 캔버스 그룹</param>
    /// <param name="classType">클래스 타입</param>
    /// <exception cref="Exception">클래스타입이 저장되어있지 않은경우</exception>
    private void SetClassCanvasByLocalVariable(CanvasGroup[] canvasGroups, PlayerClassType classType)
    {
        switch (classType)
        {
            case PlayerClassType.Dealer:
                CanvasGroupOnOff(canvasGroups[0], true);
                break;
            case PlayerClassType.Tanker:
                CanvasGroupOnOff(canvasGroups[1], true);
                break;
            case PlayerClassType.Healer:
                CanvasGroupOnOff(canvasGroups[2], true);
                break;
            case PlayerClassType.Supporter:
                CanvasGroupOnOff(canvasGroups[3], true);
                break;
            default:
                throw new Exception($"NOT VALID CLASS : {classType}");
                    
        }
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

        PhotonNetwork.CurrentRoom.IsOpen = false;
        loadingGame.text = "Starting in 5 Seconds";
        sec = 5;
        canvasChanging.gameObject.SetActive(true);
        btnBackToTitle.gameObject.SetActive(false);
    }

}