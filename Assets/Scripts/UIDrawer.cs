using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIDrawer�� ���ÿ��� �����ٰ� ��� �� ������ ������ ���� UI
public class UIDrawer : MonoBehaviour
{
    public Animator uiDrawerAnimator;

    private void Start()
    {
        uiDrawerAnimator.SetBool("DrawerOpen", false);
    }

    /// <summary>
    /// UI ���̱�
    /// </summary>
    public void OnClickOpenDrawer()
    {
        uiDrawerAnimator.SetBool("DrawerOpen", true);
    }

    /// <summary>
    /// UI �����
    /// </summary>
    public void OnClickCloseDrawer()
    {
        uiDrawerAnimator.SetBool("DrawerOpen", false);
    }
}
