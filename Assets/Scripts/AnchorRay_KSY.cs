using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;




public class AnchorRay_KSY : MonoBehaviour
{
    private GameObject introAnchorObj; 
    private ARRaycastManager raycastManager;
    private ARAnchorManager anchorManager;
    private ARAnchor anchor;
    public GameObject anchorPrefab;

    List<ARRaycastHit> arRaycastHit = new List<ARRaycastHit>();

    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        anchor = GetComponent<ARAnchor>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        // 터치하면
        Touch touch = Input.GetTouch(0); 

        //public bool Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType trackableTypes = (TrackableType)(-1));

        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //화면 중앙에서 위치 생성

        if (raycastManager.Raycast(touch.position, arRaycastHit, TrackableType.Planes)) // 레이캐스트를 바닥에만 쏴서
        {
            var hitPose = arRaycastHit[0].pose; // arRaycastHit가 제일 먼저 맞은 곳
            var instantiateObject = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation); // 그 곳에 프리팹을 생성한다.
            anchor = instantiateObject.AddComponent<ARAnchor>();

            if (arRaycastHit[0].trackable is ARPlane plane) 
            {
                anchor = anchorManager.AttachAnchor(plane, hitPose);
            }
            //anchor = an

            //public ARAnchor AttachAnchor(ARPlane plane, Pose pose);
            
        }
    }
}

  
