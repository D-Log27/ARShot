using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// <��ǥ>
// 1. ����ڰ� ��ũ���� ��ġ�ϸ�,
// 2. ��ũ���� ���߾ӿ��� ���̸� ��Ƽ� Plan�� �����Ѵ�.
// 2.1) ���̸� ����ϱ� ���� AR Raycast Manager�� �����´�.
// 2.2) ARRaycastManager Ŭ������ Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType trackableTypes = (TrackableType)(-1)); �޼��� ���
//2.2.1) Vector2�� ��ũ���� ���߾� ��ġ�� ��� ���� ����
//2.2.2) List<ARRaycastHit> ���� ����
//2.2.3) TrackableType�� ����� Plan�� �����ϵ���
// 3. �� ���� ��Ŀ�� ����


public class AnchorRay_KSY : MonoBehaviour
{

    private GameObject introAnchorObj; //���̸� ���� ������ ������Ʈ�̹Ƿ� private���� ����
    private ARRaycastManager arManager;
    private ARAnchorManager aRAnchor;

    void Start()
    {
        arManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // TODO: �հ��� ��ġ ������ ����(

        //public bool Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType trackableTypes = (TrackableType)(-1));
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //ScreenŬ������ ����� ȭ���� ���߾� 
        List<ARRaycastHit> arRaycastHit = new List<ARRaycastHit>();

        if (arManager.Raycast(screenCenter, arRaycastHit, TrackableType.Planes)) //
        {
            // : Anchor�Լ� ȣ���ϱ�
        }
    }
}

  
