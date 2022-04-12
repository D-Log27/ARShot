using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIDrawer�� ���ÿ��� �����ٰ� ��� �� ������ ������ ���� UI
public class UIDrawer : MonoBehaviour
{
    //UIDrawer ���� �ݴ� ��ư
    public GameObject drawerOpener;
    public GameObject drawerCloser;

    //�� ��ü�� ����ġ
    Transform drawerOpenerTrAtStart;
    Transform drawerCloserTrAtStart;
    Transform uiBoxTrAtStart;

    //������ UI�� ����
    public Transform uiBox;

    // Start is called before the first frame update
    void Start()
    {
        drawerOpenerTrAtStart = drawerOpener.transform;
        drawerCloserTrAtStart = drawerCloser.transform;
        uiBoxTrAtStart = uiBox;
    }

    // Update is called once per frame
    void Update()
    {
        OpenDrawer();
        //CloseDrawer();
    }


    /// <summary>
    /// UI ���̱�
    /// </summary>
    private void OpenDrawer()
    {
        drawerOpener.transform.position = Vector3.MoveTowards(drawerOpenerTrAtStart.position, new Vector3(drawerOpenerTrAtStart.position.x, drawerOpenerTrAtStart.position.y - 10, drawerOpenerTrAtStart.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBoxTrAtStart.position, new Vector3(uiBoxTrAtStart.position.x, uiBoxTrAtStart.position.y - 10, uiBoxTrAtStart.position.z), 1);
        drawerCloser.SetActive(true);
        drawerOpener.SetActive(false);
    }

    /// <summary>
    /// UI �����
    /// </summary>
    private void CloseDrawer()
    {
        drawerCloser.transform.position = Vector3.MoveTowards(drawerCloserTrAtStart.position, new Vector3(drawerCloserTrAtStart.position.x, drawerCloserTrAtStart.position.y - 10, drawerCloserTrAtStart.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBoxTrAtStart.position, new Vector3(uiBoxTrAtStart.position.x, uiBoxTrAtStart.position.y - 10, uiBoxTrAtStart.position.z), 1);
        drawerCloser.SetActive(false);
        drawerOpener.SetActive(true);
    }
}
