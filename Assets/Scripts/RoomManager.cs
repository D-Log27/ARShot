using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//Touch�� �ٲٱ�

public class RoomManager : MonoBehaviour
{
    GameObject btn;

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
        if (Input.GetMouseButton(0))
        {
        print(Input.mousePosition);
            Ray ray = new Ray(Input.mousePosition, Camera.main.transform.forward);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            Debug.Log("This hit at " + hit.point);
        }

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    print(1);
        //    btn = EventSystem.current.currentSelectedGameObject;
        //    print(btn);
        //}
        if (btn != null)
        {
            switch (btn.name)
            {
                case "Btn_PreviousClass": OnClickPreviousClass(btn); break;
                case "Btn_NextClass": OnClickNextClass(btn); break;
                case "Btn_SetReady": OnClickReady(btn); break;
                case "Btn_ReadyCancel": OnClickReadyCancel(btn); break;
                case "Btn_ReadyedCancel": OnClickReadyedCancel(btn); break;
                default: break;
            }

        }
    }

    /// <summary>
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    private void OnClickPreviousClass(GameObject currentSelectedGameObject)
    {

    }

    /// <summary>
    /// ��ư Ŭ���� ���� Ŭ���� ����
    /// </summary>
    private void OnClickNextClass(GameObject currentSelectedGameObject)
    {

    }

    /// <summary>
    /// Ready ��ư Ŭ���� �غ� UI ���� ��� �÷��̾� �غ� �Ϸ�� ���� �ε� UI Ȱ��ȭ
    /// </summary>
    private void OnClickReady(GameObject currentSelectedGameObject)
    {
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        cGrp1.alpha = 0;
        cGrp2.alpha = 1;

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
    private void OnClickReadyCancel(GameObject currentSelectedGameObject)
    {
        CanvasGroup cGrp1 = currentSelectedGameObject.GetComponentInParent<CanvasGroup>();
        CanvasGroup cGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        cGrp1.alpha = 0;
        cGrp2.alpha = 1;
    }

    /// <summary>
    /// ���� �ε� ���� ��Ȳ���� �غ� ���
    /// </summary>
    private void OnClickReadyedCancel(GameObject currentSelectedGameObject)
    {
        canvasChanging.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            playerReady[i].alpha = 1;
            playerReadyed[i].alpha = 0;
        }

        CanvasGroup canvasGrp1 = currentSelectedGameObject.transform.parent.parent.GetChild(1).GetComponent<CanvasGroup>();
        CanvasGroup canvasGrp2 = currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<CanvasGroup>();
        canvasGrp1.alpha = 0;
        canvasGrp2.alpha = 1;
    }

    public void OnClickBackToTitle()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_Test");
    }
}
