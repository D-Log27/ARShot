using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Btn_Attack]�� ��ġ�ϸ� [FirePosition]���� ī�޶� �� �������� Ray�� ���, 
// ���� Ray�� �ε����ٸ� �Ѿ��ڱ����忡�� �Ѿ��ڱ��� ����� �ε��� ��ġ�� ��ġ�ϰ� �ʹ�.

public class Gun_KSY : MonoBehaviour
{
    public GameObject bulletImpactFactory;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject efffect = Instantiate(bulletImpactFactory);
                efffect.transform.position = hitInfo.point;
                efffect.transform.forward = hitInfo.normal;
                
            }
        }
    }



}