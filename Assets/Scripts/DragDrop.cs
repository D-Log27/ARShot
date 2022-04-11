using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    //��ų��ư ���� �巡���ߴ��� Ȯ���ϴ� ����
    Collider col;

    //��ų ��ư Ʈ������(ĵ������)
    RectTransform rTr;

    //��ų�� ���� ��Ÿ�ӿ� �ɷȴ��� ���� Ȯ���ϴ� ����
    bool isCooling;

    // Start is called before the first frame update
    void Start()
    {
        //�ʱ� ���� �� �ϸ� ������ ��
        rTr = GetComponent<RectTransform>();
        rTr.position = new Vector3(110, 140, 0);
    }


    // Update is called once per frame
    void Update()
    {
        SkillButtonDrag();
        SkillButtonDrop();
    }

    void SkillSynergy()
    {

    }

    /// <summary>
    /// ��ų ��ư �巡�� �Լ�
    /// </summary>
    void SkillButtonDrag()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            //���� ���� ���� ��ü�� �̸��� "Btn_Skill"�̸�
            if (Input.GetKey(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject.name == "Btn_Skill")
            {
                //��ų ��ư ��ġ = ���콺 ��ġ
                rTr.position = Input.mousePosition;
            }
        }
    }

    /// <summary>
    /// ��ų ��ư ��� �Լ�; �⺻ ��ų, �ó��� ��ų ���� ��� ���
    /// </summary>
    void SkillButtonDrop()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            //��ų ��ư �����
            if (Input.GetKeyUp(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject.name == "Btn_Skill")
            {
                //��ų ��ư ����ġ
                rTr.position = new Vector3(110, 140, 0);

                //��ų�� ��Ÿ�ӿ� �� �ɷ� �ִٸ� && null ref ���� ����
                if (!isCooling && col != null)
                {
                    //�� case�� �´� �ó��� ��ų �Լ� ȣ��
                    switch (col.gameObject.name)
                    {
                        case "Skill_Player2": print(col.gameObject.name); break;
                        case "Skill_Player3": print(col.gameObject.name); break;
                        case "Skill_Player4": print(col.gameObject.name); break;
                        default: print("default"); rTr.position = new Vector3(110, 140, 0); break;
                    }
                }
                col = null; //���ѹݺ� ����
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        col = other;
    }

    private void OnTriggerExit(Collider other)
    {
        col = null;
    }
}
