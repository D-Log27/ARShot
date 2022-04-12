using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropAssignedNull : MonoBehaviour
{
    //�⺻ ��ų Ȱ��ȭ ���� ǥ�ö���
    public Image skillGuideLine;

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

    void OriginalSkill()
    {

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
        //null ref ���� ����
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


                    //�� case�� �´� ��ų �Լ� ȣ��
                    //print(col.gameObject.name) ��� ��ų �Լ� �־�� ��
                    //default..���� �� �𸣰���
                    switch (col.gameObject.name)
                    {
                        case "Img_SkillGuideLine":
                            print(col.gameObject.name);
                            skillGuideLine.gameObject.SetActive(false);
                            skillGuideLine.gameObject.SetActive(true);
                            break;

                        case "Skill_Player2":
                            print(col.gameObject.name);
                            break;

                        case "Skill_Player3":
                            print(col.gameObject.name);
                            break;

                        case "Skill_Player4":
                            print(col.gameObject.name);
                            break;

                        default:
                            print("default");
                            rTr.position = new Vector3(110, 140, 0);
                            break;
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
        print("Ʈ���� ��������");
    }
}