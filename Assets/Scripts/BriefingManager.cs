using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingManager : MonoBehaviour
{
    public GameObject defence;
    public GameObject enemy;
    public GameObject ending;

    public GameObject playScreen;
    public GameObject weapon;
    

    void Start()
    {
        Invoke("Defence", 5f);
    }

    void Defence()
    {
        defence.SetActive(false);
        enemy.SetActive(true);
        Invoke("Enemy", 5f);
    }

    void Enemy()
    {
        enemy.SetActive(false);
        ending.SetActive(true);
        Invoke("Ending", 5f);
    }

    void Ending()
    {
        ending.SetActive(false);
        // TODO : Inplay gun & UI load
        ObjectManager.LoadObject("MarkerGuide").GetComponent<MarkerGuide_KSY>().isBriefFinish = true;
    }

    //void Update()
    //{
    //    SetHologramPosition(Camera.main.transform);
    //}

    //void SetHologramPosition(Transform anchor)
    //{
    //    Vector3 Offset = anchor.forward * 0.5f + anchor.up * -0.2f;
    //    transform.position = anchor.position + Offset;
    //}
}
