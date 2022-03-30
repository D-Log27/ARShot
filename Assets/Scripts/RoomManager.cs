using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    //2���� �迭, ��ư �ʹ� ���Ƽ� 2�������� ����
    [System.Serializable]
    public class BtnArray //�̰� ���ͳݿ��� �����ǵ� ���� ���� ���߽��ϴ�.
    {
        public Button[] buttons;
    }
    public BtnArray[] playerArray;

    //�÷��̾� �غ�Ϸ� �г�
    public CanvasGroup[] playerReady;

    //���� �ε�� �÷��̾� �г�
    public CanvasGroup[] playerReadyed;

    //Starting in N Seconds Ʈ������
    public Transform canvasChanging;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾�1 ��ư �ڵ�
        playerArray[0].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[0].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[0].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[0].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[0].buttons[4].onClick.AddListener(OnClickReadyedCancel);

        //�÷��̾�2 ��ư �ڵ�
        playerArray[1].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[1].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[1].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[1].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[1].buttons[4].onClick.AddListener(OnClickReadyedCancel);

        //�÷��̾�3 ��ư �ڵ�
        playerArray[2].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[2].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[2].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[2].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[2].buttons[4].onClick.AddListener(OnClickReadyedCancel);

        //�÷��̾�4 ��ư �ڵ�
        playerArray[3].buttons[0].onClick.AddListener(OnClickPreviousClass);
        playerArray[3].buttons[1].onClick.AddListener(OnClickNextClass);
        playerArray[3].buttons[2].onClick.AddListener(OnClickReady);
        playerArray[3].buttons[3].onClick.AddListener(OnClickReadyCancel);
        playerArray[3].buttons[4].onClick.AddListener(OnClickReadyedCancel);
    }

    /// <summary>
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    public void OnClickPreviousClass()
    {
        GetComponentInParent<CanvasGroup>();
    }

    /// <summary>
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    public void OnClickNextClass()
    {

    }

    /// <summary>
    /// Ready ��ư Ŭ���� �غ� UI ���� ��� �÷��̾� �غ� �Ϸ�� ���� �ε� UI Ȱ��ȭ
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
    /// �غ� ��ҽ� �ʱ� UI ����ȭ
    /// </summary>
    public void OnClickReadyCancel()
    {
        CanvasGroup canvasGrp1 = GetComponentInParent<CanvasGroup>();
        CanvasGroup canvasGrp2 = transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        canvasGrp1.alpha = 0;
        canvasGrp2.alpha = 1;
    }

    /// <summary>
    /// ���� �ε� ���� ��Ȳ���� �غ� ���
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
