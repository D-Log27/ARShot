using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActive_KSY : MonoBehaviour
{
    float currentTime;
    public float createTime = 3;
    public GameObject gameObject;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            gameObject.SetActive(true);
        }
    }
}

