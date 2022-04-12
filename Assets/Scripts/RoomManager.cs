using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
<<<<<<< HEAD

public class RoomManager : MonoBehaviourPunCallbacks
{
    //Ŭ�������� ��ư�� �θ� �̸�: ������ ���̱� ���� ������
=======
using Photon.Realtime;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // 플레이 가능한 클래스 타입
    enum PlayerClassType { Tanker, Dealer, Healer, Supporter }

    // 플레이어 상태
    enum PlayerReadyType { SELECTING, READY }
    
    private PhotonView _photonView;
    
    //클래스선택 버튼의 부모 이름: 변수명 줄이기 위해 선언함
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    string btnClassParentName;

    //�ڷ� ���� ��ư
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //������
    public TMP_Text roomID;
<<<<<<< HEAD

    //Ŭ���� ���� ��ư �� ĵ�����׷�
    public Button[] btnPrevious;
    public Button[] btnNext;
    public CanvasGroup[] cGrpPrevious;
    public CanvasGroup[] cGrpNext;

    //InGame �ε� �ð�
    float sec;

    //���� Ŭ���� ��ư(Update���� ��� �ٲ�)
    GameObject btn;

    //�÷��̾� �غ���, �غ�Ϸ� �г�
=======
    
    //InGame 로딩시간
    float sec;

    //플레이어 준비중, 준비완료 패널
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    public CanvasGroup[] playerGettingReady;
    public CanvasGroup[] playerReady;

    //���� �ε�� �÷��̾� �г�
    public CanvasGroup[] playerReadyed;

    //Ŭ���� ����
    public CanvasGroup[] playerClass;
    public CanvasGroup[] player_1Class;
    public CanvasGroup[] player_2Class;
    public CanvasGroup[] player_3Class;

    //���� ���ϴ� UI�� �ִ� ĵ���� Ʈ������(���� �������� InGame �ε� �ؽ�Ʈ�� ����)
    public Transform canvasChanging;

<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        print(PhotonNetwork.CurrentLobby.Name);
        print(PhotonNetwork.CurrentRoom.Name);
        //�ʱ� ����; unity play ���� ���� �� �� ������ ��� �ʿ� ����
=======

    public Transform[] playerLabelSlots;
    

    private bool isGameSceneLoaded;
    
    // Start is called before the first frame update
    void Start()
    {
        // print($"### LOBBY INFO : {PhotonNetwork.CurrentLobby.Name}");
        // print($"### ROOM INFO : {PhotonNetwork.CurrentRoom.Name}");
        isGameSceneLoaded = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _photonView = GetComponent<PhotonView>();
        //초기 설정; unity play 전에 설정 잘 해 놓으면 사실 필요 없음
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
        canvasChanging.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            CanvasGroupOnOff(playerGettingReady[i], On);
            CanvasGroupOnOff(playerReady[i], Off);
            CanvasGroupOnOff(playerReadyed[i], Off);
        }
<<<<<<< HEAD
=======
        _photonView.RPC("StartRoom",RpcTarget.AllBufferedViaServer);
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
            print($"### player nick : {player.Value.NickName}");
            print($"### player class : {player.Value.CustomProperties["class"]}");
            print($"### player status : {player.Value.CustomProperties["status"]}");
            
            if (PhotonNetwork.LocalPlayer.NickName.Equals(player.Value.NickName))
            {
                player.Value.CustomProperties["class"] = PlayerClassType.Dealer;
                player.Value.CustomProperties["status"] = PlayerReadyType.SELECTING;
            }
        }

        

        // print($"### Lobby : {PhotonNetwork.CurrentLobby.Name}");
        // print($"### Room : {PhotonNetwork.CurrentRoom.Name}");
        // print($"### User NickName : {PhotonNetwork.NickName}");

        photonView.RPC("NicknameDisplay",RpcTarget.AllBufferedViaServer);
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
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
<<<<<<< HEAD
        //InGame �ε� �ð� ǥ�� �� ���� �� ���� �Լ�
        if (canvasChanging.gameObject.activeSelf)
