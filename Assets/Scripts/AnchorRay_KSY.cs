using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//TODO: �ٴ��� ���� ȭ���� ��ġ�ϸ� Plan�� 
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
        Debug.Log("### isCreated : " + isCreated); // ����1: Core�� ���� ���� �ʾ��� ��,

        if (Input.touchCount == 0) return; 
        Debug.Log("### istouchCount==0: " + Input.touchCount); //����2: ��ġ Ƚ���� 1�� �̻��� ��,

        Touch touch = Input.GetTouch(0); // ù��° ��ġ �������� �ڵ�

        if (touch.phase != TouchPhase.Began) return; //���� ������ ��ġ������ ����
        //���� ������ ��ġ�� "��������(true)"�̸� ����/ "������(����)�̸� ���� ���� ����"
        Debug.Log("### isTouchBegan" + touch.phase);

        if (raycastManager.Raycast(touch.position, arRaycastHit, TrackableType.Planes)) 
        {
            //���̰� ó�� ���� ���� ����
            var hitPose = arRaycastHit[0].pose; 
            //instantiateObject�� ����� anchorPrefab�� ó�� ���� ���� position�� rotation�� �����Ѵ�. 
            var instantiateObject = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation);
            
            //instantiateObject���� ARAnchor������Ʈ�� ������ anchor�̶�� �Ѵ�.
            anchor = instantiateObject.AddComponent<ARAnchor>();

            //���� ó�� ���� ���� ARPlane�̶�� 
            if (arRaycastHit[0].trackable is ARPlane plane) 
            {
                //�װ��� ��Ŀ�� �ڴ´�.
                anchor = anchorManager.AttachAnchor(plane, hitPose);
                //�׸��� �����Ѵ�.
                isCreated = true;
            } 
        }
    }
    //
}