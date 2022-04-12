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
    //기본 스킬 활성화 범위 표시라인
    public Image skillGuideLine;

    //스킬버튼 어디로 드래그했는지 확인하는 변수
    Collider col;

    //스킬 버튼 트랜스폼(캔버스용)
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
                    //각 case에 각각의 시너지 스킬 함수 호출
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
    /// SkillButtonDrop()의 switch문에 들어갈 기본 스킬 함수; instance로 외부 참조해도 될 듯
    /// </summary>
    void OriginalSkill()
    {

    }

    /// <summary>
    /// SkillButtonDrop()의 switch문에 들어갈 시너지 스킬 함수; instance로 외부 참조해도 될 듯
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
            //현재 선택 중인 물체의 이름이 "Btn_Skill"이면
            if (Input.GetKey(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject.name == "Btn_Skill")
            {
                //스킬 버튼 위치 = 마우스 위치
                rTr.position = Input.mousePosition;
            }
        }
    }

    /// <summary>
    /// 스킬 버튼 드랍 함수; 기본 스킬, 시너지 스킬 등의 사용 담당
    /// </summary>
    void SkillButtonDrop()
    {
        //null ref 오류 방지
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            //스킬 버튼 드랍시
            if (Input.GetKeyUp(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject.name == "Btn_Skill")
            {
                //스킬 버튼 원위치
                rTr.position = new Vector3(110, 140, 0);

                //스킬이 쿨타임에 안 걸려 있다면 && null ref 오류 방지
                if (!isCooling && col != null)
                {


                    //각 case에 맞는 스킬 함수 호출
                    //print(col.gameObject.name) 대신 스킬 함수 넣어야 됨
                    //default..뭔지 잘 모르겠음
                    switch (col.gameObject.name)
                    {
                        case "Img_SkillGuideLine":
                            print(col.gameObject.name);


                            //OnTriggerExit이 있어야 기본 스킬 다음 기본 스킬을 쓸 수 있음
                            //OnTriggerExit이 없으면 기본 스킬 다음엔 기본 스킬 호출 안 될 때도 있음
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
                col = null; //무한반복 방지
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        col = other;    //other 를 전역변수로 바꿈


        //기획에 따라 아래 switch문 필요성 결정
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

        //기본 스킬 활성화 표시
        if (other.gameObject.name == "Img_SkillGuideLine")
        {
            skillGuideLine.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        col = null; //trigger 초기화(스킬 사용시 다음에도 같은 스킬이 발동되는 것 방지)

        //기본 스킬 비활성화 표시
        if (other.gameObject.name == "Img_SkillGuideLine")
        {
            skillGuideLine.color = Color.gray;
        }
        
        //기획에 따라 아래 switch문 필요성 결정
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