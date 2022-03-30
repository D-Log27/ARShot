using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    //2차원 배열, 버튼 너무 많아서 2차원으로 선언
    [System.Serializable]
    public class BtnArray //이거 인터넷에서 얻은건데 아직 이해 못했습니다.
    {
        public Button[] buttons;
    }
    public BtnArray[] playerArray;

    //플레이어 준비완료 패널
    public CanvasGroup[] playerReady;

    //게임 로드시 플레이어 패널
    public CanvasGroup[] playerReadyed;

    //Starting in N Seconds 트랜스폼
    public Transform canvasChanging;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //플레이어1 버튼 코드
        playerArray[0].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[0].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[0].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[0].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[0].buttons[4].onClick.AddListener(OnClickReadyedCancel);

        //플레이어2 버튼 코드
        playerArray[1].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[1].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[1].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[1].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[1].buttons[4].onClick.AddListener(OnClickReadyedCancel);

        //플레이어3 버튼 코드
        playerArray[2].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[2].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[2].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[2].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[2].buttons[4].onClick.AddListener(OnClickReadyedCancel);

        //플레이어4 버튼 코드
        playerArray[3].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[3].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[3].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[3].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[3].buttons[4].onClick.AddListener(OnClickReadyedCancel);
    }

    /// <summary>
    /// 버튼 클릭시 이전 클래스 보임
    /// </summary>
    public void OnClickPreviousClass()
    {
        GetComponentInParent<CanvasGroup>();
    }

    /// <summary>
    /// 버튼 클릭시 다음 클래스 보임
    /// </summary>
    public void OnClickNextClass()
    {

    }

    /// <summary>
    /// Ready 버튼 클릭시 준비 UI 띄우고 모든 플레이어 준비 완료시 게임 로드 UI 활성화
    /// </summary>
    public void OnClickReady()
    {
        CanvasGroup canvasGrp1 = GetComponentInParent<CanvasGroup>();
        CanvasGroup canvasGrp2 = transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        canvasGrp1.alpha = 0;
        canvasGrp2.alpha = 1;

        if (playerReady[0].alpha == 1 && playerReady[1].alpha == 1 && playerReady[2].alpha == 1 && playerReady[3].alpha == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                playerReady[i].alpha = 0;
                playerReadyed[i].alpha = 1;
            }

            canvasChanging.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 준비 취소시 초기 UI 가시화
    /// </summary>
    public void OnClickReadyCancel()
    {
        CanvasGroup canvasGrp1 = GetComponentInParent<CanvasGroup>();
        CanvasGroup canvasGrp2 = transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        canvasGrp1.alpha = 0;
        canvasGrp2.alpha = 1;
    }

    /// <summary>
    /// 게임 로딩 중인 상황에서 준비 취소
    /// </summary>
    public void OnClickReadyedCancel()
    {
        canvasChanging.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            playerReady[i].alpha = 1;
            playerReadyed[i].alpha = 0;
        }

        CanvasGroup canvasGrp1 = transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        CanvasGroup canvasGrp2 = transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        canvasGrp1.alpha = 0;
        canvasGrp2.alpha = 1;

    }

    public void OnClickBackToTitle()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_Test");
    }
}
