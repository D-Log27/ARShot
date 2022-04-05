using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    Vector3 lastPositon;
    Vector3 curPositon;
    Vector3 doPosition;
    GameObject go;

    Collision col;
    RectTransform rTr;
    // Start is called before the first frame update
    void Start()
    {
        rTr = GetComponent<RectTransform>();
        rTr.position = new Vector3(698, 298, 0);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject.name == "Btn_Skill")
        {
            rTr.position = Input.mousePosition;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (col == null)
            {
                rTr.position = new Vector3(698, 298, 0);
            }
            else
            {
                rTr.position = new Vector3(851, 351, 0);
                switch (col.gameObject.name)
                {
                    case "Skill_Player2": print(col.gameObject.name); break;
                    case "Skill_Player3":; break;
                    case "Skill_Player4":; break;
                    default: print("default"); rTr.position = new Vector3(698, 298, 0); break;
                }
                col = null;
                rTr.position = new Vector3(698, 298, 0);
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
