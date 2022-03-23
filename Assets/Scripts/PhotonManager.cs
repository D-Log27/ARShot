using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PhotonCollection = ExitGames.Client.Photon;
using System.Text;
/// <summary>
/// 포톤 매니저
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "0.0.1";
    string apName;
    string userName;
    PhotonCollection.Hashtable roomApProperty = new PhotonCollection.Hashtable();
    private void Awake()
    {
        if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
        userName = JsonHelper.GetInstance().ReadValue(OptionType.USER_NAME);
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = userName;
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    /// <summary>
    /// Photon 클라우드 접속 후 callback
    /// </summary>
    public override void OnConnectedToMaster()
    {
        print("### photon server connected.");
        SetApProperty();
        TypedLobby typedLobby = new TypedLobby(apName,LobbyType.Default);
        PhotonNetwork.JoinLobby(typedLobby);
    }

    /// <summary>
    /// 로비 접속후 callback
    /// </summary>
    public override void OnJoinedLobby()
    {
        print($"### Lobby joined , {PhotonNetwork.CurrentLobby.Name}");
    }

    /// <summary>
    /// 방 접속 실패 후 콜백
    /// </summary>
    /// <param name="returnCode">에러코드</param>
    /// <param name="message">메시지</param>
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
        print($"### random num check : {randomNum}");
        PhotonNetwork.CreateRoom(randomNum.ToString(), roomOptions, null);
    }
    /// <summary>
    /// 랜덤 방 접속 실패 후 콜백
    /// </summary>
    /// <param name="returnCode">에러코드</param>
    /// <param name="message">메시지</param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        OnJoinRoomFailed(returnCode, message);
    }

    /// <summary>
    /// 방 생성 후 콜백
    /// </summary>
    public override void OnCreatedRoom()
    {
        print($"### room created : {PhotonNetwork.CurrentRoom.Name}");
    }

    /// <summary>
    /// 방 입장 후 콜백
    /// </summary>
    public override void OnJoinedRoom()
    {
# if UNITY_EDITOR
        PhotonCollection.Hashtable testPair = PhotonNetwork.CurrentRoom.CustomProperties;

        print($"### Lobby name : {PhotonNetwork.CurrentLobby.Name}");
        print($"### room cotains apname ? {testPair.ContainsKey("ApName")}");
        print($"### room apname : {testPair["ApName"]}");
        print($"### room name : {PhotonNetwork.CurrentRoom.Name}");
#endif
        // TODO : 방 scene으로 전환
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    
        //    PhotonNetwork.LoadLevel("Room");
        //}
    }

    /// <summary>
    /// AP 정보 설정
    /// </summary>
    void SetApProperty()
    {
        if(NetworkAPHelper.GetInstance().ApName == "" || NetworkAPHelper.GetInstance().ApName == null) NetworkAPHelper.GetInstance().GetAPInfo();
        apName = NetworkAPHelper.GetInstance().ApName;
        print(apName);
        roomApProperty = new PhotonCollection.Hashtable()
        {
            { "ApName",apName }
        };
        print($"room property check : {roomApProperty["ApName"]}");
        return;
    }

    #region UI_CALLBACK
    /// <summary>
    /// Title -> Start Click
    /// </summary>
    public void OnClickStartButton()
    {
        print("### click start");
        SetApProperty();
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion
}
