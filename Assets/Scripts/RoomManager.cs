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

public class RoomManager : MonoBehaviourPunCallbacks
{
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
    
    // Start is called before the first frame update
    void Start()
    {
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
        
        NicknameDisplay();

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
                SceneManager.LoadScene("InGame_AL");
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
            if (playerClass[3].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[3], false);
                CanvasGroupOnOff(playerClass[2], true);
                btnNext[0].enabled = true;
            }
            else if (playerClass[2].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[2], false);
                CanvasGroupOnOff(playerClass[1], true);
            }
            else if(playerClass[1].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[1], false);
                CanvasGroupOnOff(playerClass[0], true);
            }
            else // playerClass[0].alpha == 0
            {
                CanvasGroupOnOff(playerClass[0], false);
                CanvasGroupOnOff(playerClass[3], true);
            }
            
            //이거 완전 비활성화라기보단 회색빛으로 나타나게 해야함
            // btnPrevious[0].enabled = false;
        }
        ChangePlayerReadyStatus();
    }

    /// <summary>
    /// 버튼 클릭시 다음 클래스 보임
    /// </summary>
    public void OnClickNext()
    {
        btnClassParentName = EventSystem.current.currentSelectedGameObject?.transform.parent.parent.name;

        if (btnClassParentName == "Player")
        {
            if (playerClass[0].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[0], false);
                CanvasGroupOnOff(playerClass[1], true);
                btnPrevious[0].enabled = true;
            }
            else if (playerClass[1].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[1], false);
                CanvasGroupOnOff(playerClass[2], true);
            }
            else if(playerClass[2].alpha == 1)
            {
                CanvasGroupOnOff(playerClass[2], false);
                CanvasGroupOnOff(playerClass[3], true);
            }
            else // playerClass[3].alpha == 1
            {
                CanvasGroupOnOff(playerClass[3], false);
                CanvasGroupOnOff(playerClass[0], true);
            }
        }
        ChangePlayerReadyStatus();
    }

    /// <summary>
    /// Ready 버튼 클릭시 준비 UI 띄우고 모든 플레이어 준비 완료시 게임 로드 UI 활성화
    /// </summary>
    public void OnClickReady()
    {
        //현재 누른 버튼의 부모의 CanvasGroupComponent: GettingReady Panel의 CanvasGroup
        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

        //현재 누른 버튼의 부모의 부모의 2번째 자식의 CanvasGroupComponent: Ready Panel의 CanvasGroup
        CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();

        CanvasGroupOnOff(cGrp1, false);
        CanvasGroupOnOff(cGrp2, true);

        // TODO : 플레이어 인원수에 따라 조건 다변화
        if (playerReady[0].alpha == 1 && playerReady[1].alpha == 1 && playerReady[2].alpha == 1 && playerReady[3].alpha == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                CanvasGroupOnOff(playerReady[i], false);
                CanvasGroupOnOff(playerReadyed[i], true);
            }

            loadingGame.text = "Starting in 5 Seconds";
            sec = 5;
            canvasChanging.gameObject.SetActive(true);
            btnBackToTitle.gameObject.SetActive(false);
        }
        
        // TODO : PhotonView RPC 호출
    }

    /// <summary>
    /// 준비 취소시 초기 UI 가시화
    /// </summary>
    public void OnClickReadyCancel()
    {
        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        CanvasGroupOnOff(cGrp1, false);
        CanvasGroupOnOff(cGrp2, true);
        // TODO : PhotonView RPC 호출
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
        SceneManager.LoadScene("Title_AL");
        // TODO : 참여자 re-sorting
        NicknameDisplay();
        
    }

    /// <summary>
    /// 다른유저 방 나간 후 callback
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("### other palyer room left");
    }

    /// <summary>
    /// 방장 변경
    /// </summary>
    /// <param name="newMasterClient"> 새로운 방장정보</param>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (photonView.Owner.ActorNumber == newMasterClient.ActorNumber)
        {
            //this.SendKillMessage($"<color=#ffff00>[{photonView.Owner.NickName}]</color>���� �������� ����Ǿ����ϴ�");
        }
    }
    
    /// <summary>
    /// 플레이어 클래스 변경
    /// </summary>
    [PunRPC]
    private void ChangeClass()
    {
        
    }

    /// <summary>
    /// 플레이어 준비상태 변경
    /// </summary>
    /// <param name="isReady"></param>
    [PunRPC]
    private void GamePlayReady(bool isReady)
    {
        // TODO : 각 플레이어 준비상태 플래그 확인.
        
        // TODO : 전부 준비상태라면 -> 카운트다운 시작
    }

    /// <summary>
    /// 플레이어 클래스 선택/준비상태 변경 표시
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    [PunRPC]
    private void ChangePlayerReadyStatus()
    {
        
    }
    
    /// <summary>
    /// 게임 시작 카운트다운
    /// </summary>
    [PunRPC]
    private void StartCountDown()
    {
        PhotonNetwork.LoadLevel("InGame_AL");
    }
}
