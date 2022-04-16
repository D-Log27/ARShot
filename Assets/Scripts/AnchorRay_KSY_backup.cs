using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorRay_KSY_backup : MonoBehaviour 
{
    public GameObject anchorPrefab;
    private ARRaycastManager raycastManager;
    private ARAnchorManager anchorManager;
    private ARAnchor anchor;
    List<ARRaycastHit> arRaycastHit = new List<ARRaycastHit>(); 
    private bool isCreatedHologram;

    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        anchor = GetComponent<ARAnchor>();
    }

    void Update()
    {
        if (isCreatedHologram == true) return; 
        Debug.Log("### isCreated : " + isCreatedHologram); // 조건1: Hologram이 생성 되지 않았을 때,

        if (Input.touchCount == 0) return; 
        Debug.Log("### istouchCount==0: " + Input.touchCount); //조건2: 터치 횟수가 1번 이상일 때,

        Touch touch = Input.GetTouch(0); // 첫번째 터치 가져오는 코드

        if (touch.phase != TouchPhase.Began) return; //내가 여러번 터치했으면 종료
        //내가 여러번 터치를 "실행했음(true)"이면 종료/ "안했음(거짓)이면 다음 구문 실행"
        Debug.Log("### isTouchBegan" + touch.phase);

        if (raycastManager.Raycast(touch.position, arRaycastHit, TrackableType.Planes)) 
        {
            //레이가 처음 맞은 곳만 추출
            var hitPose = arRaycastHit[0].pose; 
            //anchorPrefab을 처음 맞은 곳의 position과 rotation에 생성한다. 
            var instantiateHologramPrefab = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation);
            
            //instantiateObject에서 ARAnchor컴포넌트를 가져와 anchor이라고 한다.
            anchor = instantiateHologramPrefab.AddComponent<ARAnchor>();

            //만약 처음 맞은 곳이 ARPlane이라면 
            if (arRaycastHit[0].trackable is ARPlane plane) 
            {
                //그곳에 앵커를 박는다.
                anchor = anchorManager.AttachAnchor(plane, hitPose);
                //그리고 종료한다.
                isCreatedHologram = true;
            } 
        }
    }
    //
}