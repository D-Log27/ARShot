using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//��ǥ: ARTrackedImageManager������Ʈ���� TrackedImage�� ���� ������ ������ �ʹ�.
//���� Tracked�� Image�� �ִٸ�, MarkerGuid�� ��ȭ��ȭ ��Ű�� �ʹ�.


//��Ŀ ���� �ۿ����� ������Ʈ�� ��Ȱ��ȭ
// ��Ŀ���� �ȿ����� 

public class MarkerGuide_KSY : MonoBehaviour
{
    ARTrackedImageManager hologramMarker;
    public GameObject markerGuide;

    private void Awake()
    {
        //1) ARTrackedImageManager������Ʈ�� �����ͼ� hologramMarker��� �Ѵ�.
        hologramMarker = GetComponent<ARTrackedImageManager>();

        //2) hologramMarker�� trackedImagesChanged ��������Ʈ�� �޸� �Լ��� ������ �� �ֱ� ������ DisenableMarkerGuide��� �Լ��� ���� ����(+=)��
        hologramMarker.trackedImagesChanged += DisenableMarkerGuide; 
    }

    //3) ������� DisenableMarkerGuide �Լ�����  
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

    
    

    //private void OnEnable()  // MarkerGuid Ȱ��ȭ ����               
    //{
    //    arTrackedImage.trackedImagesChanged -= OnTrackedImagesChanged; ///Marker�� �ν��� �ȵ� �����϶�
    //}

    //private void OnDisable() // MarkerGuid ��Ȱ��ȭ 
    //{
    //    arTrackedImage.trackedImagesChanged += OnTrackedImagesChanged; ///Marker�� �ν��� �Ǹ�
    //}


    //private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    //{
    //    throw new NotImplementedException();
    //}
}
