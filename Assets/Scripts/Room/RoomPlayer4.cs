using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayer4 : MonoBehaviour,IRoomPlayer
{
    int PLAYER_NUM = 4;
    [HideInInspector]
    public PlayerClassType myClass;
    [HideInInspector]
    public PlayerReadyType myReady;
    private Player myPlayer;

    public CanvasGroup ReadyCanvas;
    public CanvasGroup SelectingCavans;
    
    public Button previousClassBtn;
    public Button nextClassBtn;
    public Button readyBtn;
    public Button cancelBtn;

    public Transform classesTransform;

    private PhotonView _photonView;

    public Text nickName;

    public bool isReady;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        _photonView = this.GetComponent<PhotonView>();
        print("### CHECK ROOM PLAYER 1");
        previousClassBtn.onClick.AddListener(()=>previousClass());// TODO : Add lister method
        nextClassBtn.onClick.AddListener(()=>NextClass());// TODO : Add lister method
        readyBtn.onClick.AddListener(()=>Ready());// TODO : Add lister method
        cancelBtn.onClick.AddListener(()=>ReadyCancel());// TODO : Add lister method
        CheckSlotPlayer();
    }

    private void Initialize()
    {
        isReady = false;
        // TODO : UI 닉네임 초기화
        // TODO : UI 클래스 초기화
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckSlotPlayer()
    {
        if (PhotonNetwork.CurrentRoom.Players[PLAYER_NUM] == null) return;
        if (PhotonNetwork.LocalPlayer.NickName.Equals(PhotonNetwork.CurrentRoom.Players[PLAYER_NUM].NickName))
        {
            myPlayer = PhotonNetwork.LocalPlayer;
            _photonView.TransferOwnership(myPlayer);
            nickName.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }

    private void previousClass()
    {
        // TODO : INITIALIZE IMAGE
        for (int i = 0; i < classesTransform.childCount; i++)
        {
            CanvasGroupOnOff(classesTransform.GetChild(i).GetComponent<CanvasGroup>(), false);
        }
        
        // TODO : Change class image
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
    
    private void NextClass()
    {
        // TODO : INITIALIZE IMAGE
        for (int i = 0; i < classesTransform.childCount; i++)
        {
            CanvasGroupOnOff(classesTransform.GetChild(i).GetComponent<CanvasGroup>(), false);
        }
        
        // TODO : Change class image
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

    private void Ready()
    {
        // TODO : Change ready image
        
        myReady = PlayerReadyType.READY;
        CanvasGroupOnOff(ReadyCanvas.GetComponent<CanvasGroup>(), true);
        CanvasGroupOnOff(SelectingCavans.GetComponent<CanvasGroup>(), false);
    }

    private void ReadyCancel()
    {
        // TODO : Change ready image
        myReady = PlayerReadyType.SELECTING;
        CanvasGroupOnOff(ReadyCanvas.GetComponent<CanvasGroup>(), false);
        CanvasGroupOnOff(SelectingCavans.GetComponent<CanvasGroup>(), true);
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
}
