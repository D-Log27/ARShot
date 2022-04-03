using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Btn_Attack]을 터치하면 [FirePosition]에서 카메라 앞 방향으로 Ray를 쏘고, 
// 만약 Ray가 부딪혔다면 총알자국공장에서 총알자국을 만들어 부딪힌 위치에 배치하고 싶다.

public class Bullet_KSY : MonoBehaviour
{
    public Transform firePositon; //Ray가 시작되는 위치
    public LineRenderer lineRenderer; //LineRender를 사용하기 위해 선언
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
