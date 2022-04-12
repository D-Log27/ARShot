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
    //Å¬·¡½º¼±ÅÃ ¹öÆ°ÀÇ ºÎ¸ğ ÀÌ¸§: º¯¼ö¸í ÁÙÀÌ±â À§ÇØ ¼±¾ğÇÔ
=======
using Photon.Realtime;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // í”Œë ˆì´ ê°€ëŠ¥í•œ í´ë˜ìŠ¤ íƒ€ì…
    enum PlayerClassType { Tanker, Dealer, Healer, Supporter }

    // í”Œë ˆì´ì–´ ìƒíƒœ
    enum PlayerReadyType { SELECTING, READY }
    
    private PhotonView _photonView;
    
    //í´ë˜ìŠ¤ì„ íƒ ë²„íŠ¼ì˜ ë¶€ëª¨ ì´ë¦„: ë³€ìˆ˜ëª… ì¤„ì´ê¸° ìœ„í•´ ì„ ì–¸í•¨
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    string btnClassParentName;

    //µÚ·Î °¡±â ¹öÆ°
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //¹æÁ¦¸ñ
    public TMP_Text roomID;
<<<<<<< HEAD

    //Å¬·¡½º ¼±ÅÃ ¹öÆ° ¹× Äµ¹ö½º±×·ì
    public Button[] btnPrevious;
    public Button[] btnNext;
    public CanvasGroup[] cGrpPrevious;
    public CanvasGroup[] cGrpNext;

    //InGame ·Îµù ½Ã°£
    float sec;

    //ÇöÀç Å¬¸¯ÇÑ ¹öÆ°(Update¿¡¼­ °è¼Ó ¹Ù²ñ)
    GameObject btn;

    //ÇÃ·¹ÀÌ¾î ÁØºñÁß, ÁØºñ¿Ï·á ÆĞ³Î
=======
    
    //InGame ë¡œë”©ì‹œê°„
    float sec;

    //í”Œë ˆì´ì–´ ì¤€ë¹„ì¤‘, ì¤€ë¹„ì™„ë£Œ íŒ¨ë„
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    public CanvasGroup[] playerGettingReady;
    public CanvasGroup[] playerReady;

    //°ÔÀÓ ·Îµå½Ã ÇÃ·¹ÀÌ¾î ÆĞ³Î
    public CanvasGroup[] playerReadyed;

    //Å¬·¡½º ¼±ÅÃ
    public CanvasGroup[] playerClass;
    public CanvasGroup[] player_1Class;
    public CanvasGroup[] player_2Class;
    public CanvasGroup[] player_3Class;

    //°ªÀÌ º¯ÇÏ´Â UI°¡ ÀÖ´Â Äµ¹ö½º Æ®·£½ºÆû(ÇöÀç ¾À¿¡¼­´Â InGame ·Îµù ÅØ½ºÆ®°¡ À¯ÀÏ)
    public Transform canvasChanging;

<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        print(PhotonNetwork.CurrentLobby.Name);
        print(PhotonNetwork.CurrentRoom.Name);
        //ÃÊ±â ¼³Á¤; unity play Àü¿¡ ¼³Á¤ Àß ÇØ ³õÀ¸¸é »ç½Ç ÇÊ¿ä ¾øÀ½
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
        //ì´ˆê¸° ì„¤ì •; unity play ì „ì— ì„¤ì • ì˜ í•´ ë†“ìœ¼ë©´ ì‚¬ì‹¤ í•„ìš” ì—†ìŒ
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
    /// ì…ì¥ì‹œ
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
    /// ë‹¤ë¥¸ í”Œë ˆì´ì–´ ë°© ì…ì¥ì‹œ
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //TODO :  í”Œë ˆì´ì–´ ì…ì¥ì‹œ ìŠ¬ë¡¯ë²ˆí˜¸ ê°±ì‹ 
        
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
                // ë°©ê¸ˆ ì…ì¥í•œ ìœ ì € ì •ë³´ ì €ì¥
                currentPlayer.Value.CustomProperties["class"] = PlayerClassType.Dealer;
                currentPlayer.Value.CustomProperties["status"] = PlayerReadyType.SELECTING;
            }
        }
        
        // TODO : ìœ ì € ì •ë³´
        NicknameDisplay();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //InGame ·Îµù ½Ã°£ Ç¥½Ã ¹× ´ÙÀ½ ¾À ¿¬°á ÇÔ¼ö
        if (canvasChanging.gameObject.activeSelf)
=======
        //InGame ë¡œë”© ì‹œê°„ í‘œì‹œ ë° ë‹¤ìŒ ì”¬ ì—°ê²° í•¨ìˆ˜
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
    /// ¹öÆ° Å¬¸¯½Ã ÀÌÀü Å¬·¡½º º¸ÀÓ
=======
    /// í´ë˜ìŠ¤ ë³€ê²½ : ì´ì „ ë²„íŠ¼ í´ë¦­ì‹œ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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
    /// ¹öÆ° Å¬¸¯½Ã ´ÙÀ½ Å¬·¡½º º¸ÀÓ
