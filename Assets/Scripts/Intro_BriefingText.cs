using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ǥ1 : [briefingMan]�� �����ǰ� 2�� �ڿ� [briefingText]�� �����ϰ� �ʹ�.
//��ǥ2 : [briefingText]�� �����ǰ� 5�� �ڿ� [briefingMan]�� <3DText>�� ������� �ϰ� �ʹ�.
//��ǥ3 : ��Ŀ ���� UI��ư�� ������ �ϰ� �ʹ�.

public class Intro_BriefingText : MonoBehaviour
{
    // ó���� [briefingText]�� ���� ���·� ����
    // 2�� �� Ȱ��ȭ

   
    public GameObject briefingText;

    float currentTime;
    public float createTime = 2f;
    //public GameObject defenceObjectFactory;

    void Update()
    {
        briefingText.SetActive(false);

        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            briefingText.SetActive(true);
          
        }


        //1-1) ó�� ������ �� Text�� ��Ȱ��ȭ�ؼ� �Ⱥ��̰��ϰ� �ʹ�.
        //1-2) 1.5�� �ڿ� Ȱ��ȭ�ؼ� ���̰��ϰ� �ʹ�.


        //currentTime += Time.deltaTime;
        //if (currentTime > createTime)
        //{
        //    currentTime = 0;
        //    Destroy(gameObject);
        //    //gameObject.SetActive(false);

        //    GameObject defenceObject = Instantiate(defenceObjectFactory);
        //    defenceObject.transform.position = transform.position;

    }
}
