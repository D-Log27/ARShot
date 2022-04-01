using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARMarkerMulti_KSY : MonoBehaviour
{
    // 목표: ARTrackedImageManager컴포넌트에서 TrackedImage에 대한 정보를 얻어오고 싶다.
    // 만약 Tracked된 Image가 있다면, 그 Image 위에 증강할 물체를 올려놓고 싶다.
    // 그리고 물체가 보이게 하고 싶다.

    ARTrackedImageManager arTrackedImageManager;

    private void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable() //
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; //마커가 있으면 
    }

    private void OnDisable() //오브젝트가 안보인다.
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; //마커가 없으면
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        throw new NotImplementedException();
    }





    // Start is called before the first frame update
    void Start()
    {
       
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
