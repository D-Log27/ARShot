using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIDrawer�� ���ÿ��� �����ٰ� ��� �� ������ ������ ���� UI
public class UIDrawer : MonoBehaviour
{
    //UIDrawer ���� ��ư
    public Transform drawerOpener;

    //UIDrawer �ݴ� ��ư
    public Transform drawerCloser;

    //������ UI�� ����
    public Transform uiBox;

    //������ ��ư�� ĵ�����׷�
    public CanvasGroup cGrpDrawerOpener;
    public CanvasGroup cGrpDrawerCloser;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenDrawer();
        CloseDrawer();
    }


    /// <summary>
    /// UI ���̱�
    /// </summary>
    private void OpenDrawer()
    {
        drawerOpener.position = Vector3.MoveTowards(drawerOpener.position, new Vector3(drawerOpener.position.x, drawerOpener.position.y - 10, drawerOpener.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBox.position, new Vector3(uiBox.position.x, uiBox.position.y - 10, uiBox.position.z), 1);
        CanvasGroupOnOff(cGrpDrawerCloser, true);
        CanvasGroupOnOff(cGrpDrawerOpener, false);
    }

    /// <summary>
    /// UI �����
    /// </summary>
    private void CloseDrawer()
    {
        drawerCloser.position = Vector3.MoveTowards(drawerCloser.position, new Vector3(drawerCloser.position.x, drawerCloser.position.y - 10, drawerCloser.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBox.position, new Vector3(uiBox.position.x, uiBox.position.y - 10, uiBox.position.z), 1);
        CanvasGroupOnOff(cGrpDrawerOpener, true);
        CanvasGroupOnOff(cGrpDrawerCloser, false);
    }

    /// <summary>
    /// ĵ�����׷��� 3���� �Ӽ��� �ѹ��� ����
    /// </summary>
    /// <param name="cGrp"></param>
    /// <param name="isOn"></param>
    void CanvasGroupOnOff(CanvasGroup cGrp, bool isOn)
    {
        if (isOn)
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
}
