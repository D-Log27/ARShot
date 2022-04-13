using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIDrawer란 평상시에는 닫혔다가 당길 때 열리는 서랍과 같은 UI
public class UIDrawer : MonoBehaviour
{
    //UIDrawer 열고 닫는 버튼
    public GameObject drawerOpener;
    public GameObject drawerCloser;

    //각 개체의 원위치
    Transform drawerOpenerTrAtStart;
    Transform drawerCloserTrAtStart;
    Transform uiBoxTrAtStart;

    //숨겨진 UI들 모음
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
    /// UI 보이기
    /// </summary>
    private void OpenDrawer()
    {
        drawerOpener.transform.position = Vector3.MoveTowards(drawerOpenerTrAtStart.position, new Vector3(drawerOpenerTrAtStart.position.x, drawerOpenerTrAtStart.position.y - 10, drawerOpenerTrAtStart.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBoxTrAtStart.position, new Vector3(uiBoxTrAtStart.position.x, uiBoxTrAtStart.position.y - 10, uiBoxTrAtStart.position.z), 1);
        drawerCloser.SetActive(true);
        drawerOpener.SetActive(false);
    }

    /// <summary>
    /// UI 숨기기
    /// </summary>
    private void CloseDrawer()
    {
        drawerCloser.transform.position = Vector3.MoveTowards(drawerCloserTrAtStart.position, new Vector3(drawerCloserTrAtStart.position.x, drawerCloserTrAtStart.position.y - 10, drawerCloserTrAtStart.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBoxTrAtStart.position, new Vector3(uiBoxTrAtStart.position.x, uiBoxTrAtStart.position.y - 10, uiBoxTrAtStart.position.z), 1);
        drawerCloser.SetActive(false);
        drawerOpener.SetActive(true);
    }
}
