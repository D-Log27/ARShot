using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIDrawer란 평상시에는 닫혔다가 당길 때 열리는 서랍과 같은 UI
public class UIDrawer : MonoBehaviour
{
    public Animator uiDrawerAnimator;

    private void Start()
    {
        uiDrawerAnimator.SetBool("DrawerOpen", false);
    }

    /// <summary>
    /// UI 보이기
    /// </summary>
    public void OnClickOpenDrawer()
    {
        uiDrawerAnimator.SetBool("DrawerOpen", true);
    }

    /// <summary>
    /// UI 숨기기
    /// </summary>
    public void OnClickCloseDrawer()
    {
        uiDrawerAnimator.SetBool("DrawerOpen", false);
    }
}
