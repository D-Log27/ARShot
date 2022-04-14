using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorRay_KSY : MonoBehaviour 
{
    public GameObject anchorPrefab;
    private ARRaycastManager raycastManager;
    private ARAnchorManager anchorManager;
    private ARAnchor anchor;
    List<ARRaycastHit> arRaycastHit = new List<ARRaycastHit>(); 
    private bool isCreated;

    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
        raycastManager = GetComponent<ARRaycastManager>();






        
        anchor = GetComponent<ARAnchor>();
    }

    void Update()
    {
        if (isCreated == true) return; 
        Debug.Log("### isCreated : " + isCreated); // Console:False (물체가 생성 안되었을때)

        if (Input.touchCount == 0) return; 
        Debug.Log("### istouchCount==0: " + Input.touchCount);

        Touch touch = Input.GetTouch(0);


        if (touch.phase != TouchPhase.Began) return;
        Debug.Log("### isTouchBegan" + touch.phase);

        if (raycastManager.Raycast(touch.position, arRaycastHit, TrackableType.Planes)) 
        {
            var hitPose = arRaycastHit[0].pose; 
            var instantiateObject = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation);
            anchor = instantiateObject.AddComponent<ARAnchor>();

            if (arRaycastHit[0].trackable is ARPlane plane) 
            {
                anchor = anchorManager.AttachAnchor(plane, hitPose);
                isCreated = true;
            } 
        }
    }
    //
}