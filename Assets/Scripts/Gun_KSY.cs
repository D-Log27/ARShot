using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Btn_Attack]을 터치하면 [FirePosition]에서 카메라 앞 방향으로 Ray를 쏘고, 
// 만약 Ray가 부딪혔다면 총알자국공장에서 총알자국을 만들어 부딪힌 위치에 배치하고 싶다.

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