=======
        //InGame 로딩 시간 표시 및 다음 씬 연결 함수
        if (canvasChanging.gameObject.activeSelf )
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
        {
            sec -= Time.deltaTime;
            float count = Mathf.Ceil(sec);
            if(count >= 0) loadingGame.text = "Starting in " + Mathf.Ceil(sec) + " Seconds";

            if (count < 0 && !isGameSceneLoaded)
            {
<<<<<<< HEAD
                SceneManager.LoadScene("InGame_AL");
=======
                PhotonNetwork.LoadLevel("InGame_AL");
                isGameSceneLoaded = true;
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
            }
        }
    }
    
    /// <summary>
<<<<<<< HEAD
    /// ��ư Ŭ���� ���� Ŭ���� ����
=======
    /// 클래스 변경 : 이전 버튼 클릭시
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    /// </summary>
    public void OnClickPrevious()
    {
        int num = 0;
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
<<<<<<< HEAD
            if (playerClass[3].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[3], Off);
                CanvasGroupOnOff(playerClass[2], On);
                btnNext[0].enabled = true;
            }
            else if (playerClass[2].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[2], Off);
                CanvasGroupOnOff(playerClass[1], On);
            }
            else
            {
                CanvasGroupOnOff(playerClass[1], Off);
                CanvasGroupOnOff(playerClass[0], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnPrevious[0].enabled = false;
            }
        }
        else if (btnClassParentName == "Player_1")
        {
            if (player_1Class[3].alpha == 1)
            {
                CanvasGroupOnOff(player_1Class[3], Off);
                CanvasGroupOnOff(player_1Class[2], On);
                btnNext[1].enabled = true;
            }
            else if (player_1Class[2].alpha == 1)
            {
                CanvasGroupOnOff(player_1Class[2], Off);
                CanvasGroupOnOff(player_1Class[1], On);
            }
            else
            {
                CanvasGroupOnOff(player_1Class[1], Off);
                CanvasGroupOnOff(player_1Class[0], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnPrevious[1].enabled = false;
            }
        }
        else if (btnClassParentName == "Player_2")
        {
            if (player_2Class[3].alpha == 1)
            {
                CanvasGroupOnOff(player_2Class[3], Off);
                CanvasGroupOnOff(player_2Class[2], On);
                btnNext[2].enabled = true;
            }
            else if (player_2Class[2].alpha == 1)
            {
                CanvasGroupOnOff(player_2Class[2], Off);
                CanvasGroupOnOff(player_2Class[1], On);
            }
            else
            {
                CanvasGroupOnOff(player_2Class[1], Off);
                CanvasGroupOnOff(player_2Class[0], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnPrevious[2].enabled = false;
            }
        }
        else
        {
            if (player_3Class[3].alpha == 1)
            {
                CanvasGroupOnOff(player_3Class[3], Off);
                CanvasGroupOnOff(player_3Class[2], On);
                btnNext[3].enabled = true;
            }
            else if (player_3Class[2].alpha == 1)
            {
                CanvasGroupOnOff(player_3Class[2], Off);
                CanvasGroupOnOff(player_3Class[1], On);
            }
            else
            {
                CanvasGroupOnOff(player_3Class[1], Off);
                CanvasGroupOnOff(player_3Class[0], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnPrevious[3].enabled = false;
            }
        }
=======
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
        print($"### Before Click, class : {PhotonNetwork.LocalPlayer.CustomProperties["class"]}");
        print($"### Before Click, reday state : {PhotonNetwork.LocalPlayer.CustomProperties["status"]}");
        int nowClassIdx = (int)PhotonNetwork.LocalPlayer.CustomProperties["class"];
        // print($"### now class idx {nowClassIdx}");
        switch (nowClassIdx)
        {
            case (int)PlayerClassType.Supporter :
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Healer);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Healer});
                break;
            case (int)PlayerClassType.Healer:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Tanker);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Tanker});
                break;
            case (int)PlayerClassType.Tanker:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Dealer);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Dealer});
                break;
            case (int)PlayerClassType.Dealer :
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Supporter);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Supporter});
                break;
        }
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    }

    
    /// <summary>
