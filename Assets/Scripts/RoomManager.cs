//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;

//Touch로 바꾸기

public class RoomManager : MonoBehaviour
{
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

    //InGame 로딩 시간
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

    // Start is called before the first frame update
    void Start()
    {
        //초기 설정; unity play 전에 설정 잘 해 놓으면 사실 start 함수 전체 필요 없음
        canvasChanging.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            CanvasGroupOnOff(playerGettingReady[i], On);
            CanvasGroupOnOff(playerReady[i], Off);
            CanvasGroupOnOff(playerReadyed[i], Off);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //좌클릭시 클릭한 게임오브젝트 확인
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            btn = EventSystem.current.currentSelectedGameObject;
        }

        //좌클릭시 아래 조건에 맞게 함수 호출
        if (btn != null)
        {
            switch (btn.name)
            {
                case "Btn_PreviousClass": OnClickPrevious(btn); btn = null; break;
                case "Btn_NextClass": OnClickNext(btn); btn = null; break;
                case "Btn_SetReady": OnClickReady(btn); btn = null; break;
                case "Btn_ReadyCancel": OnClickReadyCancel(btn); btn = null; break;
                //case "Btn_ReadyedCancel": OnClickReadyedCancel(btn); btn = null; break;
                default: break;
            }
        }

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
    public void OnClickPrevious(GameObject currentSelectedGameObject)
    {
        //if (player1Class[0].alpha == 1)
        //{
            
        //}
    }

    /// <summary>
    /// 버튼 클릭시 다음 클래스 보임
    /// </summary>
    public void OnClickNext(GameObject currentSelectedGameObject)
    {

    }

    /// <summary>
    /// Ready 버튼 클릭시 준비 UI 띄우고 모든 플레이어 준비 완료시 게임 로드 UI 활성화
    /// </summary>
    public void OnClickReady(GameObject currentSelectedGameObject)
    {
        //현재 누른 버튼의 부모의 CanvasGroupComponent: GettingReady Panel의 CanvasGroup
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

        //현재 누른 버튼의 부모의 부모의 2번째 자식의 CanvasGroupComponent: Ready Panel의 CanvasGroup
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();

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
    }

    /// <summary>
    /// 준비 취소시 초기 UI 가시화
    /// </summary>
    public void OnClickReadyCancel(GameObject currentSelectedGameObject)
    {
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        CanvasGroupOnOff(cGrp1, Off);
        CanvasGroupOnOff(cGrp2, On);
    }

    /*ReadyedCancel(미사용)
    /// <summary>
    /// 게임 로딩 중인 상황에서 준비 취소
    /// </summary>
    public void OnClickReadyedCancel(GameObject currentSelectedGameObject)
    {

        canvasChanging.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            playerReady[i].alpha = 1;
            playerReady[i].interactable = true;
            playerReady[i].blocksRaycasts = true;
            playerReadyed[i].alpha = 0;
            playerReadyed[i].interactable = false;
            playerReadyed[i].blocksRaycasts = false;
        }

        CanvasGroup canvasGrp1 = currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        CanvasGroup canvasGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        canvasGrp1.alpha = 0;
        canvasGrp1.interactable = false;
        canvasGrp1.blocksRaycasts = false;
        canvasGrp2.alpha = 1;
        canvasGrp2.interactable = true;
        canvasGrp2.blocksRaycasts = true;
    }*/


    public void OnClickBackToTitle()
    {
        //PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene("Title_Test");
        SceneManager.LoadScene("Title_AL");
    }

    /// <summary>
    /// CanvasGroup 설정 변경 간소화
    /// </summary>
    void CanvasGroupOnOff(CanvasGroup cGrp, String OnOff)
    {
        if (OnOff == On)
        {
            cGrp.alpha = 1;
            cGrp.interactable = true;
            cGrp.blocksRaycasts = true;
        }
        else
        {
            cGrp.alpha = 0;
            cGrp.interactable = false;
            cGrp.blocksRaycasts = false;
        }
    }
    string On = "On"; string Off = "Off";

    //void SetActive(var i, bool )
    //{
    //    i.gameObject.SetActive();
    //}
}
