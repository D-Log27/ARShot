using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    Collision col;
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

    void SkillSynergy()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        col = collision;
    }
}
