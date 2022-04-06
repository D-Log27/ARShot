using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActive_KSY : MonoBehaviour
{
    float currentTime;
    public float createTime = 16;
    public GameObject gameObject;
    public GameObject gun;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            gameObject.SetActive(true);
            gun.SetActive(true);
        }
    }
}

