using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;

//Touch�� �ٲٱ�

public class RoomManager : MonoBehaviour
{
    //Starting in n Seconds
    public TMP_Text loadingGame;

    //InGame �ε� �ð�
    float sec;

    //���� Ŭ���� ��ư(Update���� ��� �ٲ�)
    GameObject btn;

    //�÷��̾� �غ���, �غ�Ϸ� �г�
    public CanvasGroup[] playerGettingReady;
    public CanvasGroup[] playerReady;

    //���� �ε�� �÷��̾� �г�
    public CanvasGroup[] playerReadyed;

    //���� ���ϴ� UI�� �ִ� ĵ���� Ʈ������(���� �������� InGame �ε� �ؽ�Ʈ�� ����)
    public Transform canvasChanging;

    // Start is called before the first frame update
    void Start()
    {
        canvasChanging.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            playerGettingReady[i].alpha = 1;
            playerGettingReady[i].interactable = true;
            playerGettingReady[i].blocksRaycasts = true;
            playerReady[i].alpha = 0;
            playerReady[i].interactable = false;
            playerReady[i].blocksRaycasts = false;
            playerReadyed[i].alpha = 0;
            playerReadyed[i].interactable = false;
            playerReadyed[i].blocksRaycasts = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //��Ŭ���� Ŭ���� ���ӿ�����Ʈ Ȯ��
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            btn = EventSystem.current.currentSelectedGameObject;
        }

        //��Ŭ���� �Ʒ� ���ǿ� �°� �Լ� ȣ��
        if (btn != null)
        {
            switch (btn.name)
            {
                case "Btn_PreviousClass": OnClickPreviousClass(btn); btn = null; break;
                case "Btn_NextClass": OnClickNextClass(btn); btn = null; break;
                case "Btn_SetReady": OnClickReady(btn); btn = null; break;
                case "Btn_ReadyCancel": OnClickReadyCancel(btn); btn = null; break;
                case "Btn_ReadyedCancel": OnClickReadyedCancel(btn); btn = null; break;
                default: break;
            }
        }

        //InGame �ε� �ð� ǥ�� �� ���� �� ���� �Լ�
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
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    public void OnClickPreviousClass(GameObject currentSelectedGameObject)
    {

    }

    /// <summary>
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    public void OnClickNextClass(GameObject currentSelectedGameObject)
    {

    }

    /// <summary>
    /// Ready ��ư Ŭ���� �غ� UI ���� ��� �÷��̾� �غ� �Ϸ�� ���� �ε� UI Ȱ��ȭ
    /// </summary>
    public void OnClickReady(GameObject currentSelectedGameObject)
    {
        print("1. �Լ� ����");
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        cGrp1.alpha = 0;
        cGrp1.interactable = false;
        cGrp1.blocksRaycasts = false;
        cGrp2.alpha = 1;
        cGrp2.interactable = true;
        cGrp2.blocksRaycasts = true;

        print("2. if ��");
        if (playerReady[0].alpha == 1 && playerReady[1].alpha == 1 && playerReady[2].alpha == 1 && playerReady[3].alpha == 1)
        {
            print("3. if ����");
            for (int i = 0; i < 4; i++)
            {
                playerReady[i].alpha = 0;
                playerReady[i].interactable = false;
                playerReady[i].blocksRaycasts = false;
                playerReadyed[i].alpha = 1;
                playerReadyed[i].interactable = true;
                playerReadyed[i].blocksRaycasts = true;
            }

            print("���� ����");
            loadingGame.text = "Starting in 5 Seconds";
            sec = 5;
            canvasChanging.gameObject.SetActive(true);
            print("if �� ������ ����");
        }
    }

    /// <summary>
    /// �غ� ��ҽ� �ʱ� UI ����ȭ
    /// </summary>
    public void OnClickReadyCancel(GameObject currentSelectedGameObject)
    {
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        cGrp1.alpha = 0;
        cGrp1.interactable = false;
        cGrp1.blocksRaycasts = false;
        cGrp2.alpha = 1;
        cGrp2.interactable = true;
        cGrp2.blocksRaycasts = true;
    }

    /// <summary>
    /// ���� �ε� ���� ��Ȳ���� �غ� ���
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
    }

    public void OnClickBackToTitle()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Title_Test");
    }
}
