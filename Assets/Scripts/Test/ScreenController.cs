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
    public Transform spawnZone;

    ScreenController() { }
    public static ScreenController GetInstance()
    {
        return Instance;
    }

    IEnumerator Start()
    {
        Instance = this;

        yield return new WaitForSeconds(3.0f);
    }

    public void FinishBrief()
    {
        // OK
        brief.gameObject.SetActive(false);
        GameUI.GetComponent<Canvas>().enabled = true;
        weapons.Find("Pistol").gameObject.SetActive(true);
        // NOT OK
        spawnZone.gameObject.SetActive(true);
    }
}
