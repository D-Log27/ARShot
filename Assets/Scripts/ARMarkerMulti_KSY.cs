using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//목표1 : AR Tracked Image Manager가 추적한 마커의 정보를 얻어오고 싶다.
//목표2 : 만약 추적한 마커가 있다면 특정 3D 오브젝트를 마커의 위치에 배치하고 싶다.
//목표3 : 그 3D 오브젝트가 보이게 하고 싶다.

public class ARMarkerMulti_KSY : MonoBehaviour
{
    ARTrackedImageManager aRTrackedImageManager;

    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
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
