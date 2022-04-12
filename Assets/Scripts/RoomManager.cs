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
    //클래스선택 버튼의 부모 이름: 변수명 줄이기 위해 선언함
=======
using Photon.Realtime;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // �뵆�젅�씠 媛��뒫�븳 �겢�옒�뒪 ����엯
    enum PlayerClassType { Tanker, Dealer, Healer, Supporter }

    // �뵆�젅�씠�뼱 �긽�깭
    enum PlayerReadyType { SELECTING, READY }
    
    private PhotonView _photonView;
    
    //�겢�옒�뒪�꽑�깮 踰꾪듉�쓽 遺�紐� �씠由�: 蹂��닔紐� 以꾩씠湲� �쐞�빐 �꽑�뼵�븿
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    string btnClassParentName;

    //뒤로 가기 버튼
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //방제목
    public TMP_Text roomID;
<<<<<<< HEAD

    //클래스 선택 버튼 및 캔버스그룹
    public Button[] btnPrevious;
    public Button[] btnNext;
    public CanvasGroup[] cGrpPrevious;
    public CanvasGroup[] cGrpNext;

    //InGame 로딩 시간
    float sec;

    //현재 클릭한 버튼(Update에서 계속 바뀜)
    GameObject btn;

    //플레이어 준비중, 준비완료 패널
=======
    
    //InGame 濡쒕뵫�떆媛�
    float sec;

    //�뵆�젅�씠�뼱 以�鍮꾩쨷, 以�鍮꾩셿猷� �뙣�꼸
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
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

<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        print(PhotonNetwork.CurrentLobby.Name);
        print(PhotonNetwork.CurrentRoom.Name);
        //초기 설정; unity play 전에 설정 잘 해 놓으면 사실 필요 없음
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
        //珥덇린 �꽕�젙; unity play �쟾�뿉 �꽕�젙 �옒 �빐 �넃�쑝硫� �궗�떎 �븘�슂 �뾾�쓬
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
    /// �엯�옣�떆
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
    /// �떎瑜� �뵆�젅�씠�뼱 諛� �엯�옣�떆
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //TODO :  �뵆�젅�씠�뼱 �엯�옣�떆 �뒳濡�踰덊샇 媛깆떊
        
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
                // 諛⑷툑 �엯�옣�븳 �쑀��� �젙蹂� ����옣
                currentPlayer.Value.CustomProperties["class"] = PlayerClassType.Dealer;
                currentPlayer.Value.CustomProperties["status"] = PlayerReadyType.SELECTING;
            }
        }
        
        // TODO : �쑀��� �젙蹂�
        NicknameDisplay();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //InGame 로딩 시간 표시 및 다음 씬 연결 함수
        if (canvasChanging.gameObject.activeSelf)
=======
        //InGame 濡쒕뵫 �떆媛� �몴�떆 諛� �떎�쓬 �뵮 �뿰寃� �븿�닔
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
    /// 버튼 클릭시 이전 클래스 보임
=======
    /// �겢�옒�뒪 蹂�寃� : �씠�쟾 踰꾪듉 �겢由��떆
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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
    /// 버튼 클릭시 다음 클래스 보임
=======
    /// �겢�옒�뒪 蹂�寃� : �떎�쓬 踰꾪듉 �겢由��떆 
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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

                //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
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
    /// Ready 버튼 클릭시 준비 UI 띄우고 모든 플레이어 준비 완료시 게임 로드 UI 활성화
    /// </summary>
    public void OnClickReady()
    {
<<<<<<< HEAD
        //현재 누른 버튼의 부모의 CanvasGroupComponent: GettingReady Panel의 CanvasGroup
        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

        //현재 누른 버튼의 부모의 부모의 2번째 자식의 CanvasGroupComponent: Ready Panel의 CanvasGroup
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
    /// 준비 취소시 초기 UI 가시화
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
    /// CanvasGroup 설정 변경 간소화
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
    /// TEST�슜 �땳�꽕�엫 �몴�떆湲�. 
    /// </summary>
    [PunRPC]
    private void NicknameDisplay()
    {
        // TODO : �겢�옒�뒪 �몴�떆 �뾾�쓬(�뵆�젅�씠�뼱 �뾾�쓬) -> �겢�옒�뒪 �몴�떆
        
        // TODO : 諛⑹옣�씠 蹂몄씤�씠�씪硫� 蹂몄씤�쓣 泥ル쾲吏� �몴�떆
        
        // TODO : 諛⑹옣�씠 �븘�땲�씪硫� 諛⑹옣�쓣 泥ル쾲吏� �몴�떆

        foreach (var customRoomPlayer in PhotonNetwork.CurrentRoom.Players)
        {
            playerLabelSlots[customRoomPlayer.Key-1].GetComponent<Text>().text = customRoomPlayer.Value.NickName;
        }
    }

    /// <summary>
    /// 諛� �굹媛�湲�
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
    /// �떎瑜몄쑀��� 諛� �굹媛� �썑 callback
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
    /// 諛⑹옣 蹂�寃�
    /// </summary>
    /// <param name="newMasterClient"> �깉濡쒖슫 諛⑹옣�젙蹂�</param>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (photonView.Owner.ActorNumber == newMasterClient.ActorNumber)
        {
            // TODO : 李몄뿬�옄 re-sorting

            // TODO : 李몄뿬�옄 �젙蹂� screen 珥덇린�솕

        }
    }
    
    /// <summary>
    /// �뵆�젅�씠�뼱 �겢�옒�뒪 �봽濡쒗띁�떚 蹂�寃�
    /// </summary>
    /// <param name="player">�뼱�뼡 �뵆�젅�씠�뼱</param>
    /// <param name="classType">�뼱�뼡 �겢�옒�뒪</param>
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
    /// �뵆�젅�씠�뼱 以�鍮꾩긽�깭 �봽濡쒗띁�떚 蹂�寃�
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
    /// �솕硫� 異쒕젰 �룞湲고솕
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
            
            // class canvas 珥덇린�솕
            foreach (CanvasGroup canvas in userClassCanvas)
            {
                CanvasGroupOnOff(canvas, false);
            }
            
            // TODO : �긽�깭�뿉 �뵲�씪 �겢�옒�뒪 �몴�떆 蹂�寃�
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

            
            
            // Ready �몴�떆 蹂�寃�
            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY))
            {
                //�쁽�옱 �늻瑜� 踰꾪듉�쓽 遺�紐⑥쓽 CanvasGroupComponent: GettingReady Panel�쓽 CanvasGroup
                CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

                //�쁽�옱 �늻瑜� 踰꾪듉�쓽 遺�紐⑥쓽 遺�紐⑥쓽 2踰덉㎏ �옄�떇�쓽 CanvasGroupComponent: Ready Panel�쓽 CanvasGroup
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
            
            // 移댁슫�듃�떎�슫 �솗�씤

            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY)) isAllReady *= 1;
            else isAllReady *= 0;
        }

        // �쟾遺� 以�鍮꾩긽�깭�씪硫� -> 移댁슫�듃�떎�슫 �떆�옉
        if (isAllReady == 1) StartCountDown();
        else return;
    }


    /// <summary>
    /// 寃뚯엫 �떆�옉 移댁슫�듃�떎�슫
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
