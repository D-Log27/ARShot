using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
<<<<<<< HEAD
    Collision col;
=======
    //�⺻ ��ų Ȱ��ȭ ���� ǥ�ö���
    public Image skillGuideLine;

    //��ų��ư ���� �巡���ߴ��� Ȯ���ϴ� ����
    Collider col;

    //��ų ��ư Ʈ������(ĵ������)
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    RectTransform rTr;
    // Start is called before the first frame update
    void Start()
    {
        rTr = GetComponent<RectTransform>();
        rTr.position = new Vector3(1475, 630, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Btn_Skill")
            {
                rTr.position = Input.mousePosition;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (col == null)
            {
                rTr.position = new Vector3(1475, 630, 0);
            }
            else
            {
                rTr.position = new Vector3(1475, 630, 0);
                switch (col.gameObject.name)
                {
                    //�� case�� ������ �ó��� ��ų �Լ� ȣ��
                    case "Skill_Player2": print(col.gameObject.name); break;
                    case "Skill_Player3": print(col.gameObject.name); break;
                    case "Skill_Player4": print(col.gameObject.name); break;
                    default: print("default"); rTr.position = new Vector3(1475, 630, 0); break;
                }
                col = null;
            }
        }
    }

    /// <summary>
    /// SkillButtonDrop()�� switch���� �� �⺻ ��ų �Լ�; instance�� �ܺ� �����ص� �� ��
    /// </summary>
    void OriginalSkill()
    {

    }

    /// <summary>
    /// SkillButtonDrop()�� switch���� �� �ó��� ��ų �Լ�; instance�� �ܺ� �����ص� �� ��
    /// </summary>
    void SkillSynergy()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
<<<<<<< HEAD
        col = collision;
=======
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


                            //OnTriggerExit�� �־�� �⺻ ��ų ���� �⺻ ��ų�� �� �� ����
                            //OnTriggerExit�� ������ �⺻ ��ų ������ �⺻ ��ų ȣ�� �� �� ���� ����
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
        col = other;    //other �� ���������� �ٲ�


        //��ȹ�� ���� �Ʒ� switch�� �ʿ伺 ����
        //switch (col.gameObject.name)
        //{
        //    case "Img_SkillGuideLine":
        //        skillGuideLine.color = Color.red;
        //        break;

        //    case "Img_Synergy2GuideLine":
        //        synergy2GuideLine.color = Color.red;
        //        break;

        //    case "Img_Synergy3GuideLine":
        //        synergy3GuideLine.color = Color.red;
        //        break;

        //    case "Img_Synergy4GuideLine":
        //        synergy4GuideLine.color = Color.red;
        //        break;

        //    default:
        //        print("default");
        //        break;
        //}

        //�⺻ ��ų Ȱ��ȭ ǥ��
        if (other.gameObject.name == "Img_SkillGuideLine")
        {
            skillGuideLine.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        col = null; //trigger �ʱ�ȭ(��ų ���� �������� ���� ��ų�� �ߵ��Ǵ� �� ����)

        //�⺻ ��ų ��Ȱ��ȭ ǥ��
        if (other.gameObject.name == "Img_SkillGuideLine")
        {
            skillGuideLine.color = Color.gray;
        }
        
        //��ȹ�� ���� �Ʒ� switch�� �ʿ伺 ����
        //switch (col.gameObject.name)
        //{
        //    case "Img_SkillGuideLine":
        //        skillGuideLine.color = Color.gray;
        //        break;
        
        //    case "Img_Synergy2GuideLine":
        //        synergy2GuideLine.color = Color.gray;
        //        break;
        
        //    case "Img_Synergy3GuideLine":
        //        synergy3GuideLine.color = Color.gray;
        //        break;
        
        //    case "Img_Synergy4GuideLine":
        //        synergy4GuideLine.color = Color.gray;
        //        break;
        
        //    default:
        //        print("default");
        //        break;
        //}
>>>>>>> 97c0e2f267f53e72f4a1700c1c3cf7a101afe33e
    }
}