using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PhotonCollection = ExitGames.Client.Photon;
using System.Text;
using System;

/// <summary>
/// 포톤 매니저
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    private static PhotonManager Instance;
    PhotonManager() { }

    private readonly string gameVersion = "0.0.1";
    string apName;
    string userName;
    TitleManager titleManager;
    PhotonCollection.Hashtable roomApProperty = new PhotonCollection.Hashtable();

    public static PhotonManager GetInstance ()
    {
        return Instance;
    }
    private void Awake()
    {
        Instance = this;
        userName = JsonHelper.GetInstance().ReadValue(OptionType.USER_NAME);
    }

    // Start is called before the first frame update
    void Start()
    {
        TitleManager.GetInstance().TitleLoadingImage(true);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //print("### NOT CONNECTED AP");
            //Application.Quit();
        }
        titleManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = userName;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    /// <summary>
    /// Photon 서버 접속 후 callback
    /// </summary>
    public override void OnConnectedToMaster()
    {
        try
        {
            print("### photon server connected.");
            SetApProperty();
            TypedLobby typedLobby = new TypedLobby(apName,LobbyType.Default);
            titleManager.TitleLoadingImage(true);
            // PhotonNetwork.JoinLobby(typedLobby);
            PhotonNetwork.JoinLobby();
        } catch (Exception e)
        {
            print($"### lobby connect failed , {e}");
        }
    }

    /// <summary>
    /// 로비 입장 후 callback
    /// </summary>
    public override void OnJoinedLobby()
    {
        titleManager.TitleLoadingImage(false);
        TitleManager.GetInstance().TitleLoadingImage(false);
    }

    /// <summary>
    /// 방 입장 실패 후 callback
    /// </summary>
    /// <param name="returnCode">�����ڵ�</param>
    /// <param name="message">�޽���</param>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print($"### join Random room failed , code : {returnCode}, message : {message}");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "ApName"};
        SetApProperty();
        roomOptions.CustomRoomProperties = roomApProperty;

        int randomNum = MathUtil.GetInstance().RandomInt();
        PhotonNetwork.CreateRoom(randomNum.ToString(), roomOptions, null);
    }
    /// <summary>
    /// 임의의 방 입장 실패 후 callback
    /// </summary>
    /// <param name="returnCode">�����ڵ�</param>
    /// <param name="message">�޽���</param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        OnJoinRoomFailed(returnCode, message);
    }

    /// <summary>
    /// 방 생성
    /// </summary>
    public override void OnCreatedRoom()
    {
        print($"### create");
    }

    /// <summary>
    /// 방 입장
    /// </summary>
    public override void OnJoinedRoom()
    {
        print($"### joined : {PhotonNetwork.CurrentRoom.Name}");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Room_AL");
        }
    }

    /// <summary>
    /// AP 획득
    /// </summary>
    void SetApProperty()
    {
        if(NetworkAPHelper.GetInstance().ApName == "" || NetworkAPHelper.GetInstance().ApName == null) NetworkAPHelper.GetInstance().GetAPInfo();
        apName = NetworkAPHelper.GetInstance().ApName;
        //apName = "test";
        // AP기반 미사용시 IP -> 임시닉네임으로 설정
        PhotonNetwork.NickName = apName;
        roomApProperty = new PhotonCollection.Hashtable()
        {
            { "ApName",apName }
        };
        return;
    }

    #region UI_CALLBACK
    /// <summary>
    /// Title -> Start Click
    /// </summary>
    public bool ConnectingRoom()
    {
        try
        {
            SetApProperty();
            PhotonNetwork.JoinRandomRoom();
            return true;
        } catch(Exception e)
        {
            print($"### room connect failed , {e}");
            return false;
        }
        
    }

    #endregion
}
