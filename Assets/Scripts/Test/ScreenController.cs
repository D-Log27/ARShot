using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenController : MonoBehaviour
{
    private static ScreenController Instance;
    public Transform brief;
    public Transform GameUI;
    public Transform weapons;
    public Transform gameArea;

    ScreenController() { }
    public static ScreenController GetInstance()
    {
        return Instance;
    }

    public void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Instance = this;
        //FinishBrief();
    }

    public void FinishBrief()
    {
        // OK
        brief.gameObject.SetActive(false);
        GameUI.GetComponent<Canvas>().enabled = true;
        weapons.Find("Pistol").gameObject.SetActive(true);
        // NOT OK
        gameArea.gameObject.SetActive(true);
    }
}
