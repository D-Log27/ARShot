using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//목표: ARTrackedImageManager컴포넌트에서 TrackedImage에 대한 정보를 얻어오고 싶다.
//만약 Tracked된 Image가 있다면, MarkerGuid를 비화성화 시키고 싶다.


//마커 라인 밖에서는 오브젝트가 비활성화
// 마커라인 안에서만 

public class MarkerGuide_KSY : MonoBehaviour
{
    ARTrackedImageManager hologramMarker;
    public GameObject markerGuide;

    private void Awake()
    {
        //1) ARTrackedImageManager컴포넌트를 가져와서 hologramMarker라고 한다.
        hologramMarker = GetComponent<ARTrackedImageManager>();

        //2) hologramMarker에 trackedImagesChanged 델리게이트를 달면 함수를 연결할 수 있기 때문에 DisenableMarkerGuide라는 함수를 만들어서 연결(+=)함
        hologramMarker.trackedImagesChanged += DisenableMarkerGuide; 
    }

    //3) 만들어진 DisenableMarkerGuide 함수에서  
    private void DisenableMarkerGuide(ARTrackedImagesChangedEventArgs args) 
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            string imageName = trackedImage.referenceImage.name;
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            if (imagePrefab != null)
            {
                GameObject go = Instantiate(imagePrefab, trackedImage.transform.position,
                                                         trackedImage.transform.rotation);
                markerGuide.SetActive(false);
            }
        }
    }

    
    

    //private void OnEnable()  // MarkerGuid 활성화 상태               
    //{
    //    arTrackedImage.trackedImagesChanged -= OnTrackedImagesChanged; ///Marker가 인식이 안된 상태일때
    //}

    //private void OnDisable() // MarkerGuid 비활성화 
    //{
    //    arTrackedImage.trackedImagesChanged += OnTrackedImagesChanged; ///Marker가 인식이 되면
    //}


    //private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    //{
    //    throw new NotImplementedException();
    //}
}
