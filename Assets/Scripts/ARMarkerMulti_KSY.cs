using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARMarkerMulti_KSY : MonoBehaviour
{
    // ��ǥ: ARTrackedImageManager������Ʈ���� TrackedImage�� ���� ������ ������ �ʹ�.
    // ���� Tracked�� Image�� �ִٸ�, �� Image ���� ������ ��ü�� �÷����� �ʹ�.
    // �׸��� ��ü�� ���̰� �ϰ� �ʹ�.

    ARTrackedImageManager arTrackedImageManager;

    private void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable() //
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; //��Ŀ�� ������ 
    }

    private void OnDisable() //������Ʈ�� �Ⱥ��δ�.
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; //��Ŀ�� ������
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