<<<<<<< HEAD
    /// ��ư Ŭ���� ���� Ŭ���� ����
=======
    /// 클래스 변경 : 다음 버튼 클릭시 
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    /// </summary>
    public void OnClickNext()
    {
        int num = 0;
        CanvasGroup[] canvasGroups = null;

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
<<<<<<< HEAD
            if (playerClass[0].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[0], Off);
                CanvasGroupOnOff(playerClass[1], On);
                btnPrevious[0].enabled = true;
            }
            else if (playerClass[1].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[1], Off);
                CanvasGroupOnOff(playerClass[2], On);
            }
            else
            {
                CanvasGroupOnOff(playerClass[2], Off);
                CanvasGroupOnOff(playerClass[3], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnNext[0].enabled = false;
            }
        }
        else if (btnClassParentName == "Player_1")
        {
            if (player_1Class[0].alpha == 1)
            {
                CanvasGroupOnOff(player_1Class[0], Off);
                CanvasGroupOnOff(player_1Class[1], On);
                btnPrevious[1].enabled = true;
            }
            else if (player_1Class[1].alpha == 1)
            {
                CanvasGroupOnOff(player_1Class[1], Off);
                CanvasGroupOnOff(player_1Class[2], On);
            }
            else
            {
                CanvasGroupOnOff(player_1Class[2], Off);
                CanvasGroupOnOff(player_1Class[3], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnNext[1].enabled = false;
            }
        }
        else if (btnClassParentName == "Player_2")
        {
            if (player_2Class[0].alpha == 1)
            {
                CanvasGroupOnOff(player_2Class[0], Off);
                CanvasGroupOnOff(player_2Class[1], On);
                btnPrevious[2].enabled = true;
            }
            else if (player_2Class[1].alpha == 1)
            {
                CanvasGroupOnOff(player_2Class[1], Off);
                CanvasGroupOnOff(player_2Class[2], On);
            }
            else
            {
                CanvasGroupOnOff(player_2Class[2], Off);
                CanvasGroupOnOff(player_2Class[3], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnNext[2].enabled = false;
            }
        }
        else
        {
            if (player_3Class[0].alpha == 1)
            {
                CanvasGroupOnOff(player_3Class[0], Off);
                CanvasGroupOnOff(player_3Class[1], On);
                btnPrevious[3].enabled = true;
            }
            else if (player_3Class[1].alpha == 1)
            {
                CanvasGroupOnOff(player_3Class[1], Off);
                CanvasGroupOnOff(player_3Class[2], On);
            }
            else
            {
                CanvasGroupOnOff(player_3Class[2], Off);
                CanvasGroupOnOff(player_3Class[3], On);

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnNext[3].enabled = false;
=======
            if (player.Value.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                num = player.Key;
                break;
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
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
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Dealer});
                break;
            case (int)PlayerClassType.Healer:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Supporter);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Supporter});
                break;
            case (int)PlayerClassType.Tanker:
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Healer);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Healer});
                break;
            case (int)PlayerClassType.Dealer :
                // ChangeClass(PhotonNetwork.LocalPlayer,PlayerClassType.Tanker);
                _photonView.RPC("ChangeClass", RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,PlayerClassType.Tanker});
                break;
                
        }
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
    }

    /// <summary>
    /// Ready ��ư Ŭ���� �غ� UI ���� ��� �÷��̾� �غ� �Ϸ�� ���� �ε� UI Ȱ��ȭ
    /// </summary>
    public void OnClickReady()
    {
<<<<<<< HEAD
        //���� ���� ��ư�� �θ��� CanvasGroupComponent: GettingReady Panel�� CanvasGroup
        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

        //���� ���� ��ư�� �θ��� �θ��� 2��° �ڽ��� CanvasGroupComponent: Ready Panel�� CanvasGroup
        CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();

        CanvasGroupOnOff(cGrp1, Off);
        CanvasGroupOnOff(cGrp2, On);

        if (playerReady[0].alpha == 1 && playerReady[1].alpha == 1 && playerReady[2].alpha == 1 && playerReady[3].alpha == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                CanvasGroupOnOff(playerReady[i], Off);
                CanvasGroupOnOff(playerReadyed[i], On);
            }

            loadingGame.text = "Starting in 5 Seconds";
            sec = 5;
            canvasChanging.gameObject.SetActive(true);
            btnBackToTitle.gameObject.SetActive(false);
        }
