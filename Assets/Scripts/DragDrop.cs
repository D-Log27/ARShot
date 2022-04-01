using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Collision col;
    RectTransform rTr;
    // Start is called before the first frame update
    void Start()
    {
        rTr = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            rTr.position = Input.mousePosition;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //rTr.position = new Vector3(851, 351, 0);
            switch (col.gameObject.name)
            {
                case "Skill_Player2":; break;
                case "Skill_Player3":; break;
                case "Skill_Player4":; break;
                default: rTr.position = new Vector3(851, 351, 0); break;
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