=======
    /// í´ë˜ìŠ¤ ë³€ê²½ : ë‹¤ìŒ ë²„íŠ¼ í´ë¦­ì‹œ 
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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

                //ÀÌ°Å ¿ÏÀü ºñÈ°¼ºÈ­¶ó±âº¸´Ü È¸»öºûÀ¸·Î ³ªÅ¸³ª°Ô ÇØ¾ßÇÔ
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
    /// Ready ¹öÆ° Å¬¸¯½Ã ÁØºñ UI ¶ç¿ì°í ¸ğµç ÇÃ·¹ÀÌ¾î ÁØºñ ¿Ï·á½Ã °ÔÀÓ ·Îµå UI È°¼ºÈ­
    /// </summary>
    public void OnClickReady()
    {
<<<<<<< HEAD
        //ÇöÀç ´©¸¥ ¹öÆ°ÀÇ ºÎ¸ğÀÇ CanvasGroupComponent: GettingReady PanelÀÇ CanvasGroup
        CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

        //ÇöÀç ´©¸¥ ¹öÆ°ÀÇ ºÎ¸ğÀÇ ºÎ¸ğÀÇ 2¹øÂ° ÀÚ½ÄÀÇ CanvasGroupComponent: Ready PanelÀÇ CanvasGroup
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
    /// ÁØºñ Ãë¼Ò½Ã ÃÊ±â UI °¡½ÃÈ­
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
    /// CanvasGroup ¼³Á¤ º¯°æ °£¼ÒÈ­
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
    /// TESTìš© ë‹‰ë„¤ì„ í‘œì‹œê¸°. 
    /// </summary>
    [PunRPC]
    private void NicknameDisplay()
    {
        // TODO : í´ë˜ìŠ¤ í‘œì‹œ ì—†ìŒ(í”Œë ˆì´ì–´ ì—†ìŒ) -> í´ë˜ìŠ¤ í‘œì‹œ
        
        // TODO : ë°©ì¥ì´ ë³¸ì¸ì´ë¼ë©´ ë³¸ì¸ì„ ì²«ë²ˆì§¸ í‘œì‹œ
        
        // TODO : ë°©ì¥ì´ ì•„ë‹ˆë¼ë©´ ë°©ì¥ì„ ì²«ë²ˆì§¸ í‘œì‹œ

        foreach (var customRoomPlayer in PhotonNetwork.CurrentRoom.Players)
        {
            playerLabelSlots[customRoomPlayer.Key-1].GetComponent<Text>().text = customRoomPlayer.Value.NickName;
        }
    }

    /// <summary>
    /// ë°© ë‚˜ê°€ê¸°
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
    /// ë‹¤ë¥¸ìœ ì € ë°© ë‚˜ê°„ í›„ callback
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
    /// ë°©ì¥ ë³€ê²½
    /// </summary>
    /// <param name="newMasterClient"> ìƒˆë¡œìš´ ë°©ì¥ì •ë³´</param>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (photonView.Owner.ActorNumber == newMasterClient.ActorNumber)
        {
            // TODO : ì°¸ì—¬ì re-sorting

            // TODO : ì°¸ì—¬ì ì •ë³´ screen ì´ˆê¸°í™”

        }
    }
    
    /// <summary>
    /// í”Œë ˆì´ì–´ í´ë˜ìŠ¤ í”„ë¡œí¼í‹° ë³€ê²½
    /// </summary>
    /// <param name="player">ì–´ë–¤ í”Œë ˆì´ì–´</param>
    /// <param name="classType">ì–´ë–¤ í´ë˜ìŠ¤</param>
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
    /// í”Œë ˆì´ì–´ ì¤€ë¹„ìƒíƒœ í”„ë¡œí¼í‹° ë³€ê²½
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
    /// í™”ë©´ ì¶œë ¥ ë™ê¸°í™”
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
            
            // class canvas ì´ˆê¸°í™”
            foreach (CanvasGroup canvas in userClassCanvas)
            {
                CanvasGroupOnOff(canvas, false);
            }
            
            // TODO : ìƒíƒœì— ë”°ë¼ í´ë˜ìŠ¤ í‘œì‹œ ë³€ê²½
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

            
            
            // Ready í‘œì‹œ ë³€ê²½
            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY))
            {
                //í˜„ì¬ ëˆ„ë¥¸ ë²„íŠ¼ì˜ ë¶€ëª¨ì˜ CanvasGroupComponent: GettingReady Panelì˜ CanvasGroup
                CanvasGroup cGrp1 = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

                //í˜„ì¬ ëˆ„ë¥¸ ë²„íŠ¼ì˜ ë¶€ëª¨ì˜ ë¶€ëª¨ì˜ 2ë²ˆì§¸ ìì‹ì˜ CanvasGroupComponent: Ready Panelì˜ CanvasGroup
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
            
            // ì¹´ìš´íŠ¸ë‹¤ìš´ í™•ì¸

            if (player.Value.CustomProperties["status"].Equals(PlayerReadyType.READY)) isAllReady *= 1;
            else isAllReady *= 0;
        }

        // ì „ë¶€ ì¤€ë¹„ìƒíƒœë¼ë©´ -> ì¹´ìš´íŠ¸ë‹¤ìš´ ì‹œì‘
        if (isAllReady == 1) StartCountDown();
        else return;
    }


    /// <summary>
    /// ê²Œì„ ì‹œì‘ ì¹´ìš´íŠ¸ë‹¤ìš´
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
