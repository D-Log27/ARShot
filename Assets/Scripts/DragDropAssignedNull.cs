using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropAssignedNull : MonoBehaviour
{
    //기본 스킬 활성화 범위 표시라인
    public Image skillGuideLine;

    //스킬버튼 어디로 드래그했는지 확인하는 변수
    Collider col;

    //스킬 버튼 트랜스폼(캔버스용)
    RectTransform rTr;

    //스킬이 현재 쿨타임에 걸렸는지 여부 확인하는 변수
    bool isCooling;

    // Start is called before the first frame update
    void Start()
    {
        //초기 설정 잘 하면 지워도 됨
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
    /// 스킬 버튼 드래그 함수
    /// </summary>
    void SkillButtonDrag()
    {
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
        col = other;
    }

    private void OnTriggerExit(Collider other)
    {
        col = null;
        print("트리거 빠져나감");
    }
}