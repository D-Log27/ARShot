using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIDrawer란 평상시에는 닫혔다가 당길 때 열리는 서랍과 같은 UI
public class UIDrawer : MonoBehaviour
{
    //UIDrawer 여는 버튼
    public Transform drawerOpener;

    //UIDrawer 닫는 버튼
    public Transform drawerCloser;

    //숨겨진 UI들 모음
    public Transform uiBox;

    //여닫이 버튼의 캔버스그룹
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
    /// UI 보이기
    /// </summary>
    private void OpenDrawer()
    {
        drawerOpener.position = Vector3.MoveTowards(drawerOpener.position, new Vector3(drawerOpener.position.x, drawerOpener.position.y - 10, drawerOpener.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBox.position, new Vector3(uiBox.position.x, uiBox.position.y - 10, uiBox.position.z), 1);
        CanvasGroupOnOff(cGrpDrawerCloser, true);
        CanvasGroupOnOff(cGrpDrawerOpener, false);
    }

    /// <summary>
    /// UI 숨기기
    /// </summary>
    private void CloseDrawer()
    {
        drawerCloser.position = Vector3.MoveTowards(drawerCloser.position, new Vector3(drawerCloser.position.x, drawerCloser.position.y - 10, drawerCloser.position.z), 1);
        uiBox.position = Vector3.MoveTowards(uiBox.position, new Vector3(uiBox.position.x, uiBox.position.y - 10, uiBox.position.z), 1);
        CanvasGroupOnOff(cGrpDrawerOpener, true);
        CanvasGroupOnOff(cGrpDrawerCloser, false);
    }

    /// <summary>
    /// 캔버스그룹의 3가지 속성을 한번에 제어
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