=======
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
        _photonView.RPC("GamePlayReady",RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,true});
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    }

    /// <summary>
    /// �غ� ��ҽ� �ʱ� UI ����ȭ
    /// </summary>
    public void OnClickReadyCancel()
    {
<<<<<<< HEAD
        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        CanvasGroupOnOff(cGrp1, Off);
        CanvasGroupOnOff(cGrp2, On);
    }

    public void OnClickBackToTitle()
    {
        //PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene("Title_Test");
        SceneManager.LoadScene("Title_AL");
=======
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
        if (btnClassParentName != null && !btnClassParentName.Contains(num.ToString())) return;
        _photonView.RPC("GamePlayReady",RpcTarget.AllBufferedViaServer,new System.Object[]{PhotonNetwork.LocalPlayer,false});
        _photonView.RPC("ChangeScreen",RpcTarget.AllBufferedViaServer);
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    }

    /// <summary>
    /// CanvasGroup ���� ���� ����ȭ
    /// </summary>
    bool CanvasGroupOnOff(CanvasGroup cGrp, String OnOff)
    {
        if (OnOff == On)
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
<<<<<<< HEAD
    string On = "On"; string Off = "Off";
=======

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
        _photonView.RPC("NicknameDisplay",RpcTarget.AllBufferedViaServer);
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
    /// 플레이어 클래스 프로퍼티 변경
    /// </summary>
    /// <param name="player">어떤 플레이어</param>
    /// <param name="classType">어떤 클래스</param>
    [PunRPC]
    private void ChangeClass(Player player, PlayerClassType classType)
    {

        foreach (var currentRoomPlayer in PhotonNetwork.CurrentRoom.Players)
        {
            if(currentRoomPlayer.Value.NickName.Equals(player.NickName)) currentRoomPlayer.Value.CustomProperties["class"] = classType;
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
            print($"### player key check :{player.Key}");
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
            print($"### selected class : {player.Value.CustomProperties["class"]}");
            
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
                    throw new Exception($"NOT VALID CLASS ENUM :{player.Value.CustomProperties["class"]}");
                    
            }

            
            
            // Ready 표시 변경
            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY))
            {
                //현재 누른 버튼의 부모의 CanvasGroupComponent: GettingReady Panel의 CanvasGroup
                CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

                //현재 누른 버튼의 부모의 부모의 2번째 자식의 CanvasGroupComponent: Ready Panel의 CanvasGroup
                CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
            
                CanvasGroupOnOff(cGrp1, false);
                CanvasGroupOnOff(cGrp2, true);
            }
            else
            {
                CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
                CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
                CanvasGroupOnOff(cGrp1, false);
                CanvasGroupOnOff(cGrp2, true);
            }
            
            
            
            // CanvasGroupOnOff(playerReady[i], false);
            // CanvasGroupOnOff(playerReadyed[i], true);
            
            // 카운트다운 확인

            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY)) isAllReady *= 1;
            else isAllReady *= 0;
        }

        // 전부 준비상태라면 -> 카운트다운 시작
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

>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
}
