using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ǥ2 : 7�� �ڿ� [briefingMan]�� <3DText>�� ������� �ϰ� �ʹ�.
//��ǥ3 : ��Ŀ ���� UI��ư�� ������ �ϰ� �ʹ�.

public class Intro_BriefingObjects : MonoBehaviour
{

    public GameObject brefingObjects;
    float currentTime;
    public float createTime = 7f;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            brefingObjects.SetActive(false);
        }
    }
}
