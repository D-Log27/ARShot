using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomPlayer : MonoBehaviourPunCallbacks, IRoomPlayer, IPunObservable
{
    [HideInInspector]
    public PlayerClassType myClass;
    [HideInInspector]
    public PlayerReadyType myReady;
    private Player myPlayer;

    // Buttons
    public Button previousClassBtn;
    public Button nextClassBtn;
    public Button readyBtn;
    public Button cancelBtn;

    // Classes
    public Transform classesTransform;

    private PhotonView _photonViewInstance;

    // NickName
    public Text nickName;

    public bool isReady;
    
    
    //뒤로 가기 버튼
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;
    
    //InGame 로딩시간
    float sec = 5f;

    //값이 변하는 UI가 있는 캔버스 트랜스폼(현재 씬에서는 InGame 로딩 텍스트가 유일)
    public Transform canvasChanging;
    
  
    private bool isGameSceneLoaded;
    
    public int[] playerNumArr = {1001, 2001, 3001, 4001};
    
    // Start is called before the first frame update
    void Start()
    {
        print($"### ROOM PLAYER START");
        isGameSceneLoaded = false;
        Initialize();
        CheckSlotPlayer();
        previousClassBtn.onClick.AddListener(()=>_photonViewInstance.RPC("previousClass",RpcTarget.AllBufferedViaServer)); 
        nextClassBtn.onClick.AddListener(()=>_photonViewInstance.RPC("NextClass",RpcTarget.AllBufferedViaServer));
        readyBtn.onClick.AddListener(()=>_photonViewInstance.RPC("Ready",RpcTarget.AllBufferedViaServer));
        cancelBtn.onClick.AddListener(()=>_photonViewInstance.RPC("ReadyCancel",RpcTarget.AllBufferedViaServer));

        // Hashtable userProperties = new Hashtable();
        // print($"### current room member count :{PhotonNetwork.CurrentRoom.Players.Count}");
        // userProperties.Add("Number",PhotonNetwork.CurrentRoom.Players.Count);
        // userProperties.Add("NickName",PhotonNetwork.LocalPlayer.NickName);
        // PhotonNetwork.LocalPlayer.CustomProperties = userProperties;
        
        print($"### CUSTOM PROPERTY CHECK : {PhotonNetwork.LocalPlayer.CustomProperties["Number"]}");
        print($"### CUSTOM PROPERTY CHECK : {PhotonNetwork.LocalPlayer.CustomProperties["NickName"]}");
        
        // ! CAUTION !
        // PhotonNetwork Instatiate한 직후에 위치나 정보를 수정하는것은 옳지않다.
        // RPC로 다른 PhotonView 객체에게 전달하는 시간이 존재하므로
        // PhotonView가 전달 받고나서 생성되는 때(Start)에 생성해야 정상적으로 위치가 지정된다.
        //this.gameObject.name = "User_" + this.GetComponent<PhotonView>().ViewID;
        SetView();
    }
 
    private void SetView()
    {

        this.transform.GetComponentInChildren<Text>().text = GetComponent<PhotonView>().Owner.NickName;
        switch (GetComponent<PhotonView>().ViewID)
        {
            case 1001:
                this.gameObject.transform.parent = RoomManager.GetInstance().RoomPlayers[0];
                this.gameObject.transform.localPosition = Vector3.zero;
                
                break;
            case 2001:
                this.gameObject.transform.parent = RoomManager.GetInstance().RoomPlayers[1];
                this.gameObject.transform.localPosition = Vector3.zero;
                break;
            case 3001:
                this.gameObject.transform.parent = RoomManager.GetInstance().RoomPlayers[2];
                this.gameObject.transform.localPosition = Vector3.zero;
                break;
            case 4001:
                this.gameObject.transform.parent = RoomManager.GetInstance().RoomPlayers[3];
                this.gameObject.transform.localPosition = Vector3.zero;
                break;
        }


    }
    
    

    private void Initialize()
    {
        isReady = false;
        // UI 닉네임 초기화
        nickName.text = "No PLAYER";
        // UI 클래스 초기화
        for (int i = 0; i < classesTransform.childCount; i++)
        {
            if (classesTransform.GetChild(i).name.Equals("Dealer"))
            {
                CanvasGroupOnOff(classesTransform.GetChild(i).GetComponent<CanvasGroup>(), true);
            }
            else CanvasGroupOnOff(classesTransform.GetChild(i).GetComponent<CanvasGroup>(), false);
        }
        // TODO : UI 레디 초기화
        // variable 클래스 초기화
        myClass = PlayerClassType.Dealer;
        // variable 레디 초기화
        myReady = PlayerReadyType.SELECTING;
        // variable 플레이어 초기화
        myPlayer = null;

    }
    
    private void CheckSlotPlayer()
    {
        //print($"### CHECK PRINT :{this.GetComponent<PhotonView>().IsMine}");
        if (this.GetComponent<PhotonView>().IsMine)
        {
            myPlayer = PhotonNetwork.LocalPlayer;
            nickName.text = PhotonNetwork.LocalPlayer.NickName;
            _photonViewInstance = this.GetComponent<PhotonView>();
            
            
            // _photonViewInstance.ObservedComponents.Add(this);
            

        }
        
        
        
    }
    
    [PunRPC]
    public void previousClass()
    {
        //if (!isValidPlayer()) return;
        // INITIALIZE IMAGE
        for (int i = 0; i < classesTransform.childCount; i++)
        {
            CanvasGroupOnOff(classesTransform.GetChild(i).GetComponent<CanvasGroup>(), false);
        }
        
        // Change class image
        switch (myClass)
        {
            case PlayerClassType.Supporter :
                myClass = PlayerClassType.Healer;
                CanvasGroupOnOff(classesTransform.Find("Healer").GetComponent<CanvasGroup>(), true);
                break;
            case PlayerClassType.Healer:
                myClass = PlayerClassType.Tanker;
                CanvasGroupOnOff(classesTransform.Find("Tanker").GetComponent<CanvasGroup>(), true);
                break;
            case PlayerClassType.Tanker:
                myClass = PlayerClassType.Dealer;
                CanvasGroupOnOff(classesTransform.Find("Dealer").GetComponent<CanvasGroup>(), true);
                break;
            case PlayerClassType.Dealer :
                myClass = PlayerClassType.Supporter;
                CanvasGroupOnOff(classesTransform.Find("Supporter").GetComponent<CanvasGroup>(), true);
                break;
        }
    }

    [PunRPC]
    public void NextClass()
    {
        //if (!isValidPlayer()) return;
        // INITIALIZE IMAGE
        for (int i = 0; i < classesTransform.childCount; i++)
        {
            CanvasGroupOnOff(classesTransform.GetChild(i).GetComponent<CanvasGroup>(), false);
        }
        
        // Change class image
        switch (myClass)
        {
            case PlayerClassType.Supporter :
                myClass = PlayerClassType.Dealer;
                CanvasGroupOnOff(classesTransform.Find("Dealer").GetComponent<CanvasGroup>(), true);
                break;
            case PlayerClassType.Healer:
                myClass = PlayerClassType.Supporter;
                CanvasGroupOnOff(classesTransform.Find("Supporter").GetComponent<CanvasGroup>(), true);
                break;
            case PlayerClassType.Tanker:
                myClass = PlayerClassType.Healer;
                CanvasGroupOnOff(classesTransform.Find("Healer").GetComponent<CanvasGroup>(), true);
                break;
            case PlayerClassType.Dealer :
                myClass = PlayerClassType.Tanker;
                CanvasGroupOnOff(classesTransform.Find("Tanker").GetComponent<CanvasGroup>(), true);
                break;
                
        }
        
        
    }

    [PunRPC]
    public void Ready()
    {
        //if (!isValidPlayer()) return;
        // TODO : Change ready image
        
        myReady = PlayerReadyType.READY;
        cancelBtn.gameObject.SetActive(true);
        readyBtn.gameObject.SetActive(false);
        // CanvasGroupOnOff();
        // CanvasGroupOnOff(ReadyCanvas.GetComponent<CanvasGroup>(), true);
        // CanvasGroupOnOff(SelectingCavans.GetComponent<CanvasGroup>(), false);
        RoomManager.GetInstance().ReadyRPC();
    }

    [PunRPC]
    public void ReadyCancel()
    {
        //if (!isValidPlayer()) return;
        // TODO : Change ready image
        myReady = PlayerReadyType.SELECTING;
        cancelBtn.gameObject.SetActive(false);
        readyBtn.gameObject.SetActive(true);
        // CanvasGroupOnOff(ReadyCanvas.GetComponent<CanvasGroup>(), false);
        // CanvasGroupOnOff(SelectingCavans.GetComponent<CanvasGroup>(), true);
        RoomManager.GetInstance().ReadyRPC();
    }
    
    /// <summary>
    /// CanvasGroup On/OFF
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

    public PlayerClassType GetClassType()
    {
        return myClass;
    }

    public PlayerReadyType GetReadyType()
    {
        return myReady;
    }
    
    /// <summary>
    /// 게임 시작 카운트다운
    /// </summary>
    public void StartCountDown()
    {
        print("### STARTCOUNTDOWN");
        _photonViewInstance.RPC("CountDown",RpcTarget.AllBufferedViaServer);
    }

    /// <summary>
    /// Set PhotonView
    /// </summary>
    public void SetPhotonView(PhotonView _photonView)
    {
        if (_photonView.IsMine)
        {
            _photonViewInstance = _photonView;
            _photonViewInstance.ObservedComponents.Add(this);
        }
        else print("### PHOTON VIEW NOT SETTED!!!");
    }

    [PunRPC]
    public void CountDown()
    {
        print("### COUNTDOWN");
        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        // 여기까지 OK
        
        loadingGame.text = "Starting in 5 Seconds";
        sec = 5;
        canvasChanging.gameObject.SetActive(true);
        btnBackToTitle.gameObject.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
