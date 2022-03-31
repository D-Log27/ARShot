using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라(화면중앙)가 향한 곳에 마커가 있다면
// 그곳에 오브젝트를 배치하고 싶다.
// 인디케이터가 보여질때 인디케이터를 클릭(터치)하면
// 그 위치에 물체를 배치하고 싶다.

public class MyARManager : MonoBehaviour
{
    // "GameObject(AR)"가 "indicator의 위치(Transform)"에 나타나도록
    public Transform indicator; // AR 오브젝트가 나타날 위치
    public GameObject factory; // AR로 보여질 3D오브젝트
   
    void Update()
    {
        // 레이를 만들었다! 레이가 생성될 위치는 메인카메라의 위치고 레이가 쏴질 방향은 메인카메라의 앞방향
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // 카메라(화면중앙)가 향한 곳에 바닥이 있다면
         RaycastHit hitInfo; //오브젝트가 Ray를 맞았다는 것을 RaycastHit을 통해서 저장을 한다.
        if (Physics.Raycast(ray, out hitInfo)) // 만약 오브젝트가 Ray를 맞았다면,
        {
            if (hitInfo.transform.name.Equals("Floor")) // 그리고 그 오브젝트가 "Floor"이라면,
            {
                indicator.gameObject.SetActive(true); // 인디케이터를 활성화해서 그곳에 인디케이터를 배치하고 싶다.
                indicator.transform.position = hitInfo.point + hitInfo.normal * 0.1f; // 그리고 인디케이터의 위치를 hitInfo의 point에 위치하게 하고 싶다.
            }
            else //"Floor"외에는 인디케이터를 꺼서 안보이게 하고 싶다.
            {
                indicator.gameObject.SetActive(false);
            }
        }
        // 만약 인디케이터가 보이는 상태라면
        if (true == indicator.gameObject.activeSelf) //gameObject.activeSelf는 오브젝트가 현재 보이게 설정이 되었다는 뜻
        {
            //인디케이터를 클릭(터치)했을때
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo2;
            if (Physics.Raycast(ray2, out hitInfo2))
            {
                if (hitInfo2.transform.name.Equals("Indicator"))
                {
                    //그 위체 물체를 배치하고 싶다.
                    GameObject obj = Instantiate(factory);
                    obj.transform.position = hitInfo2.point;
                }
                
            }
        }
    }

}
