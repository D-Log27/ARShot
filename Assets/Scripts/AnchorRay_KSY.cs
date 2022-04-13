using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// <목표>
// 1. 사용자가 스크린을 터치하면,
// 2. 스크린의 정중앙에서 레이를 쏘아서 Plan만 검출한다.
// 2.1) 레이를 사용하기 위해 AR Raycast Manager를 가져온다.
// 2.2) ARRaycastManager 클래스의 Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType trackableTypes = (TrackableType)(-1)); 메서드 사용
//2.2.1) Vector2에 스크린의 정중앙 위치를 담아 변수 선언
//2.2.2) List<ARRaycastHit> 변수 선언
//2.2.3) TrackableType을 사용해 Plan만 검출하도록
// 3. 그 곳에 앵커를 생성


public class AnchorRay_KSY : MonoBehaviour
{

    private GameObject introAnchorObj; //레이를 쏴서 생성할 오브젝트이므로 private으로 생성
    private ARRaycastManager arManager;
    private ARAnchorManager aRAnchor;

    void Start()
    {
        arManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // TODO: 손가락 터치 구현부 쓰기(

        //public bool Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType trackableTypes = (TrackableType)(-1));
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //Screen클래스를 사용해 화면의 정중앙 
        List<ARRaycastHit> arRaycastHit = new List<ARRaycastHit>();

        if (arManager.Raycast(screenCenter, arRaycastHit, TrackableType.Planes)) //
        {
            // : Anchor함수 호출하기
        }
    }
}

  
