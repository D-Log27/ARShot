using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//��ǥ1 : AR Tracked Image Manager�� ������ ��Ŀ�� ������ ������ �ʹ�.
//��ǥ2 : ���� ������ ��Ŀ�� �ִٸ� Ư�� 3D ������Ʈ�� ��Ŀ�� ��ġ�� ��ġ�ϰ� �ʹ�.
//��ǥ3 : �� 3D ������Ʈ�� ���̰� �ϰ� �ʹ�.

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
