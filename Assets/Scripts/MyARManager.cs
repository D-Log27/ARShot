using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶�(ȭ���߾�)�� ���� ���� ��Ŀ�� �ִٸ�
// �װ��� ������Ʈ�� ��ġ�ϰ� �ʹ�.
// �ε������Ͱ� �������� �ε������͸� Ŭ��(��ġ)�ϸ�
// �� ��ġ�� ��ü�� ��ġ�ϰ� �ʹ�.

public class MyARManager : MonoBehaviour
{
    // "GameObject(AR)"�� "indicator�� ��ġ(Transform)"�� ��Ÿ������
    public Transform indicator; // AR ������Ʈ�� ��Ÿ�� ��ġ
    public GameObject factory; // AR�� ������ 3D������Ʈ
   
    void Update()
    {
        // ���̸� �������! ���̰� ������ ��ġ�� ����ī�޶��� ��ġ�� ���̰� ���� ������ ����ī�޶��� �չ���
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // ī�޶�(ȭ���߾�)�� ���� ���� �ٴ��� �ִٸ�
         RaycastHit hitInfo; //������Ʈ�� Ray�� �¾Ҵٴ� ���� RaycastHit�� ���ؼ� ������ �Ѵ�.
        if (Physics.Raycast(ray, out hitInfo)) // ���� ������Ʈ�� Ray�� �¾Ҵٸ�,
        {
            if (hitInfo.transform.name.Equals("Floor")) // �׸��� �� ������Ʈ�� "Floor"�̶��,
            {
                indicator.gameObject.SetActive(true); // �ε������͸� Ȱ��ȭ�ؼ� �װ��� �ε������͸� ��ġ�ϰ� �ʹ�.
                indicator.transform.position = hitInfo.point + hitInfo.normal * 0.1f; // �׸��� �ε��������� ��ġ�� hitInfo�� point�� ��ġ�ϰ� �ϰ� �ʹ�.
            }
            else //"Floor"�ܿ��� �ε������͸� ���� �Ⱥ��̰� �ϰ� �ʹ�.
            {
                indicator.gameObject.SetActive(false);
            }
        }
        // ���� �ε������Ͱ� ���̴� ���¶��
        if (true == indicator.gameObject.activeSelf) //gameObject.activeSelf�� ������Ʈ�� ���� ���̰� ������ �Ǿ��ٴ� ��
        {
            //�ε������͸� Ŭ��(��ġ)������
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo2;
            if (Physics.Raycast(ray2, out hitInfo2))
            {
                if (hitInfo2.transform.name.Equals("Indicator"))
                {
                    //�� ��ü ��ü�� ��ġ�ϰ� �ʹ�.
                    GameObject obj = Instantiate(factory);
                    obj.transform.position = hitInfo2.point;
                }
                
            }
        }
    }

}
