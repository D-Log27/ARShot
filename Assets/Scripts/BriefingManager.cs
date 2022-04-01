using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingManager : MonoBehaviour
{
    public GameObject defence;
    public GameObject enemy;
    public GameObject ending;

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
    }
}
