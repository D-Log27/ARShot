using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ǥ1 : [briefingMan]�� �����ǰ� 2�� �ڿ� [briefingText]�� �����ϰ� �ʹ�.
//��ǥ2 : [briefingText]�� �����ǰ� 5�� �ڿ� [briefingMan]�� <3DText>�� ������� �ϰ� �ʹ�.

public class Intro_BriefingText : MonoBehaviour
{
    // ó���� [briefingText]�� ���� ���·� ����
    // 2�� �� Ȱ��ȭ


    public GameObject briefingText;

    float currentTime;
    public float createTime = 2f;
    //public GameObject defenceObjectFactory;

    void Start()
    {
        briefingText.SetActive(false);
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            briefingText.SetActive(true);

        }
    }
}
