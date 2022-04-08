//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

//Touch�� �ٲٱ�

public class RoomManager : MonoBehaviourPunCallbacks
{
    //Ŭ�������� ��ư�� �θ� �̸�: ������ ���̱� ���� ������
    string btnClassParentName;

    //�ڷ� ���� ��ư
    public Button btnBackToTitle;

    //Starting in n Seconds
    public TMP_Text loadingGame;

    //������
    public TMP_Text roomID;

    //Ŭ���� ���� ��ư �� ĵ�����׷�
    public Button[] btnPrevious;
    public Button[] btnNext;
    public CanvasGroup[] cGrpPrevious;
    public CanvasGroup[] cGrpNext;

    //InGame �ε� �ð�
    float sec;

    //���� Ŭ���� ��ư(Update���� ��� �ٲ�)
    GameObject btn;

    //�÷��̾� �غ���, �غ�Ϸ� �г�
    public CanvasGroup[] playerGettingReady;
    public CanvasGroup[] playerReady;

    //���� �ε�� �÷��̾� �г�
    public CanvasGroup[] playerReadyed;

    //Ŭ���� ����
    public CanvasGroup[] playerClass;
    public CanvasGroup[] player_1Class;
    public CanvasGroup[] player_2Class;
    public CanvasGroup[] player_3Class;

    //���� ���ϴ� UI�� �ִ� ĵ���� Ʈ������(���� �������� InGame �ε� �ؽ�Ʈ�� ����)
    public Transform canvasChanging;

    // Start is called before the first frame update
    void Start()
    {
        print(PhotonNetwork.CurrentLobby.Name);
        print(PhotonNetwork.CurrentRoom.Name);
        //�ʱ� ����; unity play ���� ���� �� �� ������ ��� start �Լ� ��ü �ʿ� ����
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
                case "Btn_PreviousClass": OnClickPrevious(btn); btn = null; break;
                case "Btn_NextClass": OnClickNext(btn); btn = null; break;
                case "Btn_SetReady": OnClickReady(btn); btn = null; break;
                case "Btn_ReadyCancel": OnClickReadyCancel(btn); btn = null; break;
                //case "Btn_ReadyedCancel": OnClickReadyedCancel(btn); btn = null; break;
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
    public void OnClickPrevious(GameObject currentSelectedGameObject)
    {
        btnClassParentName = currentSelectedGameObject.transform.parent.parent.name;

        if (btnClassParentName == "Player")
        {
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnPrevious[3].enabled = false;
            }
        }
    }

    /// <summary>
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    public void OnClickNext(GameObject currentSelectedGameObject)
    {

        btnClassParentName = currentSelectedGameObject.transform.parent.parent.name;
        if (btnClassParentName == "Player")
        {
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
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

                //�̰� ���� ��Ȱ��ȭ��⺸�� ȸ�������� ��Ÿ���� �ؾ���
                btnNext[3].enabled = false;
            }
        }
    }

    /// <summary>
    /// Ready ��ư Ŭ���� �غ� UI ���� ��� �÷��̾� �غ� �Ϸ�� ���� �ε� UI Ȱ��ȭ
    /// </summary>
    public void OnClickReady(GameObject currentSelectedGameObject)
    {
        //���� ���� ��ư�� �θ��� CanvasGroupComponent: GettingReady Panel�� CanvasGroup
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();

        //���� ���� ��ư�� �θ��� �θ��� 2��° �ڽ��� CanvasGroupComponent: Ready Panel�� CanvasGroup
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
    /// �غ� ��ҽ� �ʱ� UI ����ȭ
    /// </summary>
    public void OnClickReadyCancel(GameObject currentSelectedGameObject)
    {
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        CanvasGroupOnOff(cGrp1, Off);
        CanvasGroupOnOff(cGrp2, On);
    }

    /*ReadyedCancel(�̻��)
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
    }*/

    public void OnClickBackToTitle()
    {
        //PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene("Title_Test");
        SceneManager.LoadScene("Title_AL");
    }

    /// <summary>
    /// CanvasGroup ���� ���� ����ȭ
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
}